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
using System.Windows.Controls;

namespace MyToDo1.Property
{
    class ExposureCompComboBox : ComboBox, IObserver
    {
        private ActionSource _actionSource;

        private EDSDKLib.EDSDK.EdsPropertyDesc _desc;

        private readonly Dictionary<uint, string> map = new Dictionary<uint, string>();

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        public ExposureCompComboBox()
        {
            map.Add(0x28, "+5");
            map.Add(0x25, "+4 2/3");
            map.Add(0x24, "+4 1/2");
            map.Add(0x23, "+4 1/3");
            map.Add(0x20, "+4");
            map.Add(0x1D, "+3 2/3");
            map.Add(0x1C, "+3 1/2");
            map.Add(0x1B, "+3 1/3");
            map.Add(0x18, "+3");
            map.Add(0x15, "+2 2/3");
            map.Add(0x14, "+2 1/2");
            map.Add(0x13, "+2 1/3");
            map.Add(0x10, "+2");
            map.Add(0x0d, "+1 2/3");
            map.Add(0x0c, "+1 1/2");
            map.Add(0x0b, "+1 1/3");
            map.Add(0x08, "+1");
            map.Add(0x05, "+2/3");
            map.Add(0x04, "+1/2");
            map.Add(0x03, "+1/3");
            map.Add(0x00, "0");
            map.Add(0xfd, "-1/3");
            map.Add(0xfc, "-1/2");
            map.Add(0xfb, "-2/3");
            map.Add(0xf8, "-1");
            map.Add(0xf5, "-1 1/3");
            map.Add(0xf4, "-1 1/2");
            map.Add(0xf3, "-1 2/3");
            map.Add(0xf0, "-2");
            map.Add(0xed, "-2 1/3");
            map.Add(0xec, "-2 1/2");
            map.Add(0xeb, "-2 2/3");
            map.Add(0xe8, "-3");
            map.Add(0xE5, "-3 1/3");
            map.Add(0xE4, "-3 1/2");
            map.Add(0xE3, "-3 2/3");
            map.Add(0xE0, "-4");
            map.Add(0xDD, "-4 1/3");
            map.Add(0xDC, "-4 1/2");
            map.Add(0xDB, "-4 2/3");
            map.Add(0xD8, "-5");
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
                _actionSource?.FireEvent(ActionEvent.Command.SET_EXPOSURE_COMPENSATION, (nint)key);
            }
        }

        public void Update(Observable from, CameraEvent e)
        {
            CameraModel model = (CameraModel)from;
            CameraEvent.Type eventType = e.GetEventType();

            if (eventType == CameraEvent.Type.PROPERTY_CHANGED || eventType == CameraEvent.Type.PROPERTY_DESC_CHANGED)
            {
                uint propertyID = (uint)e.GetArg();

                if (propertyID == EDSDKLib.EDSDK.PropID_ExposureCompensation)
                {
                    uint property = model.ExposureCompensation;

                    switch (eventType)
                    {
                        case CameraEvent.Type.PROPERTY_CHANGED:
                            UpdateProperty(property);
                            break;

                        case CameraEvent.Type.PROPERTY_DESC_CHANGED:
                            _desc = model.ExposureCompensationDesc;
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
