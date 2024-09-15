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
    class ProgressBar : System.Windows.Controls.ProgressBar
    {
        public ActionEvent.Command Command { get; set; }

        private delegate void _UpdateProperty(uint value);

        protected void UpdateProperty(uint value)
        {
            if (!Dispatcher.CheckAccess())
            {
                // The update processing can be executed from another thread.
                Dispatcher.Invoke(new _UpdateProperty(UpdateProperty), value);
                return;
            }

            Value = (int)value;
        }
    }
}

