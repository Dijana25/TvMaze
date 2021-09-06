using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.ApiClient.Models;

namespace TvMaze.ApiClient
{
    public interface ITvMazeApiClient
    {
        Task<List<ShowInfo>> GetShowsAsync(int page);
        Task<List<CastItem>> GetShowCastAsync(int showId);
    }
}
