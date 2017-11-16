using System;

namespace ChefsBook.Core.Models
{
    public class Step
    {
        public Guid StepId { get; private set; }
        public TimeSpan? Duration { get; private set; }
        public string Description { get; private set; }

        private Step() { }

        public static Step Create(TimeSpan? duration, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Step description cannot be empty or whitespace.");
            }

            return new Step
            {
                StepId = Guid.NewGuid(),
                Duration = duration,
                Description = description
            };
        }
    }
}