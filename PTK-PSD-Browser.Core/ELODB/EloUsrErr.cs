using System;

#nullable disable

namespace PTK_PSD_Browser.Core.ELODB
{
    public partial class EloUsrErr
    {
        public decimal Userid { get; set; }
        public DateTime Time { get; set; }
        public string Modul { get; set; }
        public decimal Errcod { get; set; }
        public string Errtext { get; set; }
    }
}
