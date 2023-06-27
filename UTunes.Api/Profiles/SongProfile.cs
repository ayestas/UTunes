using AutoMapper;
using UTunes.Api.DataTransferObject;

namespace UTunes.Api.Profiles
{
    public class Song : Profile
    {
        public Song() 
        {
            CreateMap<AddSongDataTransferObject, Core.Entities.Song>();
            CreateMap<Core.Entities.Song, SongDetailDataTransferObject>();
        }
    }
}
