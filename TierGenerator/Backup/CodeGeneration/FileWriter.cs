using System;
using System.IO;

namespace TierGenerator.CodeGeneration
{
    /// <summary>
    /// Write the code File
    /// </summary>
    class FileWriter
    {


        /// <summary>
        /// method to create File
        /// </summary>
        /// <param name="folderPath">Folder Path</param>
        /// <param name="fileName">file name</param>
        /// <param name="fileText">file text</param>        
        public static void WriteFile(string folderPath, string fileName, string fileText)
        {
            // Check Folder
            CheckFolder(folderPath);

            string filePath = folderPath + Path.DirectorySeparatorChar + fileName;
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(fileText);
                sw.Flush();
                sw.Close();
            }
                        
        }

        /// <summary>
        /// method to check the existance of the folder. if folder not exist then it create it
        /// </summary>
        /// <param name="folderPath">path of the folder</param>
        public static  void CheckFolder(string folderPath)
        {

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}
