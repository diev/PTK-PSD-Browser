using System;

#nullable disable

namespace PTK_PSD_Browser.Core.ELODB
{
    public partial class EloFrmStatus
    {
        public string Frmname { get; set; }
        public DateTime Tdate { get; set; }
        public decimal Frmstatus { get; set; }
        public decimal Bik { get; set; }
        public decimal? Poststatus { get; set; }
        public DateTime? Mdate { get; set; }
    }
}
