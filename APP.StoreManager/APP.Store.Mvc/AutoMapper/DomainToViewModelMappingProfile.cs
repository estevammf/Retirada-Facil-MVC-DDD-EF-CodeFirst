using APP.Store.Mvc.Models;
using APP.StoreManager.Domain.Entities;
using AutoMapper;

namespace APP.Store.Mvc.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public virtual string ProfileName 
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<PermissaoAcessoViewModel, PermissaoAcesso>();
            Mapper.CreateMap<FuncionarioViewModel, Funcionario>();
            Mapper.CreateMap<EmpresaViewModel, Empresa>();
            Mapper.CreateMap<Caixa, CaixaViewModel>();
            Mapper.CreateMap<Retirada, RetiradaViewModel>();
            Mapper.CreateMap<Rendimento, RendimentoViewModel>();
        }
    }
}
