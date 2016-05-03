using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odonit.DAL;

namespace Odonit.Controllers
{
    public class ContactoController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Contacto/
        public ActionResult Index()
        {
            var contactos = unitOfWork.ContactoRepository.Get();
            return View(contactos.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
	}
}