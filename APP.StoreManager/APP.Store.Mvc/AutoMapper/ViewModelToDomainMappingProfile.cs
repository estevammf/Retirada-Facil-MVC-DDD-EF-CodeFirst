using System.Collections.Generic;
using APP.Store.Mvc.Models;
using APP.StoreManager.Domain.Entities;
using AutoMapper;

namespace APP.Store.Mvc.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public virtual string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<PermissaoAcesso, PermissaoAcessoViewModel>();
            Mapper.CreateMap<Funcionario, FuncionarioViewModel>();
            Mapper.CreateMap<Empresa, EmpresaViewModel>();
            Mapper.CreateMap<CaixaViewModel, Caixa>();
            Mapper.CreateMap<RetiradaViewModel, Retirada>();
            Mapper.CreateMap<RendimentoViewModel, Rendimento>();
        }
    }
}
