using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TvMaze.DataAccess.Models
{
    public class Cast
    {
        public int Id { get; set; }

        public int TvMazeId { get; set; }

        public Show Show { get; set; }

        public string Name { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
