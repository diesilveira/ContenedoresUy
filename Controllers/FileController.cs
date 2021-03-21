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
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View();
        }
        [HttpPost]
        public ActionResult Save(FileViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            } 

            String RouteRoot = Server.MapPath("~/");

            String PathFile = "";

            String PathIdFiles = Path.Combine(RouteRoot + "/Files/id.txt");
            String PathEtiquetas = Path.Combine(RouteRoot + "/Files/etiquetas.txt");

            String idFile = "";


            // leo el archivo donde estoy guardando el ultimo id
            using (var sr = new StreamReader( PathIdFiles))
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
            using (StreamWriter outputFile = new StreamWriter(PathEtiquetas,true))
            {
                outputFile.WriteLine(etiqueta);
            }

            PathFile = Path.Combine(RouteRoot + "/Files/ContenedoresEtiquetados/" + idFile + ".jpg");

            model.File.SaveAs(PathFile);
            @TempData["Message"] = "Se cargó la imagen con éxito!! :)";
            return RedirectToAction("Index");
        }


    }
}