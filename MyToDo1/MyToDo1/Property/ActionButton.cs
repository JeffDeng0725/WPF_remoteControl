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
    // �Զ���WPF��ť��
    public class ActionButton : Button
    {
        // ���ڴ洢��ť������
        public ActionEvent.Command Command { get; set; }

        // ���ڴ����¼��Ĳ���Դ
        private ActionSource _actionSource;

        // ���ò���Դ�ķ���
        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        // ��дOnClick���������尴ť���ʱ����Ϊ
        protected override void OnClick()
        {
            // ���_actionSource�����ã��򴥷��¼�
            _actionSource?.FireEvent(Command, nint.Zero);

            // ���û����OnClick��������ȷ��������׼�����Ϊ�Ĵ���
            base.OnClick();
        }
    }
}
