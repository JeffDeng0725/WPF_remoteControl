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

using MyToDo1.Command.EVF;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyToDo1.Property
{
    class EvfPictureBox : Image, IObserver
    {
        private WriteableBitmap _writeableBitmap;
        private CameraModel _model;
        private bool _active;
        private bool m_bDrawZoomFrame;
        private EDSDKLib.EDSDK.EdsRect vRect;
        private EDSDKLib.EDSDK.EdsFocusInfo m_focusInfo;
        private ActionSource _actionSource;

        public void SetActionSource(ref ActionSource actionSource)
        {
            _actionSource = actionSource;
        }

        public EvfPictureBox()
        {
            _active = false;
        }

        public void Update(Observable from, CameraEvent e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => Update(from, e));
                return;
            }

            CameraEvent.Type eventType = e.GetEventType();
            _model = (CameraModel)from;
            uint propertyID;

            switch (eventType)
            {
                case CameraEvent.Type.EVFDATA_CHANGED:
                    nint evfDataSetPtr = e.GetArg();
                    EVFDataSet evfDataSet = (EVFDataSet)Marshal.PtrToStructure(evfDataSetPtr, typeof(EVFDataSet));
                    OnDrawImage(evfDataSet);
                    propertyID = EDSDKLib.EDSDK.PropID_FocusInfo;
                    _actionSource.FireEvent(ActionEvent.Command.GET_PROPERTY, (nint)propertyID);
                    _actionSource.FireEvent(ActionEvent.Command.DOWNLOAD_EVF, nint.Zero);
                    break;

                case CameraEvent.Type.PROPERTY_CHANGED:
                    propertyID = (uint)e.GetArg();
                    if (propertyID == EDSDKLib.EDSDK.PropID_Evf_OutputDevice)
                    {
                        uint device = _model.EvfOutputDevice;

                        if (!_active && (device & EDSDKLib.EDSDK.EvfOutputDevice_PC) != 0)
                        {
                            _active = true;
                            _actionSource.FireEvent(ActionEvent.Command.DOWNLOAD_EVF, nint.Zero);
                        }

                        if (_active && (device & EDSDKLib.EDSDK.EvfOutputDevice_PC) == 0)
                        {
                            _active = false;
                        }
                    }
                    else if (propertyID == EDSDKLib.EDSDK.PropID_FocusInfo && Source != null)
                    {
                        UpdateFocusInfo();
                    }
                    else if (propertyID == EDSDKLib.EDSDK.PropID_Evf_AFMode)
                    {
                        m_bDrawZoomFrame = _model.EvfAFMode != 2 && _model.isTypeDS;
                    }
                    break;
            }
        }

        private void OnDrawImage(EVFDataSet evfDataSet)
        {
            nint evfStream;
            ulong streamLength;

            EDSDKLib.EDSDK.EdsGetPointer(evfDataSet.stream, out evfStream);
            EDSDKLib.EDSDK.EdsGetLength(evfDataSet.stream, out streamLength);

            byte[] data = new byte[(int)streamLength];
            Marshal.Copy(evfStream, data, 0, (int)streamLength);

            // Initialize the WriteableBitmap if not already done
            if (_writeableBitmap == null || _writeableBitmap.PixelWidth != evfDataSet.sizeJpegLarge.width || _writeableBitmap.PixelHeight != evfDataSet.sizeJpegLarge.height)
            {
                _writeableBitmap = new WriteableBitmap(
                    evfDataSet.sizeJpegLarge.width,
                    evfDataSet.sizeJpegLarge.height,
                    96,
                    96,
                    PixelFormats.Bgr24,
                    null);
                Source = _writeableBitmap;
            }

            // Lock the bitmap and copy the new image data into it
            _writeableBitmap.Lock();
            Marshal.Copy(data, 0, _writeableBitmap.BackBuffer, data.Length);
            _writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, _writeableBitmap.PixelWidth, _writeableBitmap.PixelHeight));
            _writeableBitmap.Unlock();

            // Use DrawingVisual to draw overlays
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                if (evfDataSet.zoom == 1 && evfDataSet.sizeJpegLarge.width != 0 && evfDataSet.sizeJpegLarge.height != 0)
                {
                    OnDrawFocusRect(dc, ref evfDataSet);
                }

                // Draw aspect ratio adjustments
                vRect = _model.VisibleRect;
                if (_model.Aspect == 1 || _model.Aspect == 2)
                {
                    float hvRatio = (float)vRect.width / vRect.height;
                    int rWidth = (int)(_writeableBitmap.PixelWidth - _writeableBitmap.PixelHeight * hvRatio) / 2;

                    dc.DrawRectangle(Brushes.Black, null, new Rect(0, 0, rWidth, _writeableBitmap.PixelHeight));
                    dc.DrawRectangle(Brushes.Black, null, new Rect(_writeableBitmap.PixelWidth - rWidth, 0, rWidth, _writeableBitmap.PixelHeight));
                }
                else if (_model.Aspect == 7)
                {
                    float vhRatio = (float)vRect.height / vRect.width;
                    int rHeight = (int)(_writeableBitmap.PixelHeight - _writeableBitmap.PixelWidth * vhRatio) / 2;

                    dc.DrawRectangle(Brushes.Black, null, new Rect(0, 0, _writeableBitmap.PixelWidth, rHeight));
                    dc.DrawRectangle(Brushes.Black, null, new Rect(0, _writeableBitmap.PixelHeight - rHeight, _writeableBitmap.PixelWidth, rHeight));
                }
            }

            // Render the DrawingVisual to the WriteableBitmap
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                _writeableBitmap.PixelWidth,
                _writeableBitmap.PixelHeight,
                96,
                96,
                PixelFormats.Pbgra32);

            renderBitmap.Render(visual);

            _writeableBitmap.Lock();
            renderBitmap.CopyPixels(new Int32Rect(0, 0, _writeableBitmap.PixelWidth, _writeableBitmap.PixelHeight),
                                    _writeableBitmap.BackBuffer,
                                    _writeableBitmap.BackBufferStride * _writeableBitmap.PixelHeight,
                                    _writeableBitmap.BackBufferStride);
            _writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, _writeableBitmap.PixelWidth, _writeableBitmap.PixelHeight));
            _writeableBitmap.Unlock();
        }

        private void OnDrawFocusRect(DrawingContext dc, ref EVFDataSet evfDataSet)
        {
            if (m_bDrawZoomFrame)
            {
                int cx = _writeableBitmap.PixelWidth;
                int cy = _writeableBitmap.PixelHeight;

                int iw = evfDataSet.sizeJpegLarge.width;
                int ih = evfDataSet.sizeJpegLarge.height;

                long left = evfDataSet.zoomRect.x;
                long top = evfDataSet.zoomRect.y;

                long x = left * cx / iw;
                long y = top * cy / ih;

                long width = evfDataSet.zoomRect.width * cx / iw;
                long height = evfDataSet.zoomRect.height * cy / ih;

                dc.DrawRectangle(null, new Pen(Brushes.White, 3), new Rect(x, y, width, height));
            }

            Pen defaultPen = new Pen(Brushes.White, 3);
            Pen errPen = new Pen(Brushes.Red, 3);
            Pen servoPen = new Pen(Brushes.SkyBlue, 3);
            Pen justPen = new Pen(Brushes.Green, 3);
            Pen disablePen = new Pen(Brushes.Gray, 3);

            Pen currentPen = defaultPen;

            for (uint i = 0; i < m_focusInfo.pointNumber; i++)
            {
                if (m_focusInfo.focusPoint[i].valid == 1)
                {
                    switch (m_focusInfo.focusPoint[i].justFocus & 0x0f)
                    {
                        case 1:
                            currentPen = justPen;
                            break;
                        case 2:
                            currentPen = errPen;
                            break;
                        case 4:
                            currentPen = servoPen;
                            break;
                        default:
                            currentPen = defaultPen;
                            break;
                    }

                    if (m_focusInfo.focusPoint[i].selected != 1)
                    {
                        currentPen = disablePen;
                    }

                    Rect afRect = new Rect(
                        m_focusInfo.focusPoint[i].rect.x,
                        m_focusInfo.focusPoint[i].rect.y,
                        m_focusInfo.focusPoint[i].rect.width,
                        m_focusInfo.focusPoint[i].rect.height
                    );
                    dc.DrawRectangle(null, currentPen, afRect);
                }
            }
        }

        private void UpdateFocusInfo()
        {
            if (_writeableBitmap == null) return;

            m_focusInfo = _model.FocusInfo;

            float xRatio = (float)_writeableBitmap.PixelWidth / m_focusInfo.imageRect.width;
            float yRatio = (float)_writeableBitmap.PixelHeight / m_focusInfo.imageRect.height;

            for (uint i = 0; i < m_focusInfo.pointNumber; i++)
            {
                m_focusInfo.focusPoint[i].rect.x = (int)(m_focusInfo.focusPoint[i].rect.x * xRatio);
                m_focusInfo.focusPoint[i].rect.y = (int)(m_focusInfo.focusPoint[i].rect.y * yRatio);
                m_focusInfo.focusPoint[i].rect.width = (int)(m_focusInfo.focusPoint[i].rect.width * xRatio);
                m_focusInfo.focusPoint[i].rect.height = (int)(m_focusInfo.focusPoint[i].rect.height * yRatio);
            }
        }
    }
}
