using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.ApiClient.Models
{
    public class CastItem
    {
        public Person Person { get; }

        public CastItem(Person person)
        {
            Person = person;
        }
    }
}
