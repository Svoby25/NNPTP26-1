using System;
using System.Drawing;
using NNPTPZ1.Fractals;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// Produces an image of the Newton fractal of the polynomial x^3 + 1.
    /// </summary>
    public static class Program
    {
        public static int Main(string[] args)
        {
            CommandLineArguments arguments;
            try
            {
                arguments = CommandLineArguments.Parse(args);
            }
            catch (ArgumentException exception)
            {
                Console.Error.WriteLine(exception.Message);
                Console.Error.WriteLine(CommandLineArguments.Usage);
                return 1;
            }

            var polynomial = new Polynomial(
                ComplexNumber.One, ComplexNumber.Zero, ComplexNumber.Zero, ComplexNumber.One);
            var renderer = new NewtonFractalRenderer(polynomial);

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomial.Derive());

            using (Bitmap bitmap = renderer.Render(arguments.Settings))
            {
                bitmap.Save(arguments.OutputPath);
            }

            return 0;
        }
    }
}
