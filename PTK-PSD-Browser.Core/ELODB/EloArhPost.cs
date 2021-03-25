using System;

#nullable disable

namespace PTK_PSD_Browser.Core.ELODB
{
    public partial class EloArhPost
    {
        public string Filetype { get; set; }
        public string Posttype { get; set; }
        public DateTime Dt { get; set; }
        public string Filename { get; set; }
        public string Pathname { get; set; }
        public decimal State { get; set; }
        public decimal Bik { get; set; }
        public decimal Nkod { get; set; }
        public decimal? Error { get; set; }
        public DateTime? Repdate { get; set; }
        public string AddInfo { get; set; }
        public decimal? Katype { get; set; }
        public string FesType { get; set; }
        public string Guid { get; set; }
        public DateTime? DtCreate { get; set; }
        public string OnGuid { get; set; }
    }
}
