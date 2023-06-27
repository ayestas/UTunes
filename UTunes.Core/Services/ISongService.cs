using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.Services
{
    public interface ISongService
    {
        Task<OperationResult<Song>> AddAsync(Song song);
        Task<OperationResult<IReadOnlyList<Song>>> GetByAlbum(int albumId);
        OperationResult<Song> GetById(int id);
        List<int> GetPrices(int albumId);

    }
}
