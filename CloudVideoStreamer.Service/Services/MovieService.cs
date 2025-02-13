using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Interfaces.Base;
using CloudVideoStreamer.Service.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace CloudVideoStreamer.Service.Services;

public class MovieService : BaseService<Movie, int>, IMovieService
{
  private readonly IUnitOfWork _unitOfWork;

  public MovieService(IUnitOfWork unitOfWork) : base(unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public new async Task<List<MovieDto>> GetAll()
  {
    var movies = await _unitOfWork.Repository<Movie, int>()
      .GetAll()
      .Include(x => x.MediaContent)
      .Select(x => new MovieDto
      {
        DurationInSeconds = x.DurationInSeconds,
        MediaContent = new MediaContentDto
        {
          Description = x.MediaContent.Description,
          Rating = x.MediaContent.Rating,
          ReleaseDate = x.MediaContent.ReleaseDate,
          Title = x.MediaContent.Title
        }
      })
      .ToListAsync();

    return movies;
  }
}