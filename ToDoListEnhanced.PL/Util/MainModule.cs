using Ninject.Modules;
using ToDoListEnhanced.BLL.DTO;
using ToDoListEnhanced.BLL.Interfaces;
using ToDoListEnhanced.BLL.Services;

namespace ToDoListEnhanced.PL.Util
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataService<ProjectDTO>>().To<ProjectService>();
            Bind<IDataService<SubTaskDTO>>().To<SubTaskService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
