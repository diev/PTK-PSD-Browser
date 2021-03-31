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

using System.Windows.Controls;

namespace PTK_PSD_Browser.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PostViewControl.xaml
    /// </summary>
    public partial class PostViewControl : UserControl
    {
        static int _level = 0;

        public PostViewControl()
        {
            InitializeComponent();
            
            if (_level > 0)
            {
                Title.Text += $" ({_level})";
            }

            _level++;
        }
    }
}
