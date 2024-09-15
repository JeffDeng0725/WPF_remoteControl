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

namespace MyToDo1.Property
{
    public class InfoLabel : Label
    {
        protected void UpdateProperty(string infoText)
        {
            if (!Dispatcher.CheckAccess())
            {
                // If called from a different thread, use Dispatcher to invoke the update on the UI thread
                Dispatcher.Invoke(() => UpdateProperty(infoText));
                return;
            }

            if (infoText != null)
            {
                Content = infoText;
            }
        }
    }
}

