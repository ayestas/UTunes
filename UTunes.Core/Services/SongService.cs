using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.Services
{
    public class SongService : ISongService
    {
        private readonly IRepository<Song> songRepository;
        private readonly IRepository<Album> albumRepository;

        public SongService(IRepository<Song> songRepository, IRepository<Album> albumRepository) 
        {
            this.songRepository = songRepository;
            this.albumRepository = albumRepository;
        }

        public async Task<OperationResult<Song>> AddAsync(Song song)
        {
            if (string.IsNullOrEmpty(song.Name))
            {
                return new OperationResult<Song>(new Error
                {
                    Code = ErrorCode.InternalError,
                    Message = "Name is a required field to add a song."
                });
            }

            if (string.IsNullOrEmpty(song.Artist))
            {
                return new OperationResult<Song>(new Error
                {
                    Code = ErrorCode.InternalError,
                    Message = "Artist is a required field to add a song."
                });
            }

            var entity = await this.songRepository.AddAsync(song);
            await this.songRepository.CommitAsync();
            return new OperationResult<Song>(entity);
        }

        public async Task<OperationResult<IReadOnlyList<Song>>> GetByAlbum(int albumId)
        {
            if (albumId == -1)
            {
                return (await this.songRepository.AllAsync()).ToList();
            }

            return this.songRepository.Filter(x => x.AlbumId == albumId).ToList();
        }

        public List<int> GetPrices(int albumId)
        {
            var list = this.songRepository.Filter(x => x.AlbumId == albumId).ToList();
            List<int> result = new List<int>();
            foreach(var item in list)
            {
                result.Add(item.Price);
            }
            return result;
        }

        public OperationResult<Song> GetById(int id)
        {
            var song = this.songRepository.GetById(id);
            if (song is null)
            {
                return new OperationResult<Song>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "The song doesn't exist."
                });
            }
            return song;
        }

        
    }
}
