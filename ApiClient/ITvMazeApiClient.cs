using System.Collections.Generic;
using System.Threading.Tasks;

namespace TvMaze.ApiClient
{
    public interface ITvMazeApiClient
    {
        Task<List<ShowInfo>> GetShowsAsync(int page);
        Task<List<Person>> GetShowCastAsync(int showId);
    }
}
