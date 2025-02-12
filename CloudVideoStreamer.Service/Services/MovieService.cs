using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Interfaces.Base;
using CloudVideoStreamer.Service.Services.Base;

namespace CloudVideoStreamer.Service.Services;

public class MovieService : BaseService<Movie, int>, IMovieService
{
  private readonly IUnitOfWork _unitOfWork;

  public MovieService(IUnitOfWork unitOfWork) : base(unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
}