using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UTunes.Api.DataTransferObject;
using UTunes.Core.Services;

namespace UTunes.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongsController : UTunesController
    {
        private readonly ISongService songService;
        private readonly IMapper mapper;

        public SongsController(ISongService songService, IAlbumService albumService, IMapper mapper) 
        {
            this.songService = songService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetSongById(int id)
        {
            var result = this.songService.GetById(id);
            var song = this.mapper.Map<SongDetailDataTransferObject>(result.Result);
            return result.Succeeded ? Ok(song) : GetErrorResult<Core.Entities.Song>(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSongAsync(AddSongDataTransferObject song)
        {
            var result = await this.songService.AddAsync(this.mapper.Map<Core.Entities.Song>(song));
            
            var addedSong = this.mapper.Map<SongDetailDataTransferObject>(result.Result);
            return result.Succeeded ? Ok(addedSong) : GetErrorResult<Core.Entities.Song>(result);
        }
    }
}
