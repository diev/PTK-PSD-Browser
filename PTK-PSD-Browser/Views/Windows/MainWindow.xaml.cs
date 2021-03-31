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

using PTK_PSD_Browser.Core.Data;
using PTK_PSD_Browser.Core.ViewModels;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PTK_PSD_Browser.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Period_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime begin = now, end = now;

            var name = (e.Source as FrameworkElement).Name;
            switch (name)
            {
                case "DateNow":
                    {
                        break;
                    }
                case "Date1Day":
                    {
                        begin = now.AddDays(-1);
                        end = begin;
                        break;
                    }
                case "Date4Days":
                    {
                        begin = now.AddDays(-4);
                        end = now;
                        break;
                    }
                case "Date1Week":
                    {
                        begin = now.AddDays(-7);
                        end = now;
                        break;
                    }
            }
            DateBegin.Text = begin.ToString("dd.MM.yyyy");
            DateEnd.Text = end.ToString("dd.MM.yyyy");

            bool date1 = begin == end;

            Date1.IsChecked = date1;
            Date2.IsChecked = !date1;
        }

        private void DateSwitch_Click(object sender, RoutedEventArgs e)
        {
            var name = (e.Source as FrameworkElement).Name;
            bool date1 = name.Equals(nameof(Date1));

            Date1.IsChecked = date1;
            Date2.IsChecked = !date1;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            if (Status == null) return; //?

            Status.Text = "Запрос...";
            Cursor = Cursors.Wait;

            var ctx = (MainViewModel)DataContext;
            QueryDatabase.SetPostObjects(ctx.PostObjects, ctx.PostFilterObject);

            Status.Text = $"Итого {ctx.PostObjects.Count}";
            Cursor = Cursors.Arrow;
        }

        private void PostList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PostView.Title.Text = (string)PostList.SelectedValue;
        }

        private void TitleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshList();
        }
    }
}
