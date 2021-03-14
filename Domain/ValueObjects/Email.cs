using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects {
    public sealed record Email {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Email(string value) {
            if (!Validator.IsMatch(value)) {
                throw new ArgumentException($"{value} is invalid email address.");
            }

            Value = value;
        }
    }
}