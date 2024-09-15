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

namespace MyToDo1
{
    public class CameraEvent
    {
        public enum Type
        {
            NONE,
            ERROR,
            DEVICE_BUSY,
            DOWNLOAD_START,
            DOWNLOAD_COMPLETE,
            EVFDATA_CHANGED,
            PROGRESS_REPORT,
            PROPERTY_CHANGED,
            PROPERTY_DESC_CHANGED,
            DELETE_START,
            DELETE_COMPLETE,
            PROGRESS,
            ANGLEINFO,
            MOUSE_CURSOR,
            SHUT_DOWN
        }

        private Type _type = Type.NONE;
        private nint _arg;

        public CameraEvent(Type type, nint arg)
        {
            _type = type;
            _arg = arg;
        }

        public Type GetEventType() { return _type; }
        public nint GetArg() { return _arg; }
    }
}
