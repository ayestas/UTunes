using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UTunes.Api.DataTransferObject;
using UTunes.Core;
using UTunes.Core.Entities;
using UTunes.Core.Services;

namespace UTunes.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : UTunesController
{
    private readonly IAlbumService albumService;
    private readonly ISongService songService;
    private readonly IMapper mapper;

    public AlbumsController(IAlbumService albumService, ISongService songService, IMapper mapper)
    {
        this.albumService = albumService;
        this.songService = songService;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAlbumsAsync()
    {
        var result = await this.albumService.GetAllAsync();
        var albums = this.mapper.Map<IList<AlbumDetailDataTransferObject>>(result.Result);
        return result.Succeeded ? Ok(albums) : GetErrorResult<IReadOnlyList<Core.Entities.Album>>(result);
    }

    //[HttpGet("{albumId}")]
    //public ActionResult<int> GetCount(int albumId)
    //{
    //    return albumService.GetCount(albumId);
    //}

    [HttpGet("{albumId}/songs")]
    public async Task<IActionResult> GetSongsByAlbumAsync(int albumId)
    {
        var result = await this.songService.GetByAlbum(albumId);
        var songs = this.mapper.Map<IList<SongDetailDataTransferObject>>(result.Result);
        return result.Succeeded ? Ok(songs) : GetErrorResult<IReadOnlyList<Core.Entities.Song>>(result);
    }


    [HttpPost]
    public async Task<IActionResult> AddAlbumAsync(AddAlbumDataTransferObject album)
    {
        var result = await this.albumService.AddAsync(new Core.Entities.Album
        {
            Name = album.Name,
            Artist = album.Artist,
            Review = album.Review,

        });
        return result.Succeeded ? Ok(result.Result) : GetErrorResult<Core.Entities.Album>(result);
    }

    [HttpPut("{id}")]
    public ActionResult<AlbumDetailDataTransferObject> UpdatePrice(int id, AddAlbumDataTransferObject album)
    {
        var updated = albumService.Update(new Album
        {
            Id = id,
            Name = album.Name,
            Artist = album.Artist,
            Review = album.Review,
            Price = songService.GetPrices(id).Sum(x => Convert.ToInt32(x)),
        });

        return Ok(new AlbumDetailDataTransferObject
        {
            Id = updated.Result.Id,
            Name = updated.Result.Name,
            Artist = updated.Result.Artist,
            Review = updated.Result.Review,
            Price = updated.Result.Price
        });
    }

}

