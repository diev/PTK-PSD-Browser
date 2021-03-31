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

using System.Collections.ObjectModel;

namespace PTK_PSD_Browser.Core.ViewModels
{
    public class MainViewModel : BaseObject
    {
        //public IReadOnlyList<string> Arguments { get; }

        public string UserName { get; private set; }

        public ObservableCollection<TitleObject> TitleObjects { get; set; }
        public PostFilterObject PostFilterObject { get; set; } = new();

        public ObservableCollection<PostObject> PostObjects { get; set; }
        public string FileName { get; set; }

        public MainViewModel()
        {
            UserName = QueryDatabase.UserName;
            TitleObjects = QueryDatabase.GetTitleObjects();
            PostObjects = QueryDatabase.GetPostObjects(PostFilterObject);
        }

        //public MainViewModel(IReadOnlyList<string> arguments)
        //{
        //    Arguments = arguments;
        //}
    }
}
