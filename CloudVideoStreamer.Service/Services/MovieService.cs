﻿using CloudVideoStreamer.Repository.DTOs.MediaContent;
using CloudVideoStreamer.Repository.DTOs.Movie;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Interfaces.Base;
using CloudVideoStreamer.Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudVideoStreamer.Service.Services;

public class MovieService : BaseService<Movie, int>, IMovieService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ILogger<MovieService> _logger;

  public MovieService(IUnitOfWork unitOfWork, ILogger<MovieService> logger) : base(unitOfWork)
  {
    _unitOfWork = unitOfWork;
    _logger = logger;
  }

  public async Task<List<MovieDto>> GetAll()
  {
    var movies = await _unitOfWork.Repository<Movie, int>()
      .GetAll()
      .Include(x => x.MediaContent)
      .Select(x => new MovieDto
      {
        Id = x.Id,
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

  public async Task Add(CreateMovieDto model) 
  {
    var mediaContent = _unitOfWork.Repository<MediaContent, int>().Get(model.MediaContentId).FirstOrDefault();
    if (mediaContent == null) 
    {
      _logger.LogError("Media Content not found");
      throw new KeyNotFoundException("Media Content not found");
    }

    var movie = new Movie() {
      DurationInSeconds = model.DurationInSeconds,
      MediaContentId = model.MediaContentId,
      MediaContent = mediaContent
    };

    _unitOfWork.Repository<Movie, int>().Add(movie);

    await _unitOfWork.SaveChangesAsync();
  }

  public async Task Update(UpdateMovieDto model) {
    var mediaContent = _unitOfWork.Repository<MediaContent, int>().Get(model.MediaContentId).FirstOrDefault();
    if (mediaContent == null) 
    {
      _logger.LogError("Media Content not found");
      throw new KeyNotFoundException("Media Content not found");
    }

    var movie = new Movie() 
    {
      Id = model.Id,
      DurationInSeconds = model.DurationInSeconds,
      MediaContentId = model.MediaContentId,
      MediaContent = mediaContent
    };

    _unitOfWork.Repository<Movie, int>().Update(movie);

    await _unitOfWork.SaveChangesAsync();
  }
}