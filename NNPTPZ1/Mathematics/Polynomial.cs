using System;
using System.Collections.Generic;
using System.Linq;

namespace NNPTPZ1.Mathematics
{
    /// <summary>
    /// Polynomial with complex coefficients, ordered from the constant term upwards
    /// (coefficient at index i belongs to x^i).
    /// </summary>
    public class Polynomial
    {
        private readonly List<ComplexNumber> coefficients;

        public Polynomial(params ComplexNumber[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            this.coefficients = coefficients.ToList();
        }

        public IReadOnlyList<ComplexNumber> Coefficients => coefficients;

        public Polynomial Derive()
        {
            ComplexNumber[] derivedCoefficients = coefficients
                .Skip(1)
                .Select((coefficient, index) => coefficient.Multiply(new ComplexNumber(index + 1, 0)))
                .ToArray();

            return new Polynomial(derivedCoefficients);
        }

        /// <summary>Evaluates the polynomial at the given point using Horner's method.</summary>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int power = coefficients.Count - 1; power >= 0; power--)
            {
                result = result.Multiply(x).Add(coefficients[power]);
            }

            return result;
        }

        public override string ToString()
        {
            var terms = coefficients.Select((coefficient, power) =>
                coefficient + new string('x', power));

            return string.Join(" + ", terms);
        }
    }
}
