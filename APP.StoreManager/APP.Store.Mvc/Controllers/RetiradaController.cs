using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using APP.Store.Mvc.Helper;
using APP.Store.Mvc.Models;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Entities;
using APP.StoreManager.Infra.CrossCutting.Identity.Configuration;
using APP.StoreManager.Infra.CrossCutting.MvcFilters;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace APP.Store.Mvc.Controllers
{
    [ClaimsAuthorize("RETIRADA_ADMIN", "True")]
    public class RetiradaController : Controller
    {
        private int _empresaUsuarioLogado;
        private readonly ApplicationUserManager _userManager;
        private readonly IRetiradaAppService _retiradaAppService;
        private readonly ICaixaAppService _caixaAppService;
        private readonly IEmpresaAppService _empresaAppService;
        private DateTime _dataOperacao;
        public RetiradaController(IRetiradaAppService retiradaAppService, ICaixaAppService caixaAppService, ApplicationUserManager userManager, IEmpresaAppService empresaAppService)
        {
            _retiradaAppService = retiradaAppService;
            _caixaAppService = caixaAppService;
            _userManager = userManager;
            _empresaAppService = empresaAppService;
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

        public void MontaTelaInicialRetirada()
        {
            
            var caixaList = _caixaAppService.ObterTodos(_empresaUsuarioLogado, _dataOperacao).ToList();

            var caixaViewModelList = Mapper.Map<IEnumerable<Caixa>, IEnumerable<CaixaViewModel>>(caixaList);

            var culture = new CultureInfo("pt-BR");

            TempData["DataOperacao"] = _dataOperacao;
            ViewBag.DataOperacao = _dataOperacao.ToShortDateString();
            ViewBag.DiaOperacao = culture.DateTimeFormat.GetDayName(_dataOperacao.DayOfWeek);

            ViewBag.TotalDoDia = caixaViewModelList.Aggregate<CaixaViewModel, decimal>(0, (current, caixaViewModel) => current + caixaViewModel.TotalRetiradas).ToString("C");
            ViewBag.Caixas = caixaViewModelList;
            ViewBag.CaixaId = new SelectList(caixaList, "Id", "NumeroIdentificador");


        }


        // GET: /Retirada/
        public ActionResult Index(DateTime? dataOperacaoFilter)
        {
            if (dataOperacaoFilter != null)
                _dataOperacao = (DateTime) dataOperacaoFilter;
            else
                _dataOperacao = _caixaAppService.ObtemDataOperacao(TimeZoneHelper.DataAtualBrasil());

            ObtemEmpresaUsuarioLogado();
            MontaTelaInicialRetirada();

            var retiradas = _retiradaAppService.ObtemRetiradasPorData(_dataOperacao, _empresaUsuarioLogado);
            var retiradasViewModel = Mapper.Map<IEnumerable<Retirada>, IEnumerable<RetiradaViewModel>>(retiradas);
            return View(retiradasViewModel);
        }

        public ActionResult ObtemRetiradasViaFiltro(string data)
        {
            DateTime dataParam;
            if (DateTime.TryParse(data, out dataParam))
            {
                return RedirectToAction("Index", new { dataOperacaoFilter = dataParam });
            }

            TempData["MessageError"] = "Ops! Ocorreu um erro ao tentar realizar a pesquisa! Parece que você não inseriu uma data válida";
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RetiradaViewModel retiradaViewModel)
        {
            if (ModelState.IsValid)
            {
                var envelopeInserido =
                    _retiradaAppService.Find(
                        x => x.NumeroEnvelope.ToLower().Equals(retiradaViewModel.NumeroEnvelope.ToLower())).FirstOrDefault();

                if (envelopeInserido != null)
                {
                    TempData["MessageError"] = string.Format("Já existe uma retirada para o envelope {0}!", retiradaViewModel.NumeroEnvelope);
                    return RedirectToAction("Index");
                }


                decimal valorConvertido;
                retiradaViewModel.DataHoraRegistro = _caixaAppService.ObtemDataOperacao(TimeZoneHelper.DataAtualBrasil());
                retiradaViewModel.ValorString = retiradaViewModel.ValorString.Replace("R$ ", "");
                if (Decimal.TryParse(retiradaViewModel.ValorString, out valorConvertido))
                    retiradaViewModel.Valor = valorConvertido;

                var retirada = Mapper.Map<RetiradaViewModel, Retirada>(retiradaViewModel);
                _retiradaAppService.Add(retirada);

                var mensagem = string.Format("Retirada do envelope {0} realizada com sucesso!", retiradaViewModel.NumeroEnvelope);
                TempData["MessageSuccess"] = mensagem;

                return RedirectToAction("Index");
            }

            MontaTelaInicialRetirada();
            return PartialView("_Create", retiradaViewModel);
        }

        [ClaimsAuthorize("EDITAR_RETIRADA", "True")]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var retirada = _retiradaAppService.GetById((int)id);
            var retiradaViewModel = Mapper.Map<Retirada, RetiradaViewModel>(retirada);

            if (retirada == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Edit", retiradaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RetiradaViewModel retiradaViewModel)
        {
            string index = Url.Action("Index", "Retirada");
            if (ModelState.IsValid)
            {
                //var envelopeInserido =
                //   _retiradaAppService.Find(
                //       x => x.NumeroEnvelope.ToLower().Equals(retiradaViewModel.NumeroEnvelope.ToLower())).FirstOrDefault();

                //if (envelopeInserido != null)
                //{
                //    if (envelopeInserido.Id != retiradaViewModel.Id)
                //    {
                //        var mensagemErro = string.Format("Já existe uma retirada para o envelope {0}!",
                //            retiradaViewModel.NumeroEnvelope);
                //        TempData["MessageError"] = mensagemErro;
                       
                //        return Json(new { sucess = false, url = index, mensagem = mensagemErro });
                //    }
                //}


                decimal valorConvertido;
                retiradaViewModel.DataHoraRegistro = _caixaAppService.ObtemDataOperacao(TimeZoneHelper.DataAtualBrasil());
                retiradaViewModel.ValorString = retiradaViewModel.ValorString.Replace("R$ ", "");
                if (Decimal.TryParse(retiradaViewModel.ValorString, out valorConvertido))
                    retiradaViewModel.Valor = valorConvertido;

                var retirada = Mapper.Map<RetiradaViewModel, Retirada>(retiradaViewModel);
                _retiradaAppService.Update(retirada);

                var mensagem = string.Format("Retirada do envelope {0} atualizada com sucesso!", retiradaViewModel.NumeroEnvelope);
                TempData["MessageSuccess"] = mensagem;

                return Json(new { sucess = true, url = index, mensagem });
            }

            return PartialView("_Edit", retiradaViewModel);
        }

        [ClaimsAuthorize("REMOVER_RETIRADA", "True")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var retirada = _retiradaAppService.GetById((int)id);

            if (retirada == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", retirada);
        }

        // POST: /Retirada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var retirada = _retiradaAppService.GetById(id);
            _retiradaAppService.Remove(retirada);

            var mensagem = string.Format("Retirada do envelope {0} removida com sucesso!", retirada.NumeroEnvelope);
            TempData["MessageSuccess"] = mensagem;

            string index = Url.Action("Index", "Retirada");
            return Json(new { sucess = true, url = index, mensagem });
        }

        //public ActionResult ImprimeRetiradas()
        //{
        //    return RedirectToAction("RelatorioRetirada");
        //}

        //[ChildActionOnly]
        public ActionResult RelatorioRetirada()
        {
            if (TempData["DataOperacao"] != null)
            {
                _dataOperacao = (DateTime) TempData["DataOperacao"];

            }
            else
            {
                TempData["MessageError"] = "Você tentou gerar um relatório de retiradas sem informar a data de operação.";
            }

            ObtemEmpresaUsuarioLogado();
            MontaTelaInicialRetirada();

            var empresa = _empresaAppService.GetById(_empresaUsuarioLogado);
            var empresaViewModel = Mapper.Map<Empresa, EmpresaViewModel>(empresa);

            ViewBag.DadosEmpresa = empresaViewModel;
            //ViewBag.EmpresaCNPJ = empresaViewModel.Cnpj;
            //ViewBag.Empres
            var retiradas = _retiradaAppService.ObtemRetiradasPorData(_dataOperacao, _empresaUsuarioLogado);
            var retiradasViewModel = Mapper.Map<IEnumerable<Retirada>, IEnumerable<RetiradaViewModel>>(retiradas);
            return View(retiradasViewModel);
        }
    }
    
}
