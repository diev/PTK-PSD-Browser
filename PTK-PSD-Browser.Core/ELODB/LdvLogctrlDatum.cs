using System;

#nullable disable

namespace PTK_PSD_Browser.Core.ELODB
{
    public partial class LdvLogctrlDatum
    {
        public string Ccode { get; set; }
        public string Scode { get; set; }
        public string Sdesc { get; set; }
        public string Form { get; set; }
        public DateTime BDate { get; set; }
        public string AddInfo { get; set; }
        public decimal? Deactivate { get; set; }
        public decimal? Calculate { get; set; }
    }
}
