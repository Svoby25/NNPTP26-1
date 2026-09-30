using System;

namespace NNPTPZ1.Fractals
{
    /// <summary>
    /// Size of the output image and the area of the complex plane it displays.
    /// </summary>
    public class RenderSettings
    {
        public int Width { get; }
        public int Height { get; }
        public double MinReal { get; }
        public double MaxReal { get; }
        public double MinImaginary { get; }
        public double MaxImaginary { get; }

        public RenderSettings(int width, int height,
            double minReal, double maxReal, double minImaginary, double maxImaginary)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width must be positive.");
            }
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Height must be positive.");
            }

            Width = width;
            Height = height;
            MinReal = minReal;
            MaxReal = maxReal;
            MinImaginary = minImaginary;
            MaxImaginary = maxImaginary;
        }

        public double RealStep => (MaxReal - MinReal) / Width;

        public double ImaginaryStep => (MaxImaginary - MinImaginary) / Height;
    }
}
