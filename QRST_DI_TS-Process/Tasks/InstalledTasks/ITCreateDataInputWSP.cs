﻿using System.IO;
using QRST_DI_TS_Basis.DirectlyAddress;

namespace QRST_DI_TS_Process.Tasks.InstalledTasks
{
    public class ITCreateDataInputWSP : TaskClass
    {
        /// <summary>
        /// 任务名,定义唯一标识，不可动态修改
        /// </summary>
        public override string TaskName
        {
            get { return "ITCreateDataInputWSP"; }
            set { }
        }

        public override void Process()
        {
            this.ParentOrder.Logs.Add(string.Format("建立工作空间。"));
            //创建工作空间目录
            if (!Directory.Exists(this.ParentOrder.OrderWorkspace))
            {
                Directory.CreateDirectory(this.ParentOrder.OrderWorkspace);
            }

            if (!Directory.Exists(StorageBasePath.SharePath_OrignalData(this.ParentOrder.OrderWorkspace)))
            {
                Directory.CreateDirectory(StorageBasePath.SharePath_OrignalData(this.ParentOrder.OrderWorkspace));
            }

            if (!Directory.Exists(StorageBasePath.SharePath_CorrectedData(this.ParentOrder.OrderWorkspace)))
            {
                Directory.CreateDirectory(StorageBasePath.SharePath_CorrectedData(this.ParentOrder.OrderWorkspace));
            }

            if (!Directory.Exists(StorageBasePath.SharePath_TiledData(this.ParentOrder.OrderWorkspace)))
            {
                Directory.CreateDirectory(StorageBasePath.SharePath_TiledData(this.ParentOrder.OrderWorkspace));
            }

            if (!Directory.Exists(StorageBasePath.SharePath_Products(this.ParentOrder.OrderWorkspace)))
            {
                Directory.CreateDirectory(StorageBasePath.SharePath_Products(this.ParentOrder.OrderWorkspace));
            }

        }

    }
}