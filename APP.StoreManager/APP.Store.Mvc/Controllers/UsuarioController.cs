using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using APP.Store.Mvc.Models;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Infra.CrossCutting.Identity.Configuration;
using APP.StoreManager.Infra.CrossCutting.Identity.Model;
using APP.StoreManager.Infra.CrossCutting.MvcFilters;
using Microsoft.AspNet.Identity;
using Empresa = APP.StoreManager.Domain.Entities.Empresa;

namespace APP.Store.Mvc.Controllers
{

    [ClaimsAuthorize("SYS_ADMIN", "True")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly ApplicationUserManager _userManager;
        private readonly IEmpresaAppService _empresaAppService;

        private int _empresaUsuarioLogado;

        public UsuarioController(IUsuarioAppService usuarioAppService, ApplicationUserManager userManager, IEmpresaAppService empresaAppService)
        {
            _usuarioAppService = usuarioAppService;
            _userManager = userManager;
            _empresaAppService = empresaAppService;
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

        private void CriaDropDownEmpresa(bool isAdm)
        {
            if (isAdm)
            {
                var lista = _empresaAppService.GetAll();
                ViewBag.EmpresaId = new SelectList(lista, "Id", "Nome");
            }
            else
            {
                var empresa = _empresaAppService.GetById(_empresaUsuarioLogado);
                var lista = new List<Empresa> { empresa };
                ViewBag.EmpresaId = new SelectList(lista, "Id", "Nome", empresa.Id);
            }

        }

        #endregion
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult List()
        {
            ObtemEmpresaUsuarioLogado();

            if (UsuarioLogadoAdministrador())
            {
                return PartialView("_List", _usuarioAppService.ObterTodos());
            }

            return PartialView("_List", _usuarioAppService.ObterTodos().Where(x => x.EmpresaId == _empresaUsuarioLogado));
        }

        public ActionResult Create()
        {
            CriaDropDownEmpresa(UsuarioLogadoAdministrador());

            return PartialView("_Create");
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmpresaId = model.EmpresaId };
                var result = await _userManager.CreateAsync(user, model.Password);
                string url = Url.Action("Index", "PermissaoAcesso");

                if (result.Succeeded)
                {
                    var mensagem = string.Format("Cadastro do usuário {0} realizado com sucesso!", model.Email);
                    TempData["MessageSuccess"] = mensagem;
                    return Json(new { success = true, url, mensagem });
                }

                AddErrors(result);

                return Json(new { success = false, url });
            }

            // If we got this far, something failed, redisplay form
            return PartialView("_Create", model);
        }


        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _usuarioAppService.ObterPorId(id);
            ViewBag.RoleNames = await _userManager.GetRolesAsync(user.Id);
            ViewBag.Claims = await _userManager.GetClaimsAsync(user.Id);

            return PartialView("_Details", user);
        }


        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _usuarioAppService.ObterPorId(id);
            ViewBag.RoleNames = await _userManager.GetRolesAsync(user.Id);
            ViewBag.Claims = await _userManager.GetClaimsAsync(user.Id);

            var model = new EdicaoUsuarioViewModel()
            {
                Id = user.Id,
                Email = user.Email,
            };

            return PartialView("_Edit", model);
        }

        public ActionResult DesativarLock(string id)
        {
            _usuarioAppService.DesativarLock(id);
            return RedirectToAction("Index");
        }
    }
}