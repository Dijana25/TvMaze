using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMaze.DataAccess.Models;

namespace TvMaze.DataAccess.Repositories
{
    public class TvMazeRepository
    {
        private readonly TvMazeContext _dbContext;

        public TvMazeRepository(TvMazeContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddShow(Show show)
        {            
            _dbContext.Shows.Add(show);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Show>> GetShowsWithCast(int skip, int take)
        {
            var shows = _dbContext.Shows
                .OrderBy(it => it.Id)
                .Skip(skip)
                .Take(take);

            var showsWithCast =
                from show in shows
                join cast in _dbContext.Cast
                    on show equals cast.Show
                select new { show, cast };

            return await
                showsWithCast
                .OrderBy(it => it.show)                
                .ThenByDescending(it => it.cast.Birthday)
                .GroupBy(it => it.show)
                .Select(group =>
                    new Show
                    {
                        Id = group.Key.Id,
                        TvMazeId = group.Key.TvMazeId,
                        Name = group.Key.Name,
                        Cast = group.Select(it => it.cast).ToList()
                    })
                .ToListAsync();
        }
    }
}
