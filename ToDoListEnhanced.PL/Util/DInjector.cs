using Ninject;
using Ninject.Modules;
using ToDoListEnhanced.PL.ViewModels;

namespace ToDoListEnhanced.PL.Util
{
    public class DInjector
    {
        private IKernel kernel;

        public DInjector()
        {
            NinjectModule mainModule = new MainModule();
            kernel = new StandardKernel(mainModule);
        }

        public LoginViewModel GetLoginVM()
        {
            return kernel.Get<LoginViewModel>();
        }

        public MainWorkspaceViewModel GetMainVM()
        {
            return kernel.Get<MainWorkspaceViewModel>();
        }

        public RegistrationViewModel GetRegVM()
        {
            return kernel.Get<RegistrationViewModel>();
        }
    }
}
