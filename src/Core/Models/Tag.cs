using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Models
{
    public class Tag
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private Tag() { }

        public static Tag Create(string name)
        {
            return new Tag
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        public static Tag Create(Guid id, string name)
        {
            return new Tag
            {
                Id = id,
                Name = name
            };
        }
    }
}