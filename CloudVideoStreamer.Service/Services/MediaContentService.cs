using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Services {
  public class MediaContentService : BaseService<MediaContent, int>, IMediaContentService {

    private readonly IUnitOfWork _unitOfWork;

    public MediaContentService(IUnitOfWork unitOfWork) : base(unitOfWork) 
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<List<MediaContentDto>> GetFiltered(MediaContentFilterDto filter)
    {
      var query = _unitOfWork.Repository<MediaContent, int>()
        .GetAll();

      if(filter.FilterMovies)
        query = query.Include(x => x.Movies);

      if(filter.FilterTvSeries)
        query = query.Include(x=> x.TvSeries);

      if (!string.IsNullOrEmpty(filter.Title))
        query = query.Where(x => x.Title.Contains(filter.Title));

      if(filter.ReleaseDateFrom.HasValue)
        query = query.Where(x => x.ReleaseDate.CompareTo(filter.ReleaseDateFrom) > 0);

      if(filter.ReleaseDateTo.HasValue)
        query = query.Where(x => x.ReleaseDate.CompareTo(filter.ReleaseDateTo) < 0);

      if(filter.RatingFrom.HasValue)
        query = query.Where(x => x.Rating.CompareTo(filter.RatingFrom) > 0);

      if(filter.RatingTo.HasValue)
        query = query.Where(x => x.Rating.CompareTo(filter.RatingTo) < 0);

      if (filter.DurationInSecondsFrom.HasValue)
        query = query.Where(x => 
        x.Movies.Any(x => x.DurationInSeconds.CompareTo(filter.DurationInSecondsFrom) > 0));

      if (filter.DurationInSecondsTo.HasValue)
        query = query.Where(x =>
        x.Movies.Any(x => x.DurationInSeconds.CompareTo(filter.DurationInSecondsTo) < 0));

      if(filter.SeasonCountFrom.HasValue)
        query = query.Where(x =>
        x.TvSeries.Any(x => x.SeasonCount.CompareTo(filter.SeasonCountFrom) > 0));

      if (filter.SeasonCountTo.HasValue)
        query = query.Where(x =>
        x.TvSeries.Any(x => x.SeasonCount.CompareTo(filter.SeasonCountTo) < 0));

      if (filter.Genres != null)
      {
        var genreNames = filter.Genres.Select(x => x.Name).ToList();

        query = query.Where(x =>
        x.Genres.Any(x => genreNames.Contains(x.Genre.Name)));
      }

      var result = await query.Select(x => new MediaContentDto()
      {
        Title = x.Title,
        Description = x.Description,
        ReleaseDate = x.ReleaseDate,
        Rating = x.Rating
      }).ToListAsync();

      return result;
    }
  }
}
