using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> albumRepository;

        public AlbumService(IRepository<Album> albumRepository) 
        {
            this.albumRepository = albumRepository;
        }
        public async Task<OperationResult<Album>> AddAsync(Album album)
        {
            if (string.IsNullOrEmpty(album.Name))
            {
                return new OperationResult<Album>(new Error
                {
                    Code = ErrorCode.InternalError,
                    Message = "Name is a required field to add a category"
                });
            }
            var entity = await this.albumRepository.AddAsync(album);
            await this.albumRepository.CommitAsync();
            return new OperationResult<Album>(entity);
        }

        public async Task<OperationResult<IReadOnlyList<Album>>> GetAllAsync() => (await this.albumRepository.AllAsync
            ()).ToList();

        //public int GetCount(int albumId)
        //{
        //    var album = this.albumRepository.GetById(albumId);
        //    var result = album.Songs.Count;
        //    return result;
        //}

        public OperationResult<Album> Update(Album album)
        {
            var result = albumRepository.Update(album);
            albumRepository.Commit();
            return result;
        }
    }
}
