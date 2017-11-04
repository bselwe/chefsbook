using System;

namespace ChefsBook.Core.Models
{
    public class Tag
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public static Tag Create(string name)
        {
            return new Tag
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }
    }
}