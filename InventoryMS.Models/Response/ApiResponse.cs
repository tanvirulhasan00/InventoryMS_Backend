using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InventoryMS.Models.Response
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public object? Results { get; set; }
        public object? Error { get; set; }
    }
}
