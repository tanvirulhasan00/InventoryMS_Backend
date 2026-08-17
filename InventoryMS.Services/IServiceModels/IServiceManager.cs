using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels
{
    public interface IServiceManager
    {
        Task<int> Save();
        public IAuthService AuthService { get; }
    }
}
