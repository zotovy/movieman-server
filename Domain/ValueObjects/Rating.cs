using System;

namespace Domain.ValueObjects {
    public sealed record Rating {
        public double Value { get; }

        public static bool Validate(double value) {
            return value >= 0 && value <= 10;
        }

        public Rating(double value) {
            if (!Validate(value)) {
                throw new ArgumentException($"{value} is invalid Rating value.");
            }

            Value = value;
        }
    }
}