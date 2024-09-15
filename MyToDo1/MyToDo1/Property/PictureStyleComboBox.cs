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
    class PictureStyleComboBox : ComboBox, IObserver
    {
        private ActionSource _actionSource;
        private EDSDKLib.EDSDK.EdsPropertyDesc _desc;
        private readonly Dictionary<uint, string> map = new Dictionary<uint, string>();

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        public PictureStyleComboBox()
        {
            map.Add(0x0081, "Standard");
            map.Add(0x0082, "Portrait");
            map.Add(0x0083, "Landscape");
            map.Add(0x0084, "Neutral");
            map.Add(0x0085, "Faithful");
            map.Add(0x0086, "Monochrome");
            map.Add(0x0087, "Auto");
            map.Add(0x0088, "FineDetail");
            map.Add(0X0021, "User Def. 1");
            map.Add(0X0022, "User Def. 2");
            map.Add(0X0023, "User Def. 3");
            map.Add(0x0041, "PC1");
            map.Add(0x0042, "PC2");
            map.Add(0x0043, "PC3");

            // Populate the ComboBox with these options
            foreach (var kvp in map)
            {
                Items.Add(kvp.Value);
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (SelectedItem != null && _desc.PropDesc != null)
            {
                int selectedIndex = SelectedIndex;
                uint key = (uint)_desc.PropDesc[selectedIndex];

                // Fire the event to set the picture style
                _actionSource?.FireEvent(ActionEvent.Command.SET_PICTURESTYLE, (nint)key);
            }
        }

        public void Update(Observable from, CameraEvent e)
        {
            CameraModel model = (CameraModel)from;
            CameraEvent.Type eventType = e.GetEventType();

            if (eventType == CameraEvent.Type.PROPERTY_CHANGED || eventType == CameraEvent.Type.PROPERTY_DESC_CHANGED)
            {
                uint propertyID = (uint)e.GetArg();

                if (propertyID == EDSDKLib.EDSDK.PropID_PictureStyle)
                {
                    uint property = model.PictureStyle;
                    switch (eventType)
                    {
                        case CameraEvent.Type.PROPERTY_CHANGED:
                            UpdateProperty(property);
                            break;

                        case CameraEvent.Type.PROPERTY_DESC_CHANGED:
                            _desc = model.PictureStyleDesc;
                            UpdatePropertyDesc(ref _desc);
                            UpdateProperty(property);
                            break;
                    }
                }
            }
        }

        private void UpdateProperty(uint value)
        {
            if (map.TryGetValue(value, out string displayValue))
            {
                SelectedItem = displayValue;
            }
        }

        private void UpdatePropertyDesc(ref EDSDKLib.EDSDK.EdsPropertyDesc desc)
        {
            Items.Clear();
            for (int i = 0; i < desc.NumElements; i++)
            {
                if (map.TryGetValue((uint)desc.PropDesc[i], out string displayValue))
                {
                    Items.Add(displayValue);
                }
            }
        }
    }
}
