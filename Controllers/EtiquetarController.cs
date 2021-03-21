using Contenedores.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.IO;

namespace Contenedores.Controllers
{
    public class EtiquetarController : Controller
    {
        // GET: Etiquetar
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();

            }
            ViewBag.Imagen = ObtenerimagenSiguiente();
            return View();
        }

        public String ObtenerimagenSiguiente()
        {
            String RouteRoot = Server.MapPath("~/");
            String FolderImages = "Files\\ContenedoresSinEtiquetar";
            String PathImagenesSin = Path.Combine(RouteRoot + FolderImages);
            String PathImgenSiguiente = "";


            if (System.IO.Directory.Exists(PathImagenesSin))
            {
                string[] Files = System.IO.Directory.GetFiles(PathImagenesSin);
                if (Files.First() != null)
                {
                    PathImgenSiguiente = Files.First();
                }
            }

            return PathImgenSiguiente.Substring(RouteRoot.Length-1);
        }
        
        [HttpPost]
        public ActionResult Etiqueta(FileViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            String RouteRoot = Server.MapPath("~/");

            String PathIdFiles = Path.Combine(RouteRoot + "/Files/id.txt");
            String PathEtiquetas = Path.Combine(RouteRoot + "/Files/etiquetas.txt");

            String idFile = "";
            String PathFile;

            // leo el archivo donde estoy guardando el ultimo id
            using (var sr = new StreamReader(PathIdFiles))
            {

                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    idFile = s;
                }
            }

            //sumo +1 al id
            int idFileNum = Int32.Parse(idFile) + 1;
            idFile = idFileNum.ToString();

            //escribo devuelta el archivo con el ultimo id
            using (StreamWriter outputFile = new StreamWriter(PathIdFiles))
            {
                outputFile.WriteLine(idFile);
            }

            String etiqueta = idFile + ";" + model.IsClean.ToString() + ";" + model.IsOpen.ToString() + ";" + model.IsBroken.ToString();
            using (StreamWriter outputFile = new StreamWriter(PathEtiquetas, true))
            {
                outputFile.WriteLine(etiqueta);
            }

            PathFile = Path.Combine(RouteRoot + "/Files/ContenedoresEtiquetados/" + idFile + ".jpg");

            System.IO.File.Move(RouteRoot + ObtenerimagenSiguiente(),PathFile);
            @TempData["Message"] = "Se etiquetó con éxito!! :)";
            return RedirectToAction("Index");
        }

    }
}