﻿using System;
using System.Diagnostics;

 
namespace QRST_DI_DS_DataTransfer
{
    public class Cmd
    {
        /// <summary>
        /// 执行Cmd命令
        /// </summary>
        /// <param name="workingDirectory">要启动的进程的目录</param>
        /// <param name="command">要执行的命令</param>
        public static string StartCmd(String workingDirectory, String command,out string errorMsg)
        {
            string str = "";
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = workingDirectory;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(command);
          //  str = p.StandardOutput.
            p.StandardInput.WriteLine("exit");
            errorMsg = p.StandardError.ReadToEnd();
            return p.StandardOutput.ReadToEnd();
        }
    }
}
