using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ImageResizer;

namespace KRBAccounting.Web.Helpers
{
    public class FileHelper
    {
        readonly string _rootDir = string.Empty;
       
        public FileHelper()
        {

        }

        public static bool IsValidImageType(string fileTypes)
        {
            switch (fileTypes)
            {
                case ".jpg":
                    return true;
                    break;
                case ".jpeg":
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }

        private static void ResizeImage(HttpPostedFileBase file, string photoFolder, int width, int height, string imgName, string extension)
        {
            if (file.ContentLength <= 0)
            {
                return;
            }
            if (!Directory.Exists(photoFolder)) Directory.CreateDirectory(photoFolder);

            //The resizing settings can specify any of 30 commands.. See http://imageresizing.net for details.
            var resizeCropSettings = new ResizeSettings("width=" + width + "&height=" + height + "&format=" + extension + "&crop=auto&anchor=topcenter");

            //Generate a filename (GUIDs are safest).
            var fileName = Path.Combine(photoFolder, imgName);

            //Let the image builder add the correct extension based on the output file type (which may differ).
            fileName = ImageBuilder.Current.Build(file, fileName, resizeCropSettings, false, true);
        }

        public void ConvertToThumbnail(HttpPostedFileBase uploadedFile, string filename, string extension, string path)
        {
            ResizeImage(uploadedFile, path, 125, 125, filename, extension);
        }

        

        public void DeleteFiles(string file)
        {

            FileInfo objFileInfoLogo = new FileInfo(file);

            if (objFileInfoLogo.Exists)
            {
                File.Delete(file);
            }

        }

        private static bool ThumbnailCallback()
        {
            return false;
        }
        public static string GetStudentImageShortLink(string guid, string ext)
        {
            guid += "_th";
            //string url = string.Empty;
            //return url;

            var imagePath = "Content/img/noimage.jpg";
            if (string.IsNullOrEmpty(guid) || string.IsNullOrEmpty(ext))
            {
                return imagePath;
            }

            try
            {

                var file = AppDomain.CurrentDomain.BaseDirectory + "Content\\academicsimage\\" + guid + ext;

                if (File.Exists(file))
                {
                    return imagePath = "Content/academicsimage/" + guid + ext;
                }
                return imagePath;

            }
            catch
            {
                return imagePath; //if any exception then return default image
            }
        }

        //public static string GetStudentPhoto()
        //{
            
        //}
    }
}