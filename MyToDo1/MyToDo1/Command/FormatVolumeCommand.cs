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
using System.Windows;

namespace MyToDo1.Command
{
    class FormatVolumeCommand : Command
    {
        public FormatVolumeCommand(ref CameraModel model, ref nint volume) : base(ref model)
        {
            _volume = volume;
        }

        private nint _volume;

        public override bool Execute()
        {
            nint camera = _model.Camera;
            uint err = 0;

            // Format the specified SD card
            if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                // Use WPF MessageBox
                if (MessageBox.Show("Format the memory card?", "CameraControl", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    return true;
                }

                if (err == EDSDKLib.EDSDK.EDS_ERR_OK)
                {
                    err = EDSDKLib.EDSDK.EdsFormatVolume(_volume);
                }
            }

            return true;
        }
    }
}
