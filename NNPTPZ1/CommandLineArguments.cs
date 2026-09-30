using System;
using System.Globalization;
using NNPTPZ1.Fractals;

namespace NNPTPZ1
{
    public class CommandLineArguments
    {
        public const string Usage =
            "Usage: NNPTPZ1 <width> <height> <minReal> <maxReal> <minImaginary> <maxImaginary> [outputPath]";

        public const string DefaultOutputPath = "../../../out.png";

        private const int RequiredArgumentCount = 6;

        public RenderSettings Settings { get; }
        public string OutputPath { get; }

        private CommandLineArguments(RenderSettings settings, string outputPath)
        {
            Settings = settings;
            OutputPath = outputPath;
        }

        /// <exception cref="ArgumentException">Arguments are missing or invalid.</exception>
        public static CommandLineArguments Parse(string[] args)
        {
            if (args == null || args.Length < RequiredArgumentCount)
            {
                throw new ArgumentException($"Expected at least {RequiredArgumentCount} arguments.");
            }

            var settings = new RenderSettings(
                width: ParseInt(args[0], "width"),
                height: ParseInt(args[1], "height"),
                minReal: ParseDouble(args[2], "minReal"),
                maxReal: ParseDouble(args[3], "maxReal"),
                minImaginary: ParseDouble(args[4], "minImaginary"),
                maxImaginary: ParseDouble(args[5], "maxImaginary"));

            string outputPath = args.Length > RequiredArgumentCount
                ? args[RequiredArgumentCount]
                : DefaultOutputPath;

            return new CommandLineArguments(settings, outputPath);
        }

        private static int ParseInt(string value, string name)
        {
            if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
            {
                throw new ArgumentException($"Argument '{name}' must be an integer, got '{value}'.");
            }

            return result;
        }

        private static double ParseDouble(string value, string name)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
            {
                throw new ArgumentException($"Argument '{name}' must be a number, got '{value}'.");
            }

            return result;
        }
    }
}
