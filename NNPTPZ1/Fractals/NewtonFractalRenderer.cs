using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractals
{
    /// <summary>
    /// Renders a Newton fractal: every pixel is colored by the root of the polynomial
    /// that Newton's method converges to when started from that pixel, and darkened
    /// by the number of iterations it took.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    public class NewtonFractalRenderer
    {
        private const int RequiredConvergingSteps = 30;
        private const int MaxIterations = 1000;
        private const double ConvergingStepThreshold = 0.5;
        private const double RootTolerance = 0.01;
        private const double ZeroCoordinateReplacement = 0.0001;
        private const int DarkeningPerIteration = 2;

        private static readonly Color[] RootColors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private readonly Polynomial polynomial;
        private readonly Polynomial derivative;
        private readonly List<ComplexNumber> roots = new List<ComplexNumber>();

        public NewtonFractalRenderer(Polynomial polynomial)
        {
            this.polynomial = polynomial ?? throw new ArgumentNullException(nameof(polynomial));
            derivative = polynomial.Derive();
        }

        public IReadOnlyList<ComplexNumber> FoundRoots => roots;

        public Bitmap Render(RenderSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var bitmap = new Bitmap(settings.Width, settings.Height);
            for (int pixelY = 0; pixelY < settings.Height; pixelY++)
            {
                for (int pixelX = 0; pixelX < settings.Width; pixelX++)
                {
                    ComplexNumber start = ToComplexPlane(settings, pixelX, pixelY);
                    (ComplexNumber root, int iterations) = FindRoot(start);
                    int rootIndex = GetRootIndex(root);

                    bitmap.SetPixel(pixelX, pixelY, GetColor(rootIndex, iterations));
                }
            }

            return bitmap;
        }

        private static ComplexNumber ToComplexPlane(RenderSettings settings, int pixelX, int pixelY)
        {
            double real = settings.MinReal + pixelX * settings.RealStep;
            double imaginary = settings.MinImaginary + pixelY * settings.ImaginaryStep;

            // zero coordinates are shifted slightly to avoid starting in a stationary point
            return new ComplexNumber(
                real == 0 ? ZeroCoordinateReplacement : real,
                imaginary == 0 ? ZeroCoordinateReplacement : imaginary);
        }

        /// <summary>
        /// Runs Newton's method until enough small steps were made (large steps are not counted).
        /// </summary>
        private (ComplexNumber Root, int Iterations) FindRoot(ComplexNumber start)
        {
            ComplexNumber current = start;
            int convergingSteps = 0;
            int iterations = 0;

            while (convergingSteps < RequiredConvergingSteps && iterations < MaxIterations)
            {
                ComplexNumber step = polynomial.Evaluate(current).Divide(derivative.Evaluate(current));
                current = current.Subtract(step);
                iterations++;

                if (step.MagnitudeSquared < ConvergingStepThreshold)
                {
                    convergingSteps++;
                }
            }

            return (current, iterations);
        }

        private int GetRootIndex(ComplexNumber root)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (root.Subtract(roots[i]).MagnitudeSquared <= RootTolerance)
                {
                    return i;
                }
            }

            roots.Add(root);
            return roots.Count - 1;
        }

        private static Color GetColor(int rootIndex, int iterations)
        {
            Color baseColor = RootColors[rootIndex % RootColors.Length];
            int darkening = iterations * DarkeningPerIteration;

            return Color.FromArgb(
                Darken(baseColor.R, darkening),
                Darken(baseColor.G, darkening),
                Darken(baseColor.B, darkening));
        }

        private static int Darken(int colorComponent, int amount) =>
            Math.Max(0, colorComponent - amount);
    }
}
