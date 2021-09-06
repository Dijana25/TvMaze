using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.DataAccess.Models
{
    public class Show
    {
        public int Id { get; set; }

        public int TvMazeId { get; set; }

        public string Name { get; set; }

        public List<Cast> Cast { get; set; }
    }
}
