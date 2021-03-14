using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Comment {
    public sealed record CommentContent {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            "(?=[a-zA-Zа-яёА-ЯЁ0-9äöüÄÖÜ\"'?!]+[\\w-])(?=.{1,100}$).*",
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