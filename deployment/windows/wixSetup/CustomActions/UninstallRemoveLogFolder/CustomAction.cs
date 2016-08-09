﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.Deployment.WindowsInstaller;
using System.Xml;

namespace SetupAppSettings
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult Main(Session session)
        {
            session.Log("Removing log folder");

            try
            {
                RemoveLogFolder();
            }
            catch (Exception e)
            {
                session.Log("Failed to remove log folder.");
                session.Log("Exception: " + e.ToString());
                return ActionResult.NotExecuted;
            }

            session.Log("Log folder removed");

            return ActionResult.Success;
        }

        private static void RemoveLogFolder()
        {
            // remove logs
            string path = Environment.GetEnvironmentVariable("APPDATA") + "\\..\\local\\Coati Software\\Coati\\log";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            // remove sample projects
            path = Environment.GetEnvironmentVariable("APPDATA") + "\\..\\local\\Coati Software\\Coati\\projects\\tutorial";
            if(Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            path = Environment.GetEnvironmentVariable("APPDATA") + "\\..\\local\\Coati Software\\Coati\\projects\\tictactoe";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            // remove projects folder if empty (avoid deleting user generated files)
            path = Environment.GetEnvironmentVariable("APPDATA") + "\\..\\local\\Coati Software\\Coati\\projects";
            if (Directory.Exists(path) && !Directory.EnumerateFileSystemEntries(path).Any())
            {
                Directory.Delete(path, true);
            }

            // remove coati folder if empty (may not be empty if user stored stuff in here)
            path = Environment.GetEnvironmentVariable("APPDATA") + "\\..\\local\\Coati Software\\Coati";
            if (Directory.Exists(path) && !Directory.EnumerateFileSystemEntries(path).Any())
            {
                Directory.Delete(path, true);
            }

            path = Environment.GetEnvironmentVariable("APPDATA") + "\\..\\local\\Coati Software";
            if (Directory.Exists(path) && !Directory.EnumerateFileSystemEntries(path).Any())
            {
                Directory.Delete(path, true);
            }

            //string coatiPath = Environment.GetEnvironmentVariable("APPDATA") + "\\..\\local\\Coati Software";

            //Directory.Delete(coatiPath, true);
        }
    }
}