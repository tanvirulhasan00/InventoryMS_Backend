using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels
{
    public interface IChecker
    {
        Task<bool> IsDatabaseConnectedAsync(string conStr);
    }
}
