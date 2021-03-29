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
using PTK_PSD_Browser.Core.Models;
using PTK_PSD_Browser.Views.UserControls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            Status.Text = "Запрос...";
            Cursor = Cursors.Wait;

            DataContext = new ViewModel();
            var db = (ViewModel)DataContext;

            db.SetUser("evdokimov");
            TitleList.SelectedIndex = 0;

            Status.Text = "Готово";
            Cursor = Cursors.Arrow;
        }

        private void Period_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime begin = now, end = now;

            DateBegin.DisplayDateEnd = now;
            DateEnd.DisplayDateEnd = now;

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

            DateEnd.Visibility = date1
                ? Visibility.Hidden
                : Visibility.Visible;

            Date1.IsChecked = date1;
            Date2.IsChecked = !date1;
        }

        private void DateSwitch_Click(object sender, RoutedEventArgs e)
        {
            var name = (e.Source as FrameworkElement).Name;
            bool date1 = name.Equals(nameof(Date1));

            DateEnd.Visibility = date1
                ? Visibility.Hidden
                : Visibility.Visible;

            Date1.IsChecked = date1;
            Date2.IsChecked = !date1;
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var control = (DatePicker)sender;
            if (control.SelectedDate != null)
            {
                var db = (ViewModel)DataContext;
                var date = (DateTime)control.SelectedDate;

                if (control.Name.Equals(nameof(DateBegin)))
                {
                    db.PostFilter.DateBegin = date;
                }

                if (control.Name.Equals(nameof(DateEnd)))
                {
                    db.PostFilter.DateEnd = date;
                }

                RefreshList();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            Status.Text = "Запрос...";
            Cursor = Cursors.Wait;

            var db = (ViewModel)DataContext;
            db.SetPosts();

            Status.Text = "Готово";
            Cursor = Cursors.Arrow;
        }

        private void TitleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var control = (ComboBox)sender;
            if (control.SelectedIndex != -1)
            {
                var item = ((sender as ComboBox).SelectedItem as Title);

                var db = (ViewModel)DataContext;
                db.SelectedTitle = item;

                RefreshList();
            }
        }
    }
}
