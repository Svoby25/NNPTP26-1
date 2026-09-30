using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        private const double Delta = 1e-9;

        [TestMethod]
        public void Add_ReturnsSumOfParts()
        {
            var a = new ComplexNumber(10, 20);
            var b = new ComplexNumber(1, 2);

            Assert.AreEqual(new ComplexNumber(11, 22), a.Add(b));
        }

        [TestMethod]
        public void Add_Zero_ReturnsSameNumber()
        {
            var a = new ComplexNumber(1, -1);

            Assert.AreEqual(a, a.Add(ComplexNumber.Zero));
        }

        [TestMethod]
        public void Subtract_ReturnsDifferenceOfParts()
        {
            var a = new ComplexNumber(10, 20);
            var b = new ComplexNumber(1, 2);

            Assert.AreEqual(new ComplexNumber(9, 18), a.Subtract(b));
        }

        [TestMethod]
        public void Multiply_ReturnsProduct()
        {
            var a = new ComplexNumber(1, 2);
            var b = new ComplexNumber(3, 4);

            // (1 + 2i)(3 + 4i) = 3 + 4i + 6i + 8i^2 = -5 + 10i
            Assert.AreEqual(new ComplexNumber(-5, 10), a.Multiply(b));
        }

        [TestMethod]
        public void Divide_ReturnsQuotient()
        {
            var a = new ComplexNumber(10, 20);
            var b = new ComplexNumber(1, 2);

            Assert.AreEqual(new ComplexNumber(10, 0), a.Divide(b));
        }

        [TestMethod]
        public void Divide_IsInverseOfMultiply()
        {
            var a = new ComplexNumber(-3.5, 1.25);
            var b = new ComplexNumber(0.5, -2);

            ComplexNumber result = a.Multiply(b).Divide(b);

            Assert.AreEqual(a.Real, result.Real, Delta);
            Assert.AreEqual(a.Imaginary, result.Imaginary, Delta);
        }

        [TestMethod]
        public void Conjugate_NegatesImaginaryPart()
        {
            Assert.AreEqual(new ComplexNumber(3, -4), new ComplexNumber(3, 4).Conjugate());
        }

        [TestMethod]
        public void Magnitude_ReturnsDistanceFromOrigin()
        {
            var number = new ComplexNumber(3, 4);

            Assert.AreEqual(25, number.MagnitudeSquared, Delta);
            Assert.AreEqual(5, number.Magnitude, Delta);
        }

        [TestMethod]
        public void Argument_ReturnsAngleInRadians()
        {
            Assert.AreEqual(Math.PI / 4, new ComplexNumber(1, 1).Argument, Delta);
            Assert.AreEqual(3 * Math.PI / 4, new ComplexNumber(-1, 1).Argument, Delta);
        }

        [TestMethod]
        public void Equals_SameParts_AreEqualWithSameHashCode()
        {
            var a = new ComplexNumber(1.5, -2);
            var b = new ComplexNumber(1.5, -2);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Equals_DifferentPartsOrType_AreNotEqual()
        {
            var a = new ComplexNumber(1, 2);

            Assert.IsFalse(a.Equals(new ComplexNumber(2, 1)));
            Assert.IsFalse(a.Equals(null));
            Assert.IsFalse(a.Equals("(1 + 2i)"));
        }

        [TestMethod]
        public void ToString_FormatsRealAndImaginaryPart()
        {
            Assert.AreEqual("(10 + 20i)", new ComplexNumber(10, 20).ToString());
            Assert.AreEqual("(1 + -1i)", new ComplexNumber(1, -1).ToString());
            Assert.AreEqual("(0 + 0i)", ComplexNumber.Zero.ToString());
        }
    }
}
