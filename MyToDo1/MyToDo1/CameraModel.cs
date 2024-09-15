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
    public class CameraModel : Observable
    {
        public nint Camera { get; set; }

        // Model name
        public string ModelName { get; set; }

        // Type DS
        public bool isTypeDS { get; set; }

        // Taking a picture parameter
        public uint AEMode { get; set; }
        public uint AFMode { get; set; }
        public uint DriveMode { get; set; }
        public uint WhiteBalance { get; set; }
        public uint Av { get; set; }
        public uint Tv { get; set; }
        public uint Iso { get; set; }
        public uint MeteringMode { get; set; }
        public uint ExposureCompensation { get; set; }
        public uint ImageQuality { get; set; }
        public uint AvailableShot { get; set; }
        public uint EvfMode { get; set; }
        public uint StartupEvfOutputDevice { get; set; }
        public uint EvfOutputDevice { get; set; }
        public uint EvfDepthOfFieldPreview { get; set; }
        public uint EvfAFMode { get; set; }
        public EDSDKLib.EDSDK.EdsFocusInfo FocusInfo { get; set; }
        public uint BatteryLebel { get; set; }
        public uint Zoom { get; set; }
        public EDSDKLib.EDSDK.EdsRect ZoomRect { get; set; }
        public uint FlashMode { get; set; }
        public bool canDownloadImage { get; set; }
        public string SaveDirectory { get; set; } // 添加全局保存路径 08/21/2024
        public bool isEvfEnable { get; set; }
        public uint TempStatus { get; set; }
        public uint RollPitch { get; set; }
        public uint MovieQuality { get; set; }
        public uint MovieHFR { get; set; }
        public uint PictureStyle { get; set; }
        public uint Aspect { get; set; }
        public EDSDKLib.EDSDK.EdsRect VisibleRect { get; set; }
        public uint FixedMovie { get; set; }
        public uint MirrorUpSetting { get; set; }
        public uint MirrorLockUpState { get; set; }
        public byte[] ClickWB { get; set; }
        public int ClickPoint { get; set; }
        public uint AutoPowerOff { get; set; }
        public EDSDKLib.EDSDK.EdsSize SizeJpegLarge { get; set; }

        public EDSDKLib.EDSDK.EdsPropertyDesc AEModeDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc DriveModeDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc WhiteBalanceDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc AvDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc TvDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc IsoDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc MeteringModeDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc ExposureCompensationDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc ImageQualityDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc EvfAFModeDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc ZoomDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc FlashModeDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc MovieQualityDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc MovieHFRDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc PictureStyleDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc AspectDesc { get; set; }
        public EDSDKLib.EDSDK.EdsPropertyDesc AutoPowerOffDesc { get; set; }

        public enum Status
        {
            NONE,
            DOWNLOADING,
            DELETEING,
            CANCELING
        }
        public Status _ExecuteStatus;

        // Constructor
        public CameraModel(nint camera)
        {
            const uint UnKnownCode = 0xffffffff;

            Camera = camera;
            AEMode = UnKnownCode;
            AFMode = UnKnownCode;
            DriveMode = UnKnownCode;
            WhiteBalance = UnKnownCode;
            Av = UnKnownCode;
            Tv = UnKnownCode;
            Iso = UnKnownCode;
            MeteringMode = UnKnownCode;
            ExposureCompensation = UnKnownCode;
            ImageQuality = UnKnownCode;
            EvfMode = UnKnownCode;
            EvfOutputDevice = UnKnownCode;
            EvfDepthOfFieldPreview = UnKnownCode;
            EvfAFMode = UnKnownCode;
            BatteryLebel = UnKnownCode;
            Zoom = UnKnownCode;
            FlashMode = UnKnownCode;
            AvailableShot = 0;
            canDownloadImage = true;
            TempStatus = UnKnownCode;
            RollPitch = 1;
            MovieQuality = UnKnownCode;
            MovieHFR = UnKnownCode;
            PictureStyle = UnKnownCode;
            Aspect = UnKnownCode;
            FixedMovie = UnKnownCode;
            MirrorUpSetting = UnKnownCode;
            MirrorLockUpState = UnKnownCode;
            AutoPowerOff = UnKnownCode;
        }

        public void SetPropertyUInt32(uint propertyID, uint value)
        {
            switch (propertyID)
            {
                case EDSDKLib.EDSDK.PropID_AEModeSelect: AEMode = value; break;
                case EDSDKLib.EDSDK.PropID_AFMode: AFMode = value; break;
                case EDSDKLib.EDSDK.PropID_DriveMode: DriveMode = value; break;
                case EDSDKLib.EDSDK.PropID_Tv: Tv = value; break;
                case EDSDKLib.EDSDK.PropID_Av: Av = value; break;
                case EDSDKLib.EDSDK.PropID_ISOSpeed: Iso = value; break;
                case EDSDKLib.EDSDK.PropID_MeteringMode: MeteringMode = value; break;
                case EDSDKLib.EDSDK.PropID_ExposureCompensation: ExposureCompensation = value; break;
                case EDSDKLib.EDSDK.PropID_ImageQuality: ImageQuality = value; break;
                case EDSDKLib.EDSDK.PropID_Evf_Mode: EvfMode = value; break;
                case EDSDKLib.EDSDK.PropID_Evf_OutputDevice: if (EvfOutputDevice == 0xffffffff) StartupEvfOutputDevice = value; EvfOutputDevice = value; break;
                case EDSDKLib.EDSDK.PropID_Evf_DepthOfFieldPreview: EvfDepthOfFieldPreview = value; break;
                case EDSDKLib.EDSDK.PropID_Evf_AFMode: EvfAFMode = value; break;
                case EDSDKLib.EDSDK.PropID_AvailableShots: AvailableShot = value; break;
                case EDSDKLib.EDSDK.PropID_DC_Zoom: Zoom = value; break;
                case EDSDKLib.EDSDK.PropID_DC_Strobe: FlashMode = value; break;
                case EDSDKLib.EDSDK.PropID_TempStatus: TempStatus = value; break;
                case EDSDKLib.EDSDK.PropID_PictureStyle: PictureStyle = value; break;
                case EDSDKLib.EDSDK.PropID_MovieHFRSetting: MovieHFR = value; break;
                case EDSDKLib.EDSDK.PropID_Aspect: Aspect = value; break;
                case EDSDKLib.EDSDK.PropID_FixedMovie: FixedMovie = value; break;
                case EDSDKLib.EDSDK.PropID_MirrorUpSetting: MirrorUpSetting = value; break;
                case EDSDKLib.EDSDK.PropID_MirrorLockUpState: MirrorLockUpState = value; break;
                case EDSDKLib.EDSDK.PropID_AutoPowerOffSetting: AutoPowerOff = value; break;
            }
        }

        public void SetPropertyInt32(uint propertyID, uint value)
        {
            switch (propertyID)
            {
                case EDSDKLib.EDSDK.PropID_WhiteBalance: WhiteBalance = value; break;
                case EDSDKLib.EDSDK.PropID_BatteryLevel: BatteryLebel = value; break;
            }
        }

        //Setting of taking a picture parameter(String)
        public void SetPropertyString(uint propertyID, string str)
        {
            switch (propertyID)
            {
                case EDSDKLib.EDSDK.PropID_ProductName: ModelName = str; isTypeDS = str.Contains("EOS"); break;
            }
        }

        public void SetPropertyFocusInfo(uint propertyID, EDSDKLib.EDSDK.EdsFocusInfo info)
        {
            switch (propertyID)
            {
                case EDSDKLib.EDSDK.PropID_FocusInfo: FocusInfo = info; break;
            }
        }

        public void SetPropertyByteBlock(uint propertyID, byte[] data)
        {
            switch (propertyID)
            {
                case EDSDKLib.EDSDK.PropID_MovieParam: MovieQuality = BitConverter.ToUInt32(data, 0); break;
                case EDSDKLib.EDSDK.PropID_Evf_ClickWBCoeffs: ClickWB = data; break;
            }
        }

        public void SetPropertyRect(uint propertyID, EDSDKLib.EDSDK.EdsRect info)
        {
            switch (propertyID)
            {
                case EDSDKLib.EDSDK.PropID_Evf_ZoomRect: ZoomRect = info; break;
                case EDSDKLib.EDSDK.PropID_Evf_VisibleRect: VisibleRect = info; break;
            }
        }

        //Setting of value list that can set taking a picture parameter
        public void SetPropertyDesc(uint propertyID, ref EDSDKLib.EDSDK.EdsPropertyDesc desc)
        {
            switch (propertyID)
            {
                case EDSDKLib.EDSDK.PropID_AEModeSelect: AEModeDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_DriveMode: DriveModeDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_WhiteBalance: WhiteBalanceDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_Tv: TvDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_Av: AvDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_ISOSpeed: IsoDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_MeteringMode: MeteringModeDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_ExposureCompensation: ExposureCompensationDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_ImageQuality: ImageQualityDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_Evf_AFMode: EvfAFModeDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_DC_Zoom: ZoomDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_DC_Strobe: FlashModeDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_MovieParam: MovieQualityDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_MovieHFRSetting: MovieHFRDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_PictureStyle: PictureStyleDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_Aspect: AspectDesc = desc; break;
                case EDSDKLib.EDSDK.PropID_AutoPowerOffSetting: AutoPowerOffDesc = desc; break;
            }
        }

        //Acquisition of value list that can set taking a picture parameter
        public EDSDKLib.EDSDK.EdsPropertyDesc GetPropertyDesc(uint propertyID)
        {
            EDSDKLib.EDSDK.EdsPropertyDesc desc = new EDSDKLib.EDSDK.EdsPropertyDesc { };
            switch (propertyID)
            {
                case EDSDKLib.EDSDK.PropID_AEModeSelect: desc = AEModeDesc; break;
                case EDSDKLib.EDSDK.PropID_DriveMode: desc = DriveModeDesc; break;
                case EDSDKLib.EDSDK.PropID_WhiteBalance: desc = WhiteBalanceDesc; break;
                case EDSDKLib.EDSDK.PropID_Tv: desc = TvDesc; break;
                case EDSDKLib.EDSDK.PropID_Av: desc = AvDesc; break;
                case EDSDKLib.EDSDK.PropID_ISOSpeed: desc = IsoDesc; break;
                case EDSDKLib.EDSDK.PropID_MeteringMode: desc = MeteringModeDesc; break;
                case EDSDKLib.EDSDK.PropID_ExposureCompensation: desc = ExposureCompensationDesc; break;
                case EDSDKLib.EDSDK.PropID_ImageQuality: desc = ImageQualityDesc; break;
                case EDSDKLib.EDSDK.PropID_Evf_AFMode: desc = EvfAFModeDesc; break;
                case EDSDKLib.EDSDK.PropID_DC_Zoom: desc = ZoomDesc; break;
                case EDSDKLib.EDSDK.PropID_DC_Strobe: desc = FlashModeDesc; break;
                case EDSDKLib.EDSDK.PropID_MovieParam: desc = MovieQualityDesc; break;
                case EDSDKLib.EDSDK.PropID_MovieHFRSetting: desc = MovieHFRDesc; break;
                case EDSDKLib.EDSDK.PropID_PictureStyle: desc = PictureStyleDesc; break;
                case EDSDKLib.EDSDK.PropID_Aspect: desc = AspectDesc; break;
                case EDSDKLib.EDSDK.PropID_AutoPowerOffSetting: desc = AutoPowerOffDesc; break;
            }
            return desc;
        }

        public EDSDKLib.EDSDK.EdsPoint GetZoomPosition()
        {
            EDSDKLib.EDSDK.EdsPoint zoomPosition;
            zoomPosition.x = ZoomRect.x;
            zoomPosition.y = ZoomRect.y;
            return zoomPosition;
        }
    }
}

