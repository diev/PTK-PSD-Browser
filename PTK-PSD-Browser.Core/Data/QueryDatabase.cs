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

using PTK_PSD_Browser.Core.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace PTK_PSD_Browser.Core.Data
{
    public static class QueryDatabase
    {
        private static readonly string _connectionString;
        private static readonly UserObject _userObject = new();

        static QueryDatabase()
        {
            //var builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            //builder.AddJsonFile("appsettings.json");

            //var config = builder.Build();
            //ConnectionString =
            //    Environment.ExpandEnvironmentVariables(config.GetConnectionString("ELODB"));

            string elodb = "";
            string file = "appsettings.json";
            if (File.Exists(file))
            {
                string json = File.ReadAllText("appsettings.json");
                using var doc = JsonDocument.Parse(json);

                elodb = doc.RootElement.GetProperty("ELODB").GetString();
            }
            _connectionString = Environment.ExpandEnvironmentVariables(elodb);
        }

        public static string UserName
        {
            get => _userObject.Name;
            set
            {
                _userObject.Name = value;

                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string sql = $"SELECT usrid FROM elo_users WHERE usrname = '{_userObject.Name}'";

                var command = new SqlCommand(sql, connection);
                var usrid = command.ExecuteScalar();

                if (usrid == null) return;

                _userObject.Id = decimal.ToInt32((decimal)usrid); //n: ..., 1: User, -1: admininf, -2: adminsys

                sql = "SELECT DISTINCT sp.posttype FROM elo_users_access ua LEFT JOIN elo_spr_post_sub sp ON ua.formname = sp.code " +
                    $"WHERE ua.userid = {_userObject.Id} AND sp.posttype <> ''";

                command.CommandText = sql;
                var reader = command.ExecuteReader();

                if (!reader.HasRows) return;

                var types = new List<string>();
                while (reader.Read())
                {
                    types.Add($"'{reader.GetString(0)}'");
                }

                if (types.Count > 0)
                {
                    _userObject.PostTypes = string.Join(",", types);
                }
            }
        }

        public static ObservableCollection<TitleObject> GetTitleObjects(int order = 0)
        {
            var titles = new ObservableCollection<TitleObject>();
            if (_userObject.Id != 0)
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string[] by = { "posttype", "postname" };

                string sql = $"SELECT posttype, postname FROM elo_spr_post WHERE posttype IN ({_userObject.PostTypes}) ORDER BY {by[order]}";

                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                titles.Add(new TitleObject()); //*

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        titles.Add(new TitleObject(
                            reader.GetString(0),
                            reader.GetString(1)
                            ));
                    }
                }
            }
            return titles;
        }

        public static ObservableCollection<PostObject> GetPostObjects(PostFilterObject filter)
        {
            var posts = new ObservableCollection<PostObject>();
            if (_userObject.Id != 0)
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string begin = filter.DateBegin.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                string end = filter.DateEnd.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                string dtBetween = $"dt BETWEEN '{begin}' AND '{end}'";

                string sql = $"SELECT filetype, posttype, dt, filename, state_, error_ FROM elo_arh_post WHERE {dtBetween} AND ";

                if (filter.PostType.Equals("*"))
                {
                    sql += $"posttype IN ({_userObject.PostTypes})";
                }
                else
                {
                    sql += $"posttype = '{filter.PostType}'";
                }

                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        posts.Add(new PostObject(
                            (string)reader["FILETYPE"],
                            (string)reader["POSTTYPE"],
                            (DateTime)reader["DT"],
                            (string)reader["FILENAME"],
                            decimal.ToInt32((decimal)reader["STATE_"]),
                            decimal.ToInt32((decimal)reader["ERROR_"]) //null?
                            ));
                    }
                }
            }
            return posts;
        }

        public static void SetPostObjects(ObservableCollection<PostObject> posts, PostFilterObject filter)
        {
            posts.Clear();
            if (_userObject.Id != 0)
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string begin = filter.DateBegin.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                string end = filter.DateEnd.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                string dtBetween = $"dt BETWEEN '{begin}' AND '{end}'";

                string sql = $"SELECT filetype, posttype, dt, filename, state_, error_ FROM elo_arh_post WHERE {dtBetween} AND ";

                if (filter.PostType.Equals("*"))
                {
                    sql += $"posttype IN ({_userObject.PostTypes})";
                }
                else
                {
                    sql += $"posttype = '{filter.PostType}'";
                }

                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        posts.Add(new PostObject(
                            (string)reader["FILETYPE"],
                            (string)reader["POSTTYPE"],
                            (DateTime)reader["DT"],
                            (string)reader["FILENAME"],
                            decimal.ToInt32((decimal)reader["STATE_"]),
                            decimal.ToInt32((decimal)reader["ERROR_"]) //null?
                            ));
                    }
                }
            }
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
