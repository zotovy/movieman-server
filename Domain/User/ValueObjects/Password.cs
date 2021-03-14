
using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects {
    public sealed record Password {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            @"",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Password(string value) {
            if (!Validator.IsMatch(value)) {
                throw new ArgumentException($"{value} is invalid Password value.");
            }

            Value = value;
        }
    }
}

