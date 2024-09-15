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
    class ActionRadioButton : RadioButton
    {
        public ActionEvent.Command Command { get; set; }

        private ActionSource _actionSource;

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        protected override void OnClick()
        {
            _actionSource?.FireEvent(Command, nint.Zero);

            base.OnClick();
        }
    }
}

