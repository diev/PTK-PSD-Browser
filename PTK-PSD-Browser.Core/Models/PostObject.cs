#region License
//------------------------------------------------------------------------------
// Copyright (c) Dmitrii Evdokimov
// Source https://github.com/diev/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//------------------------------------------------------------------------------
#endregion

using System;

namespace PTK_PSD_Browser.Core.Models
{
    public class PostObject : BaseObject
    {
        public PostObject() { }

        public PostObject(string filetype, string posttype, DateTime dt, string filename, int state, int error)
        {
            Filetype = filetype;
            Posttype = posttype;
            Dt = dt;
            Filename = filename;
            State = state;
            Error = error;
        }

        public string Filetype { get; set; }
        public string Posttype { get; set; }
        public DateTime Dt { get; set; }
        public string Filename { get; set; }
        public int State { get; set; }
        public int Error { get; set; }

        public string Arrow => State == 6
            ? "\u00DB" //6 <<
            : "\u00DC"; //7 >>
    }

    //public partial class EloArhPost
    //{
    //    public string Filetype { get; set; }
    //    public string Posttype { get; set; }
    //    public DateTime Dt { get; set; }
    //    public string Filename { get; set; }
    //    public string Pathname { get; set; }
    //    public decimal State { get; set; }
    //    public decimal Bik { get; set; }
    //    public decimal Nkod { get; set; }
    //    public decimal? Error { get; set; }
    //    public DateTime? Repdate { get; set; }
    //    public string AddInfo { get; set; }
    //    public decimal? Katype { get; set; }
    //    public string FesType { get; set; }
    //    public string Guid { get; set; }
    //    public DateTime? DtCreate { get; set; }
    //    public string OnGuid { get; set; }
    //}
}
