using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Services {
  public class MediaContentService : BaseService<MediaContent, int>, IMediaContentService {

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MediaContentService> _logger;

    public MediaContentService(IUnitOfWork unitOfWork, ILogger<MediaContentService> logger) : base(unitOfWork) 
    {
      _unitOfWork = unitOfWork;
      _logger = logger;
    }

    public async Task<List<MediaContentDto>> GetFiltered(MediaContentFilterDto filter)
    {
      try
      {
        var query = _unitOfWork.Repository<MediaContent, int>()
        .GetAll()
        .AsNoTracking();
        
        if (filter.FilterMovies)
          query = query.Include(x => x.Movies);

        if (filter.FilterTvSeries)
          query = query.Include(x => x.TvSeries);

        if (!string.IsNullOrEmpty(filter.Title))
          query = query.Where(x => x.Title.Contains(filter.Title));

        if (filter.ReleaseDateFrom.HasValue)
          query = query.Where(x => x.ReleaseDate >= filter.ReleaseDateFrom);

        if (filter.ReleaseDateTo.HasValue)
          query = query.Where(x => x.ReleaseDate <= filter.ReleaseDateTo);

        if (filter.RatingFrom.HasValue)
          query = query.Where(x => x.Rating >= filter.RatingFrom);

        if (filter.RatingTo.HasValue)
          query = query.Where(x => x.Rating <= filter.RatingTo);

        if (filter.DurationInSecondsFrom.HasValue)
          query = query.Where(x =>
          x.Movies.Any(x => x.DurationInSeconds >= filter.DurationInSecondsFrom));

        if (filter.DurationInSecondsTo.HasValue)
          query = query.Where(x =>
          x.Movies.Any(x => x.DurationInSeconds <= filter.DurationInSecondsTo));

        if (filter.SeasonCountFrom.HasValue)
          query = query.Where(x =>
          x.TvSeries.Any(x => x.SeasonCount >= filter.SeasonCountFrom));

        if (filter.SeasonCountTo.HasValue)
          query = query.Where(x =>
          x.TvSeries.Any(x => x.SeasonCount <= filter.SeasonCountTo));

        if (filter.Genres != null)
          query = query.Where(x =>
          x.Genres.Any(x => filter.Genres.Contains(x.Genre.Name)));
        
        var result = await query.Select(x => new MediaContentDto()
        {
          Title = x.Title,
          Description = x.Description,
          ReleaseDate = x.ReleaseDate,
          Rating = x.Rating
        }).ToListAsync();

        return result;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        throw;
      }
    }
  }
}
