using System;

#nullable disable

namespace PTK_PSD_Browser.Core.ELODB
{
    public partial class EloUser
    {
        public string Usrname { get; set; }
        public string Usrpwd { get; set; }
        public decimal Usrid { get; set; }
        public DateTime? Date { get; set; }
        public decimal? IbdArx { get; set; }
    }
}
