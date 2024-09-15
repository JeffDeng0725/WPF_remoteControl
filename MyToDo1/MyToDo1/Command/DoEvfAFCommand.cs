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

namespace MyToDo1.Command
{
    class DoEvfAFCommand : Command
    {
        private uint _status;

        public DoEvfAFCommand(ref CameraModel model, uint status) : base(ref model)
        {
            _status = status;
        }

        // Execute command	
        public override bool Execute()
        {
            uint err = EDSDKLib.EDSDK.EdsSendCommand(_model.Camera, EDSDKLib.EDSDK.CameraCommand_DoEvfAf, (int)_status);
            //Notification of error
            if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                // It retries it at device busy
                if (err == EDSDKLib.EDSDK.EDS_ERR_DEVICE_BUSY)
                {
                    CameraEvent e = new CameraEvent(CameraEvent.Type.DEVICE_BUSY, nint.Zero);
                    _model.NotifyObservers(e);
                    return true;
                }
                else
                {
                    CameraEvent e = new CameraEvent(CameraEvent.Type.ERROR, (nint)err);
                    _model.NotifyObservers(e);
                    return true;
                }
            }

            return true;
        }
    }
}
