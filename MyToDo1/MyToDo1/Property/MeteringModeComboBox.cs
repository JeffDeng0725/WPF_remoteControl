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
    class MeteringModeComboBox : ComboBox, IObserver
    {
        private ActionSource _actionSource;
        private EDSDKLib.EDSDK.EdsPropertyDesc _desc;
        private readonly Dictionary<uint, string> map = new Dictionary<uint, string>();

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        public MeteringModeComboBox()
        {
            map.Add(1, "Spot metering");
            map.Add(3, "Evaluative metering");
            map.Add(4, "Partial metering");
            map.Add(5, "Center-weighted average");
            map.Add(0xffffffff, "unknown");

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
                _actionSource?.FireEvent(ActionEvent.Command.SET_METERING_MODE, (nint)key);
            }
        }

        public void Update(Observable from, CameraEvent e)
        {
            CameraModel model = (CameraModel)from;
            CameraEvent.Type eventType = e.GetEventType();

            if (eventType == CameraEvent.Type.PROPERTY_CHANGED || eventType == CameraEvent.Type.PROPERTY_DESC_CHANGED)
            {
                uint propertyID = (uint)e.GetArg();

                if (propertyID == EDSDKLib.EDSDK.PropID_MeteringMode)
                {
                    uint property = model.MeteringMode;
                    // Update property
                    switch (eventType)
                    {
                        case CameraEvent.Type.PROPERTY_CHANGED:
                            UpdateProperty(property);
                            break;

                        case CameraEvent.Type.PROPERTY_DESC_CHANGED:
                            _desc = model.MeteringModeDesc;
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
