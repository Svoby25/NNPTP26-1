using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class PolynomialTests
    {
        // x^2 + 1
        private static readonly Polynomial Quadratic =
            new Polynomial(ComplexNumber.One, ComplexNumber.Zero, ComplexNumber.One);

        [TestMethod]
        public void Evaluate_RealPoints_ReturnsValues()
        {
            Assert.AreEqual(new ComplexNumber(1, 0), Quadratic.Evaluate(new ComplexNumber(0, 0)));
            Assert.AreEqual(new ComplexNumber(2, 0), Quadratic.Evaluate(new ComplexNumber(1, 0)));
            Assert.AreEqual(new ComplexNumber(5, 0), Quadratic.Evaluate(new ComplexNumber(2, 0)));
        }

        [TestMethod]
        public void Evaluate_Root_ReturnsZero()
        {
            Assert.AreEqual(ComplexNumber.Zero, Quadratic.Evaluate(new ComplexNumber(0, 1)));
        }

        [TestMethod]
        public void Evaluate_EmptyPolynomial_ReturnsZero()
        {
            Assert.AreEqual(ComplexNumber.Zero, new Polynomial().Evaluate(new ComplexNumber(3, 4)));
        }

        [TestMethod]
        public void Derive_ReturnsDerivative()
        {
            // 1 + 2x + 3x^2 + 4x^3 -> 2 + 6x + 12x^2
            var polynomial = new Polynomial(
                new ComplexNumber(1, 0), new ComplexNumber(2, 0), new ComplexNumber(3, 0), new ComplexNumber(4, 0));

            Polynomial derivative = polynomial.Derive();

            CollectionAssert.AreEqual(
                new[] { new ComplexNumber(2, 0), new ComplexNumber(6, 0), new ComplexNumber(12, 0) },
                new[] { derivative.Coefficients[0], derivative.Coefficients[1], derivative.Coefficients[2] });
            Assert.AreEqual(3, derivative.Coefficients.Count);
        }

        [TestMethod]
        public void Derive_Constant_ReturnsEmptyPolynomial()
        {
            Assert.AreEqual(0, new Polynomial(ComplexNumber.One).Derive().Coefficients.Count);
        }

        [TestMethod]
        public void Constructor_Null_Throws()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Polynomial(null));
        }

        [TestMethod]
        public void ToString_ListsTermsFromConstant()
        {
            Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", Quadratic.ToString());
        }
    }
}
