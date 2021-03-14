using System;

namespace Domain.ValueObjects.Review {
    public sealed record ReviewContent {
        public string Value { get; }

        public static bool Validate(string value) {
            return value.Length > 0 && value.Length <= 1000;
        }

        public ReviewContent(string value) {
            if (!Validate(value)) {
                throw new ArgumentException($"{value} is invalid ReviewContent value.");
            }

            Value = value;
        }
    }
}