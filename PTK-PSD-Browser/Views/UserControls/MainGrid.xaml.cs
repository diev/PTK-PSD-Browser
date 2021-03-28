﻿#region License
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

using System;
using System.Collections.Generic;
using System.Data;
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

namespace PTK_PSD_Browser.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MainGrid.xaml
    /// </summary>
    public partial class MainGrid : UserControl
    {
        public MainGrid()
        {
            InitializeComponent();

            //PostList.ItemsSource = QueryDatabase.PostStore();
            var group = CollectionViewSource.GetDefaultView(PostList.ItemsSource);
            if (group != null && group.CanGroup)
            {
                group.GroupDescriptions.Clear();
                group.GroupDescriptions.Add(new PropertyGroupDescription("Posttype"));
            }
        }
    }
}
