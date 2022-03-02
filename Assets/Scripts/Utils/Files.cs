using System;
using System.IO;
using System.Text;
using static System.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Axitor.Utils
{
    public static class Files
    {
        /// <summary>
        /// Private fields
        /// </summary>
        private static System.Object _syncObject = new System.Object();

        /// <summary>
        /// Read lines from a file
        /// </summary>
        public static List<string> ReadLines(string filePath, bool suppressWarning = false)
        {
            List<string> lines = new List<string>();

            try
            {
                if (File.Exists(filePath))
                {
                    string line;
                    Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line != "")
                            {
                                lines.Add(line);
                            }
                        }
                    }
                    if (stream != null) { stream.Dispose(); }
                }
                else if(!suppressWarning)
                {
                    DebugMessage("File not exist " + filePath);
                }
            }
            catch (Exception e)
            {
                DebugException(e);
            }

            return lines;
        }

        /// <summary>
        /// Write lines to a file
        /// </summary>
        public static void WriteLines(string filePath, List<string> lines)
        {
            try
            {
                lock (_syncObject)
                {
                    Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                    using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
                    {
                        for (int i = 0; i < lines.Count; i++)
                        {
                            streamWriter.WriteLine(lines[i]);
                        }
                    }
                    if (stream != null) { stream.Dispose(); }
                }
            }
            catch (Exception e)
            {
                DebugException(e);
            }
        }

        /// <summary>
        /// Debug message
        /// </summary>
        private static void DebugMessage(string message = "")
        {
            Debug.LogError(message);
        }

        /// <summary>
        /// Debug exception
        /// </summary>
        private static void DebugException(Exception e)
        {
            Debug.LogError("Message: " + e.Message + NewLine + "TargetSite: " + e.TargetSite + NewLine + "StackTrace: " + e.StackTrace);
        }
    }
}
