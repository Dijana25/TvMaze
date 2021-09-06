using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMaze.ApiClient;
using TvMaze.DataAccess.Models;
using TvMaze.DataAccess.Repositories;

namespace TvMaze.Scraper
{
    public class TvMazeScraper
    {
        private readonly ITvMazeApiClient _apiClient;
        private readonly ITvMazeRepository _tvMazeRepository;
        private readonly ILogger<TvMazeScraper> _logger;

        public TvMazeScraper(
            ITvMazeApiClient apiClient,
            ITvMazeRepository tvMazeRepository,
            ILogger<TvMazeScraper> logger
        )
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _tvMazeRepository = tvMazeRepository ?? throw new ArgumentNullException(nameof(tvMazeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            const int PAGE_LIMIT = 99999;

            for (int page = 0; page < PAGE_LIMIT; page++)
            {
               await ProcessPageShows(page);
            }            
        }

        private async Task<bool> ProcessPageShows(int page)
        {
            try
            {
                List<ShowInfo> shows = await _apiClient.GetShowsAsync(page);
                _logger.LogInformation("Received page {0} from the API, got {1} show(s)", page, shows.Count);
                
                foreach (ShowInfo showInfo in shows)
                {
                    int showId = showInfo.Id;

                    try
                    {
                        List<Person> cast = await _apiClient.GetShowCastAsync(showId);
                        _logger.LogInformation("Retrieved the cast for show {0}", showId);

                        await SaveShow(showInfo, cast);
                    }
                    catch (Exception exception)
                    {                       
                        _logger.LogError(exception, "Error when retrieving cast for show {0}", showId);
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error when getting page {0} from the API", page);
                return false;
            }
        }

        private async Task SaveShow(ShowInfo showHeader, List<Person> cast)
        {
            try
            {
                IEnumerable<Cast> castDbos = cast.Select(it => new Cast
                {
                    TvMazeId = it.Id,
                    Name = it.Name,
                    Birthday = it.Birthday
                });

                List<Cast> uniqueCast = castDbos.Distinct(new PersonEqualityComparer()).ToList();

                var showDbo = new Show
                {
                    TvMazeId = showHeader.Id,
                    Name = showHeader.Name,
                    Cast = uniqueCast
                };

                await _tvMazeRepository.AddShow(showDbo);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Cannot save show {0} into the database", showHeader.Id);
            }
        }

        private class PersonEqualityComparer : IEqualityComparer<Cast>
        {
            public bool Equals(Cast x, Cast y)
            {
                return x.TvMazeId == y.TvMazeId;
            }

            public int GetHashCode(Cast obj)
            {
                return obj.TvMazeId;
            }
        }
    }
}
