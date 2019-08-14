using Ninject.Modules;
using Store.BLL.Interfaces;
using Store.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityStore.Util
{
    public class AdminModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IAdminService>().To<AdminService>();
        }
    }
}