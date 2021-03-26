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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTK_PSD_Browser.Core.Data
{
    public static class QueryDatabase
    {
        public static readonly string ConnectionString;

        static string _username;
        static int _userId;
        static string _posttypes;

        static QueryDatabase()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            ConnectionString =
                Environment.ExpandEnvironmentVariables(config.GetConnectionString("ELODB"));
        }

        //public static async Task<object> GetScalarAsync(string sql)
        //{
        //    using var connection = new SqlConnection(ConnectionString);
        //    await connection.OpenAsync();
        //    var command = new SqlCommand(sql, connection);
        //    return await command.ExecuteScalarAsync();
        //}

        public static string UserName => _username;
        public static int UserId => _userId;

        public static void SelectUser(string username = null)
        {
            _username = username ?? Environment.GetEnvironmentVariable("USERNAME");
            _userId = 0;
            _posttypes = "";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string sql = $"SELECT usrid FROM elo_users WHERE usrname = '{_username}'";

            var command = new SqlCommand(sql, connection);
            var usrid = command.ExecuteScalar();

            if (usrid == null) return;

            _userId = Decimal.ToInt32((decimal)usrid); //n: ..., 1: User, -1: admininf, -2: adminsys

            sql = "SELECT DISTINCT sp.posttype FROM elo_users_access ua LEFT JOIN elo_spr_post_sub sp ON ua.formname = sp.code " +
                $"WHERE ua.userid = {_userId} AND sp.posttype <> ''";

            command.CommandText = sql;
            var reader = command.ExecuteReader();

            if (!reader.HasRows) return;

            var types = new List<string>();
            while (reader.Read())
            {
                types.Add($"'{reader.GetString(0)}'");
            }

            if (types.Count == 0) return;

            _posttypes = $"WHERE posttype IN ({string.Join(",", types)})";
        }

        public static List<string> PostTitles(int order = 0)
        {
            if (_userId == 0) return null;

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string[] by = { "posttype", "postname" };
            string sql = $"SELECT posttype, postname FROM elo_spr_post {_posttypes} ORDER BY {by[order]}";

            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            var result = new List<string>();
            result.Add("Все формы");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add($"{reader.GetString(0)}: {reader.GetString(1)}");
                }
            }

            return result;
        }

        public static List<List<string>> PostStore(int order = 0)
        {
            //if (_userId == 0) return null;

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string dt = $"WHERE dt BETWEEN '26-MAR-2021' AND '27-MAR-2021'";
            string[] by = { "posttype", "postname" };
            //string sql = $"SELECT * FROM elo_arh_post {_posttypes} AND {dt} ORDER BY {by[order]}";
            string sql = $"SELECT * FROM elo_arh_post {dt} ORDER BY {by[order]}";

            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            var table = new List<List<string>>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var row = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader[i].ToString());
                    }
                    table.Add(row);
                }
            }

            return table;
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
