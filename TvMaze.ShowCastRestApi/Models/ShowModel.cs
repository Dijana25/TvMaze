using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TvMaze.ShowCastRestApi.Models
{
    public class ShowModel
    {
        public int Id { get; }

        public string Name { get; }

        public List<CastModel> Cast { get; }

        public ShowModel(int id, string name, List<CastModel> cast)
        {
            Id = id;
            Name = name;
            Cast = cast;
        }
    }
}
