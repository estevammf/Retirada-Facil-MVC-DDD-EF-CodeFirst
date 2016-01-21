using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using APP.Store.Mvc.Models;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Infra.CrossCutting.Identity.Configuration;
using APP.StoreManager.Infra.CrossCutting.MvcFilters;
using AutoMapper;
using Microsoft.AspNet.Identity;
using APP.StoreManager.Domain.Entities;

namespace APP.Store.Mvc.Controllers
{
     [ClaimsAuthorize("SYS_ADMIN", "True")]
    public class PermissaoAcessoController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IPermissaoAcessoAppService _permissaoAcessoAppService;
        public PermissaoAcessoController(ApplicationUserManager userManager, IPermissaoAcessoAppService permissaoAcessoAppService)
        {
            _permissaoAcessoAppService = permissaoAcessoAppService;
            _userManager = userManager;
        }

         public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            var lista = _permissaoAcessoAppService.GetAll();

            return PartialView("_List", lista);
        }
        
        public ActionResult SetPermissaoAcesso(string id)
        {
            ViewBag.Type = new SelectList
                (
                   _permissaoAcessoAppService.Find(x=> !x.Name.Equals("SYS_ADMIN")),
                    "Name",
                    "Name"
                );

            ViewBag.User = _userManager.FindById(id);

            return View(); 
        }

        // POST: ClaimsAdmin/SetUserClaim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPermissaoAcesso(UsuarioPermissaoAcessoViewModel permissaoAcesso, string id)
        {
            try
            {
                await _userManager.AddClaimAsync(id, new Claim(permissaoAcesso.Type, permissaoAcesso.Value));

                return RedirectToAction("Details","Usuario", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PermissaoAcessoViewModel permissaoAcessoViewModel)
        {
           
                if (ModelState.IsValid)
                {
                    var permissao = Mapper.Map<PermissaoAcessoViewModel, PermissaoAcesso>(permissaoAcessoViewModel);
                    _permissaoAcessoAppService.Add(permissao);

                    var mensagem = string.Format("Cadastro da permissão {0} realizado com sucesso!", permissaoAcessoViewModel.Name);
                    TempData["MessageSuccess"] = mensagem;

                    string url = Url.Action("Index", "PermissaoAcesso");
                    return Json(new { success = true, url, mensagem });
                }

            return PartialView("_Create", permissaoAcessoViewModel);

        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var permissaoAcesso = _permissaoAcessoAppService.ObtemPermissaoAcesso(id);
            if (permissaoAcesso == null)
            {
                return HttpNotFound();
            }

            var permissaoAcessoViewModel = Mapper.Map<PermissaoAcesso, PermissaoAcessoViewModel>(permissaoAcesso);
            return PartialView("_Edit", permissaoAcessoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PermissaoAcessoViewModel permissaoAcessoViewModel)
        {
            if (ModelState.IsValid)
            {

                var permissaoAcesso = Mapper.Map<PermissaoAcessoViewModel, PermissaoAcesso>(permissaoAcessoViewModel);

                _permissaoAcessoAppService.Update(permissaoAcesso);

                var mensagem = string.Format("Atualização da permissão {0} realizada com sucesso!", permissaoAcessoViewModel.Name);
                TempData["MessageSuccess"] = mensagem;

                string url = Url.Action("Index", "PermissaoAcesso");
                return Json(new { success = true, url, mensagem });
            }

            return PartialView("_Edit", permissaoAcessoViewModel);
        }

        // GET: /Funcionario/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var permissaoAcesso = _permissaoAcessoAppService.ObtemPermissaoAcesso(id);
            var permissaoAcessoViewModel = Mapper.Map<PermissaoAcesso, PermissaoAcessoViewModel>(permissaoAcesso);

            if (permissaoAcessoViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", permissaoAcessoViewModel);
        }

        // POST: /Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var permissaoAcesso = _permissaoAcessoAppService.ObtemPermissaoAcesso(id);
            _permissaoAcessoAppService.Remove(permissaoAcesso);

            var mensagem = string.Format("Remoção da permissão {0} realizada com sucesso!", permissaoAcesso.Name);
            TempData["MessageSuccess"] = mensagem;

            string url = Url.Action("Index", "PermissaoAcesso");
            return Json(new { success = true, url, mensagem });
        }

    }
}
