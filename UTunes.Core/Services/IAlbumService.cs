using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.Services
{
    public interface IAlbumService
    {
        Task<OperationResult<Album>> AddAsync(Album album);

        Task<OperationResult<IReadOnlyList<Album>>> GetAllAsync();

        OperationResult<Album> Update(Album album);

        //int GetCount(int albumId);
    }
}
