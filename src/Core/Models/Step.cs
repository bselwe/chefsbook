using System;

namespace ChefsBook.Core.Models
{
    public class Step
    {
        public Guid Id { get; private set; }
        public TimeSpan? Duration { get; private set; }
        public string Description { get; private set; }
        public int SequenceNumber { get; private set; }

        private Step() { }

        public static Step Create(TimeSpan? duration, string description, int sequenceNumber = 0)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Step description cannot be empty or whitespace.");
            }

            return new Step
            {
                Id = Guid.NewGuid(),
                Duration = duration,
                Description = description,
                SequenceNumber = sequenceNumber
            };
        }
    }
}