using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Tests
{
    [TestClass]
    public class CommandLineArgumentsTests
    {
        [TestMethod]
        public void Parse_AllArguments_ReturnsSettingsAndOutputPath()
        {
            CommandLineArguments arguments = CommandLineArguments.Parse(
                new[] { "300", "200", "-1.5", "1.5", "-1", "1", "fractal.png" });

            Assert.AreEqual(300, arguments.Settings.Width);
            Assert.AreEqual(200, arguments.Settings.Height);
            Assert.AreEqual(-1.5, arguments.Settings.MinReal);
            Assert.AreEqual(1.5, arguments.Settings.MaxReal);
            Assert.AreEqual(-1.0, arguments.Settings.MinImaginary);
            Assert.AreEqual(1.0, arguments.Settings.MaxImaginary);
            Assert.AreEqual("fractal.png", arguments.OutputPath);
        }

        [TestMethod]
        public void Parse_WithoutOutputPath_UsesDefault()
        {
            CommandLineArguments arguments = CommandLineArguments.Parse(
                new[] { "300", "200", "-1.5", "1.5", "-1", "1" });

            Assert.AreEqual(CommandLineArguments.DefaultOutputPath, arguments.OutputPath);
        }

        [TestMethod]
        public void Parse_TooFewArguments_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => CommandLineArguments.Parse(new[] { "300", "200" }));
            Assert.ThrowsException<ArgumentException>(() => CommandLineArguments.Parse(null));
        }

        [TestMethod]
        public void Parse_InvalidNumber_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() => CommandLineArguments.Parse(
                new[] { "300", "abc", "-1.5", "1.5", "-1", "1" }));
            Assert.ThrowsException<ArgumentException>(() => CommandLineArguments.Parse(
                new[] { "300", "200", "-1.5", "x", "-1", "1" }));
        }

        [TestMethod]
        public void Parse_NonPositiveSize_Throws()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => CommandLineArguments.Parse(
                new[] { "0", "200", "-1.5", "1.5", "-1", "1" }));
        }
    }
}
