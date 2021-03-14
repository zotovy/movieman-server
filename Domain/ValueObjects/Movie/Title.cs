using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Movie {
    public sealed record Title {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            "(?=[a-zA-Zа-яёА-ЯЁ0-9äöüÄÖÜ\"'?!]+[\\w-])(?=.{1,100}$).*",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Title(string value) {
            if (!Validator.IsMatch(value) || value.Length > 100) {
                throw new ArgumentException($"{value} is invalid Title value.");
            }

            Value = value;
        }
    }
}