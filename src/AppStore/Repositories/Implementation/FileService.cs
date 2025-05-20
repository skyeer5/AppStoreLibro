using AppStore.Repositories.Absatract;

namespace AppStore.Repositories.Implementation
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try{
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(imageFile.FileName);
                var extensionAllowed = new string[]{".jpg",".jpeg", ".png"};
                if(!extensionAllowed.Contains(ext))
                {
                    var message = $"Las extensiones de imagenes permitidas son {extensionAllowed}";
                    return new Tuple<int, string>(0,message);
                }
                var uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                
                var fileWithPath = Path.Combine(path, newFileName);

                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();

                return new Tuple<int,string>(1, newFileName);

            }catch(Exception){
                return new Tuple<int, string>(0, "Error al guardar la imagen");
            }
           

        }
        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\",imageFileName);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}