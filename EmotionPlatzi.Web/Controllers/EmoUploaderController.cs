using EmotionPlatzi.Web.Models;
using EmotionPlatzi.Web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.Web.Controllers
{
    public class EmoUploaderController : Controller
    {
        string serverFolderPath;
        EmotionHelper emoHelper;
        string key;
        EmotionPlatziWebContext db = new EmotionPlatziWebContext();

        public EmoUploaderController()
        {
            serverFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            key = ConfigurationManager.AppSettings["EMOTION_KEY"];
            emoHelper = new EmotionHelper(key);
        }
        // GET: EmoUploader
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            //if(file != null && file.ContentLength > 0)
            if (file?.ContentLength > 0)
            {
                //Cadena aleatoria
                var pictureName = Guid.NewGuid().ToString();
                pictureName += Path.GetExtension(file.FileName);
                //Mapea ruta del servidor a ruta local
                var route = Server.MapPath(serverFolderPath);
                route = route + "/" + pictureName;
                file.SaveAs(route);

                var emoPicture = await emoHelper.DetectAndExtracFacesAsync(file.InputStream);
                emoPicture.Name = file.FileName;
                //Path para la web (relativo)
                //interpolacion de cadenas
                emoPicture.Path = $"{serverFolderPath}/{pictureName}";
                //emoPicture.Path = serverFolderPath + "/" + pictureName;
                //ALmacenar en BD Datacontex?
                db.EmoPictures.Add(emoPicture);
                await db.SaveChangesAsync();

                return RedirectToAction("Details", "EmoPictures", new { Id = emoPicture.Id});
            }
                return View();
        }
    }
}