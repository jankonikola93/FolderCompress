using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderCompress
{
    public static class Pomocna
    {
        public static string BrowseFolderPath()
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            //folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                return folderDlg.SelectedPath;
            }
            return string.Empty;
        }
        public static void PrikaziPoruku(string poruka)
        {
            MessageBox.Show(poruka, "Message");
        }
        public static string CompressByDate(string sourcePath, string destinationPath, string archiveName)
        {
            if(string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destinationPath) || string.IsNullOrEmpty(archiveName))
            {
                return "Source folder, destination folder and Archive name are mandatory parametars!";
            }
            int filesCount = 0;
            int archivesCount = 0;
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(sourcePath);
                var files = dirInfo.GetFiles().GroupBy(f => f.LastWriteTime.Date);
                if (files.Count() < 1)
                    return "No files to compress in folder " + sourcePath;
                foreach (var fileList in files)
                {
                    var zipFileName = archiveName + "_" + fileList.First().LastWriteTime.Date.ToString("yyyyMMdd") + ".zip";
                    var zipFilePath = Path.Combine(destinationPath, zipFileName);
                    using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                    {
                        foreach (var file in fileList)
                        {
                            archive.CreateEntryFromFile(file.FullName, file.Name, CompressionLevel.Optimal);
                            filesCount += 1;
                        }
                        archivesCount += 1;
                    }
                }
            }
            catch (Exception e)
            {
                return "Some error occured while compressing this files!\n Error:" + e.Message;
            }
            return "Compressed: " + filesCount + "file(s) in: " + archivesCount + "archive(s)";
        }

        public static string CompressAllFiles(string sourcePath, string destinationPath, string archiveName)
        {
            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destinationPath) || string.IsNullOrEmpty(archiveName))
            {
                return "Source folder, destination folder and Archive name are mandatory parametars!";
            }
            int filesCount = 0;
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(sourcePath);
                var files = dirInfo.GetFiles();
                filesCount = files.Count();
                if (filesCount<1)
                {
                    return "No files to compress in folder " + sourcePath;
                }
                archiveName += ".zip";
                var zipName = Path.Combine(destinationPath, archiveName);
                ZipFile.CreateFromDirectory(sourcePath, zipName);
            }
            catch (Exception e)
            {
                return "Some error occured while compressing this files!\n Error:" + e.Message;
            }
            return "Compressed: " + filesCount + "file(s)";
        }
    }
}
