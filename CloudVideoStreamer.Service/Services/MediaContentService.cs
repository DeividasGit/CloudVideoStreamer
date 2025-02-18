using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Services.Base;
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
  }
}
