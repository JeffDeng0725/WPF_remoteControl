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
using System.Runtime.InteropServices;

namespace MyToDo1.Command
{
    class SetPropertyCommand : Command
    {
        private uint _propertyID;
        private object _data;

        public SetPropertyCommand(ref CameraModel model, uint propertyID, object data) : base(ref model)
        {
            _propertyID = propertyID;
            _data = data;
        }

        public override bool Execute()
        {
            uint err = EDSDKLib.EDSDK.EDS_ERR_OK;

            err = EDSDKLib.EDSDK.EdsSetPropertyData(_model.Camera, _propertyID, 0, Marshal.SizeOf(_data), _data);

            if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                // It retries it at device busy
                if (err == EDSDKLib.EDSDK.EDS_ERR_DEVICE_BUSY)
                {
                    CameraEvent e = new CameraEvent(CameraEvent.Type.DEVICE_BUSY, nint.Zero);
                    _model.NotifyObservers(e);
                    return true;
                }

                {
                    CameraEvent e = new CameraEvent(CameraEvent.Type.ERROR, (nint)err);
                    _model.NotifyObservers(e);
                }
            }
            return true;
        }
    }
}
