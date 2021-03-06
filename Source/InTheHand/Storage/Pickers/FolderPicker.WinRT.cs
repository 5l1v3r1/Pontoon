﻿//-----------------------------------------------------------------------
// <copyright file="FolderPicker.WinRT.cs" company="In The Hand Ltd">
//     Copyright © 2016-17 In The Hand Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace InTheHand.Storage.Pickers
{
    partial class FolderPicker
    {
        private Windows.Storage.Pickers.FolderPicker _picker;

        public FolderPicker()
        {
            _picker = new Windows.Storage.Pickers.FolderPicker();
        }

        private async Task<StorageFolder> DoPickSingleFolderAsync()
        {
            return await _picker.PickSingleFolderAsync();
        }
    }
}