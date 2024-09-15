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
using System.Collections.Generic;


namespace MyToDo1.Command
{
    class FileCounterCommand : Command
    {
        public FileCounterCommand(ref CameraModel model, ref nint volume) : base(ref model) { _volume = volume; }

        public struct FileNumber
        {
            public nint DcimItem;
            public int dirnum;
            public int[] filenum;

            public FileNumber(int p1)
            {
                DcimItem = nint.Zero;
                dirnum = p1;
                filenum = new int[dirnum];
            }
        }

        private nint _volume;

        public uint CountDirectory(nint camera, ref nint directoryItem, out int directory_count)
        {
            uint err = EDSDKLib.EDSDK.EDS_ERR_OK;
            int item_count = 0;
            directory_count = 0;

            // Get DCIM folder
            nint dirItem;
            EDSDKLib.EDSDK.EdsDirectoryItemInfo dirItemInfo;
            dirItemInfo.szFileName = "";
            dirItemInfo.Size = 0;

            err = EDSDKLib.EDSDK.EdsGetChildCount(_volume, out item_count);
            if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                return err;
            }
            for (int i = 0; i < item_count; ++i)
            {
                // Get the ith item under the specifed volume
                err = EDSDKLib.EDSDK.EdsGetChildAtIndex(_volume, i, out dirItem);
                if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
                {
                    continue;
                }

                // Get retrieved item information
                err = EDSDKLib.EDSDK.EdsGetDirectoryItemInfo(dirItem, out dirItemInfo);
                if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
                {
                    return err;
                }

                // Indicates whether or not the retrieved item is a DCIM folder.
                if (dirItemInfo.szFileName == "DCIM" && dirItemInfo.isFolder == 1)
                {
                    directoryItem = dirItem;
                    break;
                }

                // Release retrieved item
                if (dirItem != nint.Zero)
                {
                    EDSDKLib.EDSDK.EdsRelease(dirItem);
                }
            }

            // Get number of directory in DCIM.
            return err = EDSDKLib.EDSDK.EdsGetChildCount(directoryItem, out directory_count);

        }

        public uint CountImages(nint camera, ref nint directoryItem, ref int directory_count, ref int fileCount, ref FileNumber fileNumber, ref List<nint> imageItems)
        {
            uint err = EDSDKLib.EDSDK.EDS_ERR_OK;

            // Get the number of camera volumes
            fileCount = 0;

            // Get retrieved item information

            for (int i = 0; i < directory_count; ++i)
            {
                int count = 0;
                err = CountImagesByDirectory(ref directoryItem, i, ref count, ref imageItems);
                if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
                {
                    return err;
                }
                fileCount += count;
                fileNumber.filenum[i] = count;
            }
            return EDSDKLib.EDSDK.EDS_ERR_OK;
        }

        private uint CountImagesByDirectory(ref nint directoryItem, int directoryNo, ref int image_count, ref List<nint> imageItems)
        {
            int item_count = 0;

            nint directoryfiles;
            nint fileitem;
            EDSDKLib.EDSDK.EdsDirectoryItemInfo dirItemInfo;

            uint err = EDSDKLib.EDSDK.EdsGetChildAtIndex(directoryItem, directoryNo, out directoryfiles);
            if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                return err;
            }

            // Get retrieved item information
            // Get item name
            err = EDSDKLib.EDSDK.EdsGetDirectoryItemInfo(directoryfiles, out dirItemInfo);
            if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                return err;
            }

            int index = 0, filecount = 0;
            err = EDSDKLib.EDSDK.EdsGetChildCount(directoryfiles, out item_count);
            if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
            {
                return err;
            }
            for (index = 0; index < item_count; ++index)
            {
                err = EDSDKLib.EDSDK.EdsGetChildAtIndex(directoryfiles, index, out fileitem);
                if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
                {
                    return err;
                }

                // Get retrieved item information
                err = EDSDKLib.EDSDK.EdsGetDirectoryItemInfo(fileitem, out dirItemInfo);
                if (err != EDSDKLib.EDSDK.EDS_ERR_OK)
                {
                    return err;
                }
                if (dirItemInfo.isFolder == 0)
                {
                    imageItems.Add(fileitem);
                    filecount += 1;
                }

            }
            image_count = filecount;

            return EDSDKLib.EDSDK.EDS_ERR_OK;
        }


    }

}
