using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public static readonly ComplexNumber Zero = new ComplexNumber(0, 0);
        public static readonly ComplexNumber One = new ComplexNumber(1, 0);

        public double Real { get; }
        public double Imaginary { get; }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public double MagnitudeSquared => Real * Real + Imaginary * Imaginary;

        public double Magnitude => Math.Sqrt(MagnitudeSquared);

        /// <summary>Angle of the number in the complex plane, in radians.</summary>
        public double Argument => Math.Atan2(Imaginary, Real);

        public ComplexNumber Add(ComplexNumber other) =>
            new ComplexNumber(Real + other.Real, Imaginary + other.Imaginary);

        public ComplexNumber Subtract(ComplexNumber other) =>
            new ComplexNumber(Real - other.Real, Imaginary - other.Imaginary);

        public ComplexNumber Multiply(ComplexNumber other) =>
            new ComplexNumber(
                Real * other.Real - Imaginary * other.Imaginary,
                Real * other.Imaginary + Imaginary * other.Real);

        public ComplexNumber Divide(ComplexNumber other)
        {
            // multiply numerator and denominator by the conjugate of the denominator
            ComplexNumber numerator = Multiply(other.Conjugate());
            double denominator = other.MagnitudeSquared;

            return new ComplexNumber(numerator.Real / denominator, numerator.Imaginary / denominator);
        }

        public ComplexNumber Conjugate() => new ComplexNumber(Real, -Imaginary);

        public override bool Equals(object obj) =>
            obj is ComplexNumber other
            && Real == other.Real
            && Imaginary == other.Imaginary;

        public override int GetHashCode()
        {
            unchecked
            {
                return (Real.GetHashCode() * 397) ^ Imaginary.GetHashCode();
            }
        }

        public override string ToString() => $"({Real} + {Imaginary}i)";
    }
}
