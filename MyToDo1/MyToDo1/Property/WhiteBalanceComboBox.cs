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
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace MyToDo1.Property
{
    class WhiteBalanceComboBox : ComboBox, IObserver
    {
        private ActionSource _actionSource;
        private EDSDKLib.EDSDK.EdsPropertyDesc _desc;

        private readonly Dictionary<uint, string> map = new Dictionary<uint, string>();

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        public WhiteBalanceComboBox()
        {
            map.Add(0, "Auto: Ambience priority");
            map.Add(1, "Daylight");
            map.Add(2, "Cloudy");
            map.Add(3, "Tungsten light");
            map.Add(4, "White fluorescent light");
            map.Add(5, "Flash");
            map.Add(6, "Custom1");
            map.Add(8, "Shade");
            map.Add(9, "Color temp.");
            map.Add(10, "Custom white balance: PC-1");
            map.Add(11, "Custom white balance: PC-2");
            map.Add(12, "Custom white balance: PC-3");
            map.Add(15, "Custom2");
            map.Add(16, "Custom3");
            map.Add(17, "Underwater");
            map.Add(18, "Custom4");
            map.Add(19, "Custom5");
            map.Add(20, "Custom white balance: PC-4");
            map.Add(21, "Custom white balance: PC-5");
            map.Add(23, "Auto: White priority");

            // Populate the ComboBox with these options
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
                uint key = map.FirstOrDefault(x => x.Value == SelectedItem.ToString()).Key;

                _actionSource?.FireEvent(ActionEvent.Command.SET_WHITE_BALANCE, (nint)key);
            }
        }

        public void Update(Observable from, CameraEvent e)
        {
            CameraModel model = (CameraModel)from;
            CameraEvent.Type eventType = e.GetEventType();

            if (eventType == CameraEvent.Type.PROPERTY_CHANGED || eventType == CameraEvent.Type.PROPERTY_DESC_CHANGED)
            {
                uint propertyID = (uint)e.GetArg();

                if (propertyID == EDSDKLib.EDSDK.PropID_WhiteBalance)
                {
                    uint property = model.WhiteBalance;

                    // Update property
                    switch (eventType)
                    {
                        case CameraEvent.Type.PROPERTY_CHANGED:
                            SelectedItem = map[property];
                            break;

                        case CameraEvent.Type.PROPERTY_DESC_CHANGED:
                            _desc = model.WhiteBalanceDesc;
                            // Optionally update ComboBox items based on _desc
                            SelectedItem = map[23];
                            break;
                    }
                }
                else if (propertyID == EDSDKLib.EDSDK.PropID_Evf_ClickWBCoeffs)
                {
                    if (eventType == CameraEvent.Type.PROPERTY_CHANGED)
                    {
                        int size;
                        EDSDKLib.EDSDK.EdsDataType datatype;
                        uint err = EDSDKLib.EDSDK.EdsGetPropertySize(model.Camera, EDSDKLib.EDSDK.PropID_Evf_ClickWBCoeffs, 0, out datatype, out size);
                        if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
                        {
                            return;
                        }

                        // Get the WB coefficient
                        EDSDKLib.EDSDK.EdsManualWBData wbCoefs = new EDSDKLib.EDSDK.EdsManualWBData();
                        nint ptr = Marshal.AllocHGlobal(size);
                        err = EDSDKLib.EDSDK.EdsGetPropertyData(model.Camera, EDSDKLib.EDSDK.PropID_Evf_ClickWBCoeffs, 0, size, ptr);
                        if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
                        {
                            Marshal.FreeHGlobal(ptr);
                            return;
                        }

                        // Set the WB coefficient converted to the manual white balance data structure
                        wbCoefs = EDSDKLib.EDSDK.MarshalPtrToManualWBData(ptr);
                        byte[] mwb = EDSDKLib.EDSDK.ConvertMWB(wbCoefs);
                        err = EDSDKLib.EDSDK.EdsSetPropertyData(model.Camera, EDSDKLib.EDSDK.PropID_ManualWhiteBalanceData, 0, mwb.Length, mwb);
                        if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
                        {
                            Marshal.FreeHGlobal(ptr);
                            return;
                        }

                        // Change the camera's white balance setting to manual white balance
                        err = EDSDKLib.EDSDK.EdsSetPropertyData(model.Camera, EDSDKLib.EDSDK.PropID_WhiteBalance, 0, sizeof(uint), 6);

                        Marshal.FreeHGlobal(ptr);
                    }
                }
            }
        }
    }
}
