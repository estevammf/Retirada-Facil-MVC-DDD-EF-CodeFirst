using APP.StoreManager.Application;
using APP.StoreManager.Application.Interface;
using APP.StoreManager.Domain.Interfaces.Repositories;
using APP.StoreManager.Domain.Interfaces.Services;
using APP.StoreManager.Domain.Services;
using APP.StoreManager.Infra.CrossCutting.Identity.Configuration;
using APP.StoreManager.Infra.CrossCutting.Identity.Context;
using APP.StoreManager.Infra.CrossCutting.Identity.Model;
using APP.StoreManager.Infra.Data.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;

namespace APP.StoreManager.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));

            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            //app services
            container.Register<IProdutoAppService, ProdutoAppService>(Lifestyle.Scoped);
            container.Register<IUsuarioAppService, UsuarioAppService>(Lifestyle.Scoped);
            container.Register<IFuncionarioAppService, FuncionarioAppService>(Lifestyle.Scoped);
            container.Register<IEmpresaAppService, EmpresaAppService>(Lifestyle.Scoped);
            container.Register<IPermissaoAcessoAppService, PermissaoAcessoAppService>(Lifestyle.Scoped);
            container.Register<IRetiradaAppService, RetiradaAppService>(Lifestyle.Scoped);
            container.Register<IFechamentoCaixaAppService, FechamentoCaixaAppService>(Lifestyle.Scoped);
            container.Register<ICaixaAppService, CaixaAppService>(Lifestyle.Scoped);
            container.Register<IRendimentoAppService, RendimentoAppService>(Lifestyle.Scoped);

            //services
            container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
            container.Register<IUsuarioService, UsuarioService>(Lifestyle.Scoped);
            container.Register<IFuncionarioService, FuncionarioService>(Lifestyle.Scoped);
            container.Register<IEmpresaService, EmpresaService>(Lifestyle.Scoped);
            container.Register<IPermissaoAcessoService, PermissaoAcessoService>(Lifestyle.Scoped);
            container.Register<IRetiradaService, RetiradaService>(Lifestyle.Scoped);
            container.Register<IFechamentoCaixaService, FechamentoCaixaService>(Lifestyle.Scoped);
            container.Register<ICaixaService, CaixaService>(Lifestyle.Scoped);
            container.Register<IRendimentoService, RendimentoService>(Lifestyle.Scoped);

            //repositorios
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
            container.Register<IFuncionarioRepository, FuncionarioRepository>(Lifestyle.Scoped);
            container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<IEmpresaRepository, EmpresaRepository>(Lifestyle.Scoped);
            container.Register<IPermissaoAcessoRepository, PermissaoAcessoRepository>(Lifestyle.Scoped);
            container.Register<IRetiradaRepository, RetiradaRepository>(Lifestyle.Scoped);
            container.Register<IFechamentoCaixaRepository, FechamentoCaixaRepository>(Lifestyle.Scoped);
            container.Register<ICaixaRepository, CaixaRepository>(Lifestyle.Scoped);
            container.Register<IRendimentoRepository, RendimentoRepository>(Lifestyle.Scoped);

          
        } 
    }
}
