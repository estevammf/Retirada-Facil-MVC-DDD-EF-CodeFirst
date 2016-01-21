using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using APP.Store.Mvc.Helper;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Enum;
using APP.StoreManager.Infra.CrossCutting.Identity.Configuration;
using APP.StoreManager.Infra.CrossCutting.MvcFilters;
using AutoMapper;
using Microsoft.AspNet.Identity;
using APP.StoreManager.Domain.Entities;
using APP.Store.Mvc.Models;

namespace APP.Store.Mvc.Controllers
{
    [ClaimsAuthorize("RENDIMENTO_ADMIN", "True")]
    public class RendimentoController : Controller
    {
        private readonly IRendimentoAppService _rendimentoAppService;
        private readonly ApplicationUserManager _userManager;
        private int _empresaUsuarioLogado;

        public RendimentoController(IRendimentoAppService rendimentoAppService, ApplicationUserManager userManager)
        {
            _rendimentoAppService = rendimentoAppService;
            _userManager = userManager;
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

        private decimal TotalRendimentos(Rendimento rendimento)
        {
            decimal valorTipoTransacao = 0;

            if (rendimento.Quantidade > 0)
            {
                switch (rendimento.TipoTransacao)
                {
                    case TipoTransacaoBanesfacilEnum.Deposito:
                        if (decimal.TryParse(Resources.BaseCalculo.Deposito, out valorTipoTransacao))
                        {
                            valorTipoTransacao = rendimento.Quantidade * valorTipoTransacao;
                        }

                        break;

                    case TipoTransacaoBanesfacilEnum.RecebimentoConta:
                        if (rendimento.Quantidade > 5000)
                        {
                            if (decimal.TryParse(Resources.BaseCalculo.RecebimentoContaAcimaCincoMil, out valorTipoTransacao))
                            {
                                valorTipoTransacao = rendimento.Quantidade * valorTipoTransacao;
                            }
                        }
                        else
                        {
                            if (decimal.TryParse(Resources.BaseCalculo.RecebimentoContaAbaixoCincoMil, out valorTipoTransacao))
                            {
                                valorTipoTransacao = rendimento.Quantidade * valorTipoTransacao;
                            }
                        }
                        break;
                    case TipoTransacaoBanesfacilEnum.RecebimentoEscelsa:
                        if (decimal.TryParse(Resources.BaseCalculo.RecebimentoEscelsa, out valorTipoTransacao))
                        {
                            valorTipoTransacao = rendimento.Quantidade * valorTipoTransacao;
                        }
                        break;
                    case TipoTransacaoBanesfacilEnum.Saques:
                        if (rendimento.Quantidade > 5000)
                        {
                            if (decimal.TryParse(Resources.BaseCalculo.SaqueQtdeAcimaCincoMil, out valorTipoTransacao))
                            {
                                valorTipoTransacao = rendimento.Quantidade * valorTipoTransacao;
                            }
                        }
                        else
                        {
                            if (decimal.TryParse(Resources.BaseCalculo.SaqueQtdeAbaixoCincoMil, out valorTipoTransacao))
                            {
                                valorTipoTransacao = rendimento.Quantidade * valorTipoTransacao;
                            }
                        }
                        break;
                }
            }

            return valorTipoTransacao;
        }

        #endregion

        // GET: Rendimento
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAnual()
        {
            ObtemEmpresaUsuarioLogado();

            var rendimentosTodos = _rendimentoAppService.ObtemRendimentos(_empresaUsuarioLogado).ToList();
            var rendimentosAno = rendimentosTodos.Where(x => x.DataReferencia.Year == TimeZoneHelper.DataAtualBrasil().Year).ToList();
            var rendimentosMes = rendimentosTodos.Where(x => x.DataReferencia.Month == TimeZoneHelper.DataAtualBrasil().Month).ToList();

            var rendimentosMensais = new List<TotalRendimentoMesViewModel>();

            ViewBag.TotalRendimentosAno = _rendimentoAppService.TotalRendimentos(rendimentosAno).ToString("C");
            ViewBag.TotalRendimentosEmpresa = _rendimentoAppService.TotalRendimentos(rendimentosTodos).ToString("C");
            ViewBag.TotalRendimentosMes = _rendimentoAppService.TotalRendimentos(rendimentosMes).ToString("C");

            if (rendimentosAno.Any())
            {
                var quantidadeMeses = rendimentosAno.Max(x => x.DataReferencia.Month);

                for (int i = 1; i <= quantidadeMeses; i++)
                {
                    var rendimentos = rendimentosAno.Where(x => x.DataReferencia.Month == i).ToList();
                    var totalRendimentosMesI = _rendimentoAppService.TotalRendimentos(rendimentos);

                    rendimentosMensais.Add(new TotalRendimentoMesViewModel
                    {
                        Total = totalRendimentosMesI,
                        DataReferencia = new DateTime(TimeZoneHelper.DataAtualBrasil().Year, i, 1)
                    });
                }
               }

            return PartialView("_ListAnual", rendimentosMensais);
        }

        public ActionResult List(DateTime? data)
        {
           
            ObtemEmpresaUsuarioLogado();

            if (data == null)
                data = TimeZoneHelper.DataAtualBrasil();

            ViewBag.Mes = data.Value.ToString("MMMM", CultureInfo.CurrentCulture);
            var rendimentos = _rendimentoAppService.ObtemRendimentoMensal(_empresaUsuarioLogado, data.Value.Month, data.Value.Year).ToList();
            var rendimentosMesViewModel = Mapper.Map<IEnumerable<Rendimento>, IEnumerable<RendimentoViewModel>>(rendimentos);

            
            return PartialView("_List", rendimentosMesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RendimentoViewModel rendimentoViewModel)
        {
            if (ModelState.IsValid)
            {
                ObtemEmpresaUsuarioLogado();
                DateTime data;
                if (DateTime.TryParse(rendimentoViewModel.DataReferenciaString, out data))
                {
                    rendimentoViewModel.DataReferencia = data;
                    rendimentoViewModel.EmpresaId = _empresaUsuarioLogado;
                    

                    var rendimento = Mapper.Map<RendimentoViewModel, Rendimento>(rendimentoViewModel);

                    var tipoRendimentoExiste =
                        _rendimentoAppService.Find(
                            x =>
                                x.TipoTransacao == rendimento.TipoTransacao &&
                                x.DataReferencia.Month == rendimento.DataReferencia.Month &&
                                x.DataReferencia.Year == rendimento.DataReferencia.Year).FirstOrDefault();

                    if (tipoRendimentoExiste != null)
                    {
                        tipoRendimentoExiste.Quantidade = tipoRendimentoExiste.Quantidade + rendimento.Quantidade;
                        tipoRendimentoExiste.TotalRendimento = TotalRendimentos(tipoRendimentoExiste);
                        _rendimentoAppService.Update(tipoRendimentoExiste);
                    }
                    else
                    {
                        rendimento.TotalRendimento = TotalRendimentos(rendimento);
                        _rendimentoAppService.Add(rendimento);
                    }

                    var mensagem = string.Format("Novo rendimento para o mês de  {0} adicionado!", data.ToString("MMMM", CultureInfo.CurrentCulture));
                    TempData["MessageSuccess"] = mensagem;

                    return RedirectToAction("Index");
                }
            }

            return PartialView("_Create", rendimentoViewModel);
        }
    }
}