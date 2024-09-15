/******************************************************************************
*                                                                             *
*   PROJECT : Eos Digital camera Software Development Kit EDSDK               *
*                                                                             *
*   Description: This is the Sample code to show the usage of EDSDK.          *
*                                                                             *
*                                                                             *
*******************************************************************************
*                                                                             *
*   Written and developed by Canon Inc.                                       *
*   Copyright Canon Inc. 2018 All Rights Reserved                             *
*                                                                             *
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MyToDo1.Property
{
    public class PropertyComboBox : ComboBox
    {
        protected Dictionary<uint, string> map = new Dictionary<uint, string>();

        protected void UpdateProperty(uint value)
        {
            // The character string corresponding to data is acquired. 
            if (map.TryGetValue(value, out string outString) && !outString.Equals("unknown"))
            {
                if (Items.Count == 0)
                {
                    Items.Add(outString);
                }

                SelectedItem = outString;

                if ((string)SelectedItem != outString && ToString().Contains("AeModeComboBox"))
                {
                    Items.Clear();
                    Items.Add(outString);
                    SelectedItem = outString;
                }
            }
        }

        protected void UpdatePropertyDesc(ref EDSDKLib.EDSDK.EdsPropertyDesc desc)
        {
            IsEnabled = desc.NumElements > 1;

            Items.Clear();
            for (int i = 0; i < desc.NumElements; i++)
            {
                // The character string corresponding to data is acquired.
                if (map.TryGetValue((uint)desc.PropDesc[i], out string outString) && !outString.Equals("unknown"))
                {
                    // Create list of combo box
                    Items.Add(outString);
                }
            }
        }
    }
}
