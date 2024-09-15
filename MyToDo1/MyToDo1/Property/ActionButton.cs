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

// Updated by Jeff 09/03/2024

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MyToDo1.Property
{
    // 自定义WPF按钮类
    public class ActionButton : Button
    {
        // 用于存储按钮的命令
        public ActionEvent.Command Command { get; set; }

        // 用于触发事件的操作源
        private ActionSource _actionSource;

        // 设置操作源的方法
        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        // 重写OnClick方法，定义按钮点击时的行为
        protected override void OnClick()
        {
            // 如果_actionSource已设置，则触发事件
            _actionSource?.FireEvent(Command, nint.Zero);

            // 调用基类的OnClick方法，以确保其他标准点击行为的处理
            base.OnClick();
        }
    }
}
