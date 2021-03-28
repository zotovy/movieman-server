using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Comment {
    public sealed record CommentContent {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            "^[A-Za-z0-9а-яёА-ЯЁ _]*[A-Za-z0-9а-яёА-ЯЁ][A-Za-z0-9 _а-яёА-ЯЁ]*$",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public CommentContent(string value) {
            if (!Validator.IsMatch(value) || value.Length > 1000) {
                throw new ArgumentException($"{value} is invalid CommentContent value.");
            }

            Value = value;
        }
    }
}