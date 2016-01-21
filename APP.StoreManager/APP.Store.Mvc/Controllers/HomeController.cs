using System.Security.Claims;
using System.Web.Mvc;

namespace APP.Store.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private bool UsuarioLogadoAdministrador()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var claimSysAdm = claimsIdentity.FindFirst(c => c.Type == "SYS_ADMIN");
                return claimSysAdm != null;
            }

            return false;
        }

        public ActionResult Index()
        {
            if (UsuarioLogadoAdministrador())
            {
                return RedirectToAction("Index", "Empresa");
            }

            return RedirectToAction("Index", "Retirada");
            //return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}