using UTunes.Core.Entities;

namespace UTunes.Api.DataTransferObject
{
    public class AddSongDataTransferObject
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public int Price { get; set; }
        public int AlbumId { get; set; }
    }
}
