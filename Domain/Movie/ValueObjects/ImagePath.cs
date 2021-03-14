using System;
using System.Text.RegularExpressions;

namespace Domain.Movie.ValueObjects {
    public sealed record ImagePath {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            @"(https?:\/\/.*\.(?:png|jpg|jpeg))",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public ImagePath(string value) {
            if (!Validator.IsMatch(value)) {
                throw new ArgumentException($"{value} is invalid ImagePath value.");
            }

            Value = value;
        }
    }
}