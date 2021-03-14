using System;
using System.Text.RegularExpressions;

namespace Domain.Movie.ValueObjects {
    public sealed record Year {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            @"^(19[5-9]\d|20[0-4]\d|2050)$",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Year(string value) {
            if (!Validator.IsMatch(value)) {
                throw new ArgumentException($"{value} is invalid Year value.");
            }

            Value = value;
        }
    }
}