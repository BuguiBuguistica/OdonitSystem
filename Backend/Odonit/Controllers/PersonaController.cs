using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odonit.DAL;

namespace Odonit.Controllers
{
    public class PersonaController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Persona/
        public ActionResult Index()
        {
            var personas = unitOfWork.PersonaRepository.Get();
            return View(personas.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
	}
}