using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvMaze.DataAccess.Models;

namespace TvMaze.DataAccess.Repositories
{
    public interface ITvMazeRepository
    {
        Task AddShow(Show show);
        Task<IReadOnlyCollection<Show>> GetShowsWithCast(int skip, int take);
    }
}
