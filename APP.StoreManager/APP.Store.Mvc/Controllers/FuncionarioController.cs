using System;
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
    [ClaimsAuthorize("FUNCIONARIO_ADMIN", "True")]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioAppService _funcionarioAppService;
        private readonly ICaixaAppService _caixaAppService;
        private readonly ApplicationUserManager _userManager;
        private int _empresaUsuarioLogado;
        public FuncionarioController(IFuncionarioAppService funcionarioAppService, ApplicationUserManager userManager, ICaixaAppService caixaAppService)
        {
            _funcionarioAppService = funcionarioAppService;
            _userManager = userManager;
            _caixaAppService = caixaAppService;
        }

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


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            ObtemEmpresaUsuarioLogado();

            return PartialView("_List", _funcionarioAppService.ObterTodos(_empresaUsuarioLogado));
        }

        // GET: /Funcionario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var funcionario = _funcionarioAppService.GetById((int) id);
            var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);

            if (funcionarioViewModel == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Details", funcionarioViewModel);
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionarioViewModel funcionarioViewModel)
        {
            if (ModelState.IsValid)
            {
                ObtemEmpresaUsuarioLogado();

                var funcionario = Mapper.Map<FuncionarioViewModel, Funcionario>(funcionarioViewModel);
                funcionario.EmpresaId = _empresaUsuarioLogado;
                _funcionarioAppService.Add(funcionario);

                var mensagem = string.Format("Cadatro do funcionário {0} realizado com sucesso!", funcionarioViewModel.Nome);
                TempData["MessageSuccess"] = mensagem;

                string url = Url.Action("Index", "Funcionario");
                return Json(new { success = true, url, mensagem });
            }

            return PartialView("_Create", funcionarioViewModel);
        }

        // GET: /Funcionario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var funcionario = _funcionarioAppService.GetById((int)id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }

            var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);
            return PartialView("_Edit", funcionarioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FuncionarioViewModel funcionarioViewModel)
        {
            if (ModelState.IsValid)
            {
                ObtemEmpresaUsuarioLogado();

                var funcionario = Mapper.Map<FuncionarioViewModel, Funcionario>(funcionarioViewModel);
                funcionario.EmpresaId = _empresaUsuarioLogado;
                _funcionarioAppService.Update(funcionario);

                var mensagem = string.Format("Atualização do funcionário {0} realizada com sucesso!", funcionarioViewModel.Nome);
                TempData["MessageSuccess"] = mensagem;

                string url = Url.Action("Index", "Funcionario");
                return Json(new { success = true, url, mensagem });
            }

            return PartialView("_Edit", funcionarioViewModel);
        }

        // GET: /Funcionario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var funcionario = _funcionarioAppService.GetById((int)id);
            var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>(funcionario);

            if (funcionarioViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", funcionarioViewModel);
        }

        // POST: /Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var caixa = _caixaAppService.Find(x => x.FuncionarioId == id).FirstOrDefault();
            string url = Url.Action("Index", "Funcionario");
            if (caixa != null)
            {
                const string mensagemErro = "Esse funcionário não pode ser removido, pois já possui associação com um Caixa.";
                TempData["MessageError"] = mensagemErro;

                string index = Url.Action("Index", "Funcionario");
                return Json(new { success = false, url, mensagemErro });
            }

            var funcionario = _funcionarioAppService.GetById(id);
            _funcionarioAppService.Remove(funcionario);

            var mensagem = string.Format("Cadatro do funcionário {0} realizada com sucesso!", funcionario.Nome);
            TempData["MessageSuccess"] = mensagem;
            
            return Json(new { success = true, url, mensagem });
        }
    }
}
