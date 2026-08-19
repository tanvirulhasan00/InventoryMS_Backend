using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Models.Entities.ProductModels.Dto
{
    public class UpdateBrandDto
    {
        public string BrandId { get; init; }
        public string? BrandName { get; init; }
        public string? BrandDescription { get; init; }
        public string? LicenseNo { get; init; }
    }
}
