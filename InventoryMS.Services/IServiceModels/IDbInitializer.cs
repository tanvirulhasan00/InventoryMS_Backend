using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
