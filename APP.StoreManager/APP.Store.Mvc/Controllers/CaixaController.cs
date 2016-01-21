using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using APP.Store.Mvc.Models;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Infra.CrossCutting.Identity.Configuration;
using APP.StoreManager.Infra.CrossCutting.MvcFilters;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace APP.Store.Mvc.Controllers
{
    [ClaimsAuthorize("CAIXA_ADMIN", "True")]
    public class CaixaController : Controller
    {
        private readonly ICaixaAppService _caixaAppService;
        private readonly ApplicationUserManager _userManager;
        private readonly IFuncionarioAppService _funcionarioAppService;
        private int _empresaUsuarioLogado;
        public CaixaController(ICaixaAppService caixaAppService, ApplicationUserManager userManager, IFuncionarioAppService funcionarioAppService)
        {
            _caixaAppService = caixaAppService;
            _userManager = userManager;
            _funcionarioAppService = funcionarioAppService;
        }

        #region Helper
        private void ObtemEmpresaUsuarioLogado()
        {
            if (User != null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                if (claimsIdentity != null)
                {
                    var claimEmpresaUsuario = claimsIdentity.FindFirst(x => x.Type.Equals("EmpresaUsuarioLogado"));
                    if (claimEmpresaUsuario != null)
                    {
                        _empresaUsuarioLogado = Convert.ToInt32(claimEmpresaUsuario.Value);
                    }
                    else
                    {
                        var user = _userManager.FindByName(User.Identity.Name);
                        _empresaUsuarioLogado = user.EmpresaId;
                        claimsIdentity.AddClaim(new Claim("EmpresaUsuarioLogado", user.EmpresaId.ToString(CultureInfo.InvariantCulture)));
                        var ctx = Request.GetOwinContext();
                        var authenticationManager = ctx.Authentication;
                        authenticationManager.SignIn(claimsIdentity);
                    }
                }

            }
        }

        #endregion

        // GET: /Caixa/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            ObtemEmpresaUsuarioLogado();
            ViewBag.FuncionarioId = new SelectList(_funcionarioAppService.ObterTodos(_empresaUsuarioLogado), "Id", "Nome");

            var caixas = _caixaAppService.ObterTodos(_empresaUsuarioLogado);

            return PartialView("_List", caixas.ToList());
        }

        // GET: /Caixa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var caixa = _caixaAppService.GetById((int)id);
            if (caixa == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", caixa);
        }

        public ActionResult Create()
        {
            ObtemEmpresaUsuarioLogado();
            ViewBag.FuncionarioId = new SelectList(_funcionarioAppService.ObterTodos(_empresaUsuarioLogado), "Id", "Nome");
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CaixaViewModel caixaViewModel)
        {
            if (ModelState.IsValid)
            {
                var caixa = Mapper.Map<CaixaViewModel, Caixa>(caixaViewModel);
                ObtemEmpresaUsuarioLogado();
                caixa.EmpresaId = _empresaUsuarioLogado;
                _caixaAppService.Add(caixa);

                var mensagem = string.Format("Cadastro do caixa {0} realizado com sucesso!", caixaViewModel.NumeroIdentificador);
                TempData["MessageSuccess"] = mensagem;
             
                string url = Url.Action("Index", "Caixa");
                return Json(new { success = true, url = url, mensagem });
            }

            return PartialView("_Create", caixaViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var caixa = _caixaAppService.GetById((int)id);
            if (caixa == null)
            {
                return HttpNotFound();
            }

            ObtemEmpresaUsuarioLogado();
            ViewBag.FuncionarioId = new SelectList(_funcionarioAppService.ObterTodos(_empresaUsuarioLogado), "Id", "Nome", caixa.FuncionarioId);

            var caixaViewModel = Mapper.Map<Caixa, CaixaViewModel>(caixa);

            return PartialView("_Edit", caixaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CaixaViewModel caixaViewModel)
        {
            if (ModelState.IsValid)
            {
                var caixa = Mapper.Map<CaixaViewModel, Caixa>(caixaViewModel);
                ObtemEmpresaUsuarioLogado();
                caixa.EmpresaId = _empresaUsuarioLogado;
                _caixaAppService.Update(caixa);


                var mensagem = string.Format("Atualização do caixa {0} realizada com sucesso!", caixaViewModel.NumeroIdentificador);
                TempData["MessageSuccess"] = mensagem;

                string url = Url.Action("Index", "Caixa");
                return Json(new { success = true, url = url, mensagem });
            }

            return PartialView("_Edit", caixaViewModel);
        }

        // GET: /Caixa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var caixa = _caixaAppService.GetById((int)id);
            if (caixa == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", caixa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var caixa = _caixaAppService.AsQueryable().Where(x => x.Id == id).Include(x => x.Retiradas).FirstOrDefault();
            string url = Url.Action("Index", "Caixa");
            if (caixa != null)
            {
                if (caixa.Retiradas.Any())
                {
                    const string mensagemErro =
                        "Esse caixa não pode ser removido, pois existem retiradas associadas ao mesmo.";
                    TempData["MessageError"] = mensagemErro;

                   
                    return Json(new { sucess = false, url, mensagem = mensagemErro });
                }

                
                _caixaAppService.Remove(caixa);

                var mensagem = string.Format("Remoção do caixa {0} realizada com sucesso!", caixa.NumeroIdentificador);
                TempData["MessageSuccess"] = mensagem;

               
                return Json(new { success = true, url, mensagem });

            }

            return Json(new { success = true, url });
        }

    }
}
