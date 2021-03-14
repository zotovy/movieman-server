using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects {
    public sealed record Name {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Name(string value) {
            if (!Validator.IsMatch(value)) {
                throw new ArgumentException($"{value} is invalid name value.");
            }

            Value = value;
        }
    }
}