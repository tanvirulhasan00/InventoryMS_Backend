using InventoryMS.Services.IServiceModels.IProductServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.IServiceModels
{
    public interface IServiceManager
    {
        Task<int> Save(CancellationToken cancellationToken);
        public IAuthService AuthService { get; }
        public ICustomerService CustomerService { get; }
        public IWarehouseService WarehouseService { get; }
        public IBrandService BrandService { get; }
        public ICategoryService CategoryService { get; }
        public IColorService ColorService { get; }
        public ISizeService SizeService { get; }
        public IUnitService UnitService { get; }
        public IProductService ProductService { get; }
        public ISupplierService SupplierService { get; }
        public IPhoneNumberService PhoneNumberService { get; }
    }
}
