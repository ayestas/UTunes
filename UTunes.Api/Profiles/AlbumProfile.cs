using AutoMapper;
using UTunes.Api.DataTransferObject;
using UTunes.Core.Entities;

namespace UTunes.Api.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile() 
        {
            CreateMap<Album, AlbumDetailDataTransferObject>();
        }
    }
}
