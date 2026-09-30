using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Fractals.Tests
{
    [TestClass]
    public class NewtonFractalRendererTests
    {
        private const double RootTolerance = 0.1;

        // x^3 + 1, roots are -1 and (1 +- i*sqrt(3)) / 2
        private static Polynomial CreateCubic() =>
            new Polynomial(ComplexNumber.One, ComplexNumber.Zero, ComplexNumber.Zero, ComplexNumber.One);

        [TestMethod]
        public void Render_ReturnsBitmapOfRequestedSize()
        {
            var renderer = new NewtonFractalRenderer(CreateCubic());

            using (Bitmap bitmap = renderer.Render(new RenderSettings(40, 20, -2, 2, -1, 1)))
            {
                Assert.AreEqual(40, bitmap.Width);
                Assert.AreEqual(20, bitmap.Height);
            }
        }

        [TestMethod]
        public void Render_NonSquareImage_FillsWholeBitmap()
        {
            var renderer = new NewtonFractalRenderer(CreateCubic());

            using (Bitmap wide = renderer.Render(new RenderSettings(30, 10, -2, 2, -2, 2)))
            using (Bitmap tall = renderer.Render(new RenderSettings(10, 30, -2, 2, -2, 2)))
            {
                Assert.AreNotEqual(0, wide.GetPixel(29, 9).A);
                Assert.AreNotEqual(0, tall.GetPixel(9, 29).A);
            }
        }

        [TestMethod]
        public void Render_FindsAllRootsOfPolynomial()
        {
            var renderer = new NewtonFractalRenderer(CreateCubic());

            renderer.Render(new RenderSettings(50, 50, -2, 2, -2, 2)).Dispose();

            Assert.AreEqual(3, renderer.FoundRoots.Count);
            AssertContainsRoot(renderer, new ComplexNumber(-1, 0));
            AssertContainsRoot(renderer, new ComplexNumber(0.5, Math.Sqrt(3) / 2));
            AssertContainsRoot(renderer, new ComplexNumber(0.5, -Math.Sqrt(3) / 2));
        }

        [TestMethod]
        public void Render_PixelsConvergingToDifferentRoots_HaveDifferentColors()
        {
            var renderer = new NewtonFractalRenderer(CreateCubic());

            // 4x4 pixels over [-2, 2] x [-2, 2]: pixel (1, 2) starts at -1 + 0i (a root),
            // pixel (2, 3) starts at 0 + 1i which converges to (1 + i*sqrt(3)) / 2
            using (Bitmap bitmap = renderer.Render(new RenderSettings(4, 4, -2, 2, -2, 2)))
            {
                Color nearNegativeOne = bitmap.GetPixel(1, 2);
                Color nearUpperRoot = bitmap.GetPixel(2, 3);

                Assert.AreNotEqual(nearNegativeOne.ToArgb(), nearUpperRoot.ToArgb());
            }
        }

        [TestMethod]
        public void Constructor_Null_Throws()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new NewtonFractalRenderer(null));
        }

        private static void AssertContainsRoot(NewtonFractalRenderer renderer, ComplexNumber expected)
        {
            foreach (ComplexNumber root in renderer.FoundRoots)
            {
                if (root.Subtract(expected).Magnitude < RootTolerance)
                {
                    return;
                }
            }

            Assert.Fail($"Root {expected} was not found.");
        }
    }
}
