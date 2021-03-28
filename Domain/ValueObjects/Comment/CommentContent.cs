using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Comment {
    public sealed record CommentContent {
        public string Value { get; }


        public CommentContent(string value) {
            if (value.Length > 1000 || value.Length == 0) {
                throw new ArgumentException($"{value} is invalid CommentContent value.");
            }

            Value = value;
        }
    }
}
