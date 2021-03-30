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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTK_PSD_Browser.Core.Models
{
    public class Title : BaseObject
    {
        public Title()
        {
            PostType = "*";
            PostName = "Все Формы";
        }

        public Title(string postType, string postName)
        {
            PostType = postType;
            PostName = postName;
        }

        public string PostType { get; set; }
        public string PostName { get; set; }
        public string PostDescription => $"{PostType}: {PostName}";

        //public string ComboName => $"{PostType}: {PostName}";
    }

    //public partial class EloSprPost
    //{
    //    public string Posttype { get; set; }
    //    public string Postname { get; set; }
    //    public decimal? Enable { get; set; }
    //    public decimal? Dtype { get; set; }
    //    public string KodOt { get; set; }
    //    public decimal? PathId { get; set; }
    //    public string VerElo { get; set; }
    //    public decimal? Datatype { get; set; }
    //    public decimal? SetEcpAuto { get; set; }
    //    public decimal? PathSet { get; set; }
    //}
}
