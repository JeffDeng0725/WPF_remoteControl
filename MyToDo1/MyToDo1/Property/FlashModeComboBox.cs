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
    class FlashModeComboBox : ComboBox, IObserver
    {
        private ActionSource _actionSource;
        private EDSDKLib.EDSDK.EdsPropertyDesc _desc;
        private readonly Dictionary<uint, string> map = new Dictionary<uint, string>();

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        public FlashModeComboBox()
        {
            map.Add(0, "Auto");
            map.Add(1, "On");
            map.Add(2, "Slow Synchro");
            map.Add(3, "Off");

            foreach (var kvp in map)
            {
                Items.Add(kvp.Value);
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (SelectedItem != null)
            {
                int selectedIndex = SelectedIndex;
                uint key = (uint)_desc.PropDesc[selectedIndex];
                _actionSource?.FireEvent(ActionEvent.Command.SET_FLASH_MODE, (nint)key);
            }
        }

        public void Update(Observable from, CameraEvent e)
        {
            CameraModel model = (CameraModel)from;
            CameraEvent.Type eventType = e.GetEventType();

            if (eventType == CameraEvent.Type.PROPERTY_CHANGED || eventType == CameraEvent.Type.PROPERTY_DESC_CHANGED)
            {
                uint propertyID = (uint)e.GetArg();

                if (propertyID == EDSDKLib.EDSDK.PropID_DC_Strobe)
                {
                    uint property = model.FlashMode;

                    switch (eventType)
                    {
                        case CameraEvent.Type.PROPERTY_CHANGED:
                            UpdateProperty(property);
                            break;

                        case CameraEvent.Type.PROPERTY_DESC_CHANGED:
                            _desc = model.FlashModeDesc;
                            UpdatePropertyDesc(ref _desc);
                            UpdateProperty(property);
                            break;
                    }
                }
            }
        }

        private void UpdateProperty(uint value)
        {
            string displayValue;
            if (map.TryGetValue(value, out displayValue))
            {
                SelectedItem = displayValue;
            }
        }

        private void UpdatePropertyDesc(ref EDSDKLib.EDSDK.EdsPropertyDesc desc)
        {
            Items.Clear();
            for (int i = 0; i < desc.NumElements; i++)
            {
                string displayValue;
                if (map.TryGetValue((uint)desc.PropDesc[i], out displayValue))
                {
                    Items.Add(displayValue);
                }
            }
        }
    }
}
