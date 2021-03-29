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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PTK_PSD_Browser.Core.Data
{
    public class ViewModel : BaseObject
    {
        public ObservableCollection<Title> Titles { get; set; } = new();
        public ObservableCollection<Post> Posts { get; set; } = new();

        public User User { get; set; } = new();

        public Title SelectedTitle { get; set; } = new();

        public PostFilter PostFilter { get; set; } = new();

        public string SelectedFilename { get; set; } = "File!";

        //public UserInfo SelectedUserInfo
        //{
        //    get => GetValue<UserInfo>(nameof(SelectedUserInfo));
        //    set => SetValue(nameof(SelectedUserInfo), value);
        //}

        public void SetUser()
        {
            SetUser(Environment.GetEnvironmentVariable("USERNAME"));
        }

        public void SetUser(string username)
        {
            User.Name = username;
            User.Id = 0;
            User.PostTypes = null;

            using var connection = new SqlConnection(QueryDatabase.ConnectionString);
            connection.Open();

            string sql = $"SELECT usrid FROM elo_users WHERE usrname = '{username}'";

            var command = new SqlCommand(sql, connection);
            var usrid = command.ExecuteScalar();

            if (usrid == null) return;

            User.Id = Decimal.ToInt32((decimal)usrid); //n: ..., 1: User, -1: admininf, -2: adminsys

            sql = "SELECT DISTINCT sp.posttype FROM elo_users_access ua LEFT JOIN elo_spr_post_sub sp ON ua.formname = sp.code " +
                $"WHERE ua.userid = {User.Id} AND sp.posttype <> ''";

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
                User.PostTypes = string.Join(",", types);
            }

            SetTitles();
            SetPosts();
        }

        public void SetTitles()
        {
            Titles.Clear();
            if (User.Id == 0) return;

            using var connection = new SqlConnection(QueryDatabase.ConnectionString);
            connection.Open();

            string[] by = { "posttype", "postname" };
            int order = 0;

            string sql = $"SELECT posttype, postname FROM elo_spr_post WHERE posttype IN ({User.PostTypes}) ORDER BY {by[order]}";

            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            Titles.Add(new Title(
                "*",
                "Все формы"
                ));

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Titles.Add(new Title(
                        reader.GetString(0),
                        reader.GetString(1)
                        ));
                }
            }
        }

        public void SetPosts()
        {
            Posts.Clear();
            if (User.Id == 0) return;

            using var connection = new SqlConnection(QueryDatabase.ConnectionString);
            connection.Open();

            string begin = PostFilter.DateBegin.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            string end = PostFilter.DateEnd.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            string dtBetween = $"dt BETWEEN '{begin}' AND '{end}'";
            string sql = $"SELECT filetype, posttype, dt, filename, state_, bik, error_ FROM elo_arh_post WHERE {dtBetween} AND ";
            if (SelectedTitle.PostType.Equals("*"))
            {
                sql += $"posttype IN ({User.PostTypes})";
            }
            else
            {
                sql += $"posttype = '{SelectedTitle.PostType}'";
            }    

            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Posts.Add(new Post(
                        (string)reader["FILETYPE"],
                        (string)reader["POSTTYPE"],
                        (DateTime)reader["DT"],
                        (string)reader["FILENAME"],
                        Decimal.ToInt32((decimal)reader["STATE_"]),
                        Decimal.ToInt32((decimal)reader["BIK"]),
                        Decimal.ToInt32((decimal)reader["ERROR_"]) //null?
                        ));
                }
            }
        }
    }
}
