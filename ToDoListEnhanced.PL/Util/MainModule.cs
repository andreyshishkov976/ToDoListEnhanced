using Ninject.Modules;
using ToDoListEnhanced.ClientBLL.DTO;
using ToDoListEnhanced.ClientBLL.Interfaces;
using ToDoListEnhanced.ClientBLL.WebServices;

namespace ToDoListEnhanced.PL.Util
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataWebService<ProjectDTO>>().To<ProjectWebService>();
            Bind<IDataWebService<SubTaskDTO>>().To<SubTaskWebService>();
            Bind<IUserWebService>().To<UserWebService>();
        }
    }
}
