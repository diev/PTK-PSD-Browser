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

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using PTK_PSD_Browser.Core.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTK_PSD_Browser.Core.Data
{
    public static class QueryDatabase
    {
        public static readonly string ConnectionString;

        static QueryDatabase()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            ConnectionString =
                Environment.ExpandEnvironmentVariables(config.GetConnectionString("ELODB"));
        }

        //public static bool UserExists(string username)
        //{
        //    return UserId(username) != 0;
        //}

        //public static decimal UserId(string username)
        //{
        //    using var db = new ELODBContext();
        //    foreach (var user in db.EloUsers)
        //    {
        //        if (user.Usrname.Equals(username, StringComparison.OrdinalIgnoreCase))
        //        {
        //            return user.Usrid;
        //        }
        //    }
        //    return 0; //..., 1: User, -1: admininf, -2: adminsys
        //}
    }
}
