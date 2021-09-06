using Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMaze.DataAccess.Models;
using TvMaze.DataAccess.Repositories;
using TvMaze.ShowCastRestApi.Models;

namespace TvMaze.ShowCastRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly int PAGE_SIZE;

        private readonly ITvMazeRepository _repository;

        public ShowsController(ITvMazeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            PAGE_SIZE = Constants.PAGE_SIZE;
        }

        [HttpGet]
        public async Task<List<ShowModel>> Get(int? page)
        {
            int pageNumber = page ?? 1;
            int skip = (pageNumber - 1) * PAGE_SIZE;

            var shows = await _repository.GetShowsWithCast(skip, PAGE_SIZE);

            return shows.Select(ConvertShow).ToList();
        }

        private ShowModel ConvertShow(Show show)
        {
            List<CastModel> cast = show.Cast.Select(ConvertCast).ToList();

            return new ShowModel(show.TvMazeId, show.Name, cast);
        }

        private CastModel ConvertCast(Cast cast)
        {
            return new CastModel(cast.TvMazeId, cast.Name, cast.Birthday);
        }
    }
}
