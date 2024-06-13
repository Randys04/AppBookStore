using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBookStore.Repositories.Abstract;

namespace AppBookStore.Repositories.Implementation
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public bool DeleteImage(string imgFileName)
        {
            try
            {
                var wwwpath =  this.environment.WebRootPath; 
                var path = Path.Combine(wwwpath, "Uploads\\", imgFileName);

                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }

                return true;

            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public Tuple<int, string> SaveImage(IFormFile imgFile)
        {
            try
            {
                var wwwpath =  this.environment.WebRootPath; // Se obtine el directorio raiz, en este caso seria el wwwroot
                var path = Path.Combine(wwwpath, "Uploads");

                if(!Directory.Exists(path)) // Valida la existencia del directorio Uploads y en caso ed que no exista lo crea
                {
                    Directory.CreateDirectory(path);
                }

                var ext = Path.GetExtension(imgFile.FileName); // Obtiene la extencion del archivo
                var allowedExtensions = new String[] {".jpg" , ".png" , ".jpeg"};

                if(!allowedExtensions.Contains(ext))
                {
                    return new Tuple<int, string>(0, "Extension not valid");
                }

                string uniqueString =  Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;

                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create); // Crea instancia de stream
                imgFile.CopyTo(stream); // Lo carga en el directorio

                return new Tuple<int, string>(1, newFileName);
            }

            catch (System.Exception)
            {
                return new Tuple<int, string>(0, "The image could not be saved");
            }
        }
    }
}