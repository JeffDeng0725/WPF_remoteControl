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

using MyToDo1;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace
    MyToDo1
{
    class MovieHFRComboBox : ComboBox, IObserver
    {
        private ActionSource _actionSource;
        private EDSDKLib.EDSDK.EdsPropertyDesc _desc;
        private readonly Dictionary<uint, string> map = new Dictionary<uint, string>();

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        public MovieHFRComboBox()
        {
            map.Add(0x0000, "Disable");
            map.Add(0x0001, "Enable");

            foreach (var kvp in map)
            {
                Items.Add(kvp.Value);
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (this.SelectedItem != null)
            {
                int selectedIndex = this.SelectedIndex;
                uint key = (uint)_desc.PropDesc[selectedIndex];
                _actionSource?.FireEvent(ActionEvent.Command.SET_MOVIE_HFR, (IntPtr)key);
            }
        }

        public void Update(Observable from, CameraEvent e)
        {
            CameraModel model = (CameraModel)from;
            CameraEvent.Type eventType = e.GetEventType();

            if (eventType == CameraEvent.Type.PROPERTY_CHANGED || eventType == CameraEvent.Type.PROPERTY_DESC_CHANGED)
            {
                uint propertyID = (uint)e.GetArg();

                if (propertyID == EDSDKLib.EDSDK.PropID_MovieHFRSetting)
                {
                    uint property = model.MovieHFR;
                    // Update property
                    switch (eventType)
                    {
                        case CameraEvent.Type.PROPERTY_CHANGED:
                            this.UpdateProperty(property);
                            break;

                        case CameraEvent.Type.PROPERTY_DESC_CHANGED:
                            _desc = model.MovieHFRDesc;
                            this.UpdatePropertyDesc(ref _desc);
                            this.UpdateProperty(property);
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
                this.SelectedItem = displayValue;
            }
        }

        private void UpdatePropertyDesc(ref EDSDKLib.EDSDK.EdsPropertyDesc desc)
        {
            this.Items.Clear();
            for (int i = 0; i < desc.NumElements; i++)
            {
                string displayValue;
                if (map.TryGetValue((uint)desc.PropDesc[i], out displayValue))
                {
                    this.Items.Add(displayValue);
                }
            }
        }
    }
}

