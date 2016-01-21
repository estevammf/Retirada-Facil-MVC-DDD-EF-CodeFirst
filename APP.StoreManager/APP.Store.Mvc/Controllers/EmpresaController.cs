using System.Net;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using APP.Store.Mvc.Models;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Infra.CrossCutting.MvcFilters;
using AutoMapper;
using APP.StoreManager.Domain.Entities;

namespace APP.Store.Mvc.Controllers
{
    [ClaimsAuthorize("SYS_ADMIN", "True")]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaAppService _empresaAppService;
        public EmpresaController(IEmpresaAppService empresaAppService)
        {
            _empresaAppService = empresaAppService;
        }
        
        //
        // GET: /Empresa/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", _empresaAppService.GetAll());
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpresaViewModel empresaViewModel)
        {
            if (ModelState.IsValid)
            {
                var empresa = Mapper.Map<EmpresaViewModel, Empresa>(empresaViewModel);
                _empresaAppService.Add(empresa);
                var mensagem = string.Format("Cadastro da empresa {0} realizado com sucesso!", empresaViewModel.Nome);
                TempData["MessageSuccess"] = mensagem;

                string url = Url.Action("Index", "Empresa");
                return Json(new { success = true, url, mensagem });
            }

            return PartialView("_Create", empresaViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = _empresaAppService.GetById((int)id);
            var empresaViewModel = Mapper.Map<Empresa, EmpresaViewModel>(empresa);

            if (empresaViewModel == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Details", empresaViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = _empresaAppService.GetById((int)id);
            if (empresa == null)
            {
                return HttpNotFound();
            }

            var empresaViewModel = Mapper.Map<Empresa, EmpresaViewModel>(empresa);
            return PartialView("_Edit", empresaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpresaViewModel empresaViewModel)
        {
            if (ModelState.IsValid)
            {
                var empresa = Mapper.Map<EmpresaViewModel, Empresa>(empresaViewModel);
                
                _empresaAppService.Update(empresa);

                var mensagem = string.Format("Atualização da empresa {0} realizada com sucesso!", empresaViewModel.Nome);
                TempData["MessageSuccess"] = mensagem;

                string url = Url.Action("Index", "Empresa");
                return Json(new { success = true, url, mensagem });
            }

            return PartialView("_Edit", empresaViewModel);
        }

        // GET: /Funcionario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empresa = _empresaAppService.GetById((int)id);
            var empresaViewModel = Mapper.Map<Empresa, EmpresaViewModel>(empresa);

            if (empresaViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", empresaViewModel);
        }

        // POST: /Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var empresa = _empresaAppService.GetById(id);
            _empresaAppService.Remove(empresa);

            var mensagem = string.Format("Remoção da empresa {0} realizada com sucesso!", empresa.Nome);
            TempData["MessageSuccess"] = mensagem;

            string url = Url.Action("Index", "Empresa");
            return Json(new { success = true, url, mensagem });
        }
	}
}