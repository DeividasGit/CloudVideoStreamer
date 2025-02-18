using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces.Base;

namespace CloudVideoStreamer.Service.Interfaces;

public interface IMovieService : IBaseService<Movie, int>
{
  Task<List<MovieDto>> GetAll();
  Task Add(CreateMovieDto model);
  Task Update(UpdateMovieDto model);
}