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
using System.Windows.Input;

namespace MyToDo1.Property
{
    class ZoomTrackBar : Slider, IObserver
    {
        private ActionSource _actionSource;
        private EDSDKLib.EDSDK.EdsPropertyDesc _desc;

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        public ZoomTrackBar()
        {
            MouseUp += ZoomTrackBar_MouseUp;
            PreviewMouseWheel += ZoomTrackBar_PreviewMouseWheel;
        }

        private void ZoomTrackBar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_actionSource != null)
            {
                _actionSource.FireEvent(ActionEvent.Command.SET_ZOOM, (nint)Value);
            }
        }

        private void ZoomTrackBar_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true; // Prevent the mouse wheel from changing the Slider value
        }

        public void Update(Observable from, CameraEvent e)
        {
            CameraModel model = (CameraModel)from;
            CameraEvent.Type eventType = e.GetEventType();

            if (eventType == CameraEvent.Type.PROPERTY_CHANGED || eventType == CameraEvent.Type.PROPERTY_DESC_CHANGED)
            {
                uint propertyID = (uint)e.GetArg();

                // DS does not need zoom step value
                if (propertyID == EDSDKLib.EDSDK.PropID_DC_Zoom && !model.isTypeDS)
                {
                    uint property = model.Zoom;

                    // Update property
                    switch (eventType)
                    {
                        case CameraEvent.Type.PROPERTY_CHANGED:
                            Value = property;
                            break;

                        case CameraEvent.Type.PROPERTY_DESC_CHANGED:
                            _desc = model.ZoomDesc;
                            Maximum = _desc.PropDesc[0] - 1;
                            Value = property;
                            break;
                    }
                }
            }
        }
    }
}
