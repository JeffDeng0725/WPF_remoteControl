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
using System.Windows.Threading;

namespace MyToDo1;

public class PropertyTrackBar : Slider
{
    private delegate void _UpdateProperty(uint value);

    protected void UpdateProperty(uint value)
    {
        if (!Dispatcher.CheckAccess())
        {
            // The update processing can be executed from another thread.
            Dispatcher.Invoke(new _UpdateProperty(UpdateProperty), value);
            return;
        }

        // The character string corresponding to data is acquired.
        if (this.IsEnabled)
        {
            this.Value = (int)value;
        }
    }

    private delegate void _UpdatePropertyDesc(ref EDSDKLib.EDSDK.EdsPropertyDesc desc);

    protected void UpdatePropertyDesc(ref EDSDKLib.EDSDK.EdsPropertyDesc desc)
    {
        if (!Dispatcher.CheckAccess())
        {
            // The update processing can be executed from another thread.
            Dispatcher.Invoke(new _UpdatePropertyDesc(UpdatePropertyDesc), desc);
            return;
        }

        this.IsEnabled = (desc.NumElements != 0);

        if (desc.NumElements != 0)
        {
            this.Maximum = desc.PropDesc[0] - 1;
        }
    }
}
