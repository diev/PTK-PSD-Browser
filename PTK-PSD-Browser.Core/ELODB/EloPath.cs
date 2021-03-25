#nullable disable

namespace PTK_PSD_Browser.Core.ELODB
{
    public partial class EloPath
    {
        public decimal PathId { get; set; }
        public string PathIn { get; set; }
        public string PathOut { get; set; }
        public decimal? Enable { get; set; }
        public string Ecp { get; set; }
        public string PathDesc { get; set; }
    }
}
