using System;
using System.Collections.Generic;
using amplitude.tool.Utilities;

namespace amplitude.tool.Events.Domain.Model
{
    public struct ValidatedEvents
    {
        public readonly List<Validation> Validations;

        public ValidatedEvents(List<Validation> validations)
        {
            Validations = validations;
        }

        bool Equals(ValidatedEvents other) => Validations.SafeSequenceEqual(other.Validations);

        public override bool Equals(object obj) => obj is ValidatedEvents other && Equals(other);

        public override string ToString() => Validations != null ? String.Join(',', Validations) : string.Empty;
    }
}