using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions
{
    public interface IProductService
    {
        List<Product> GetProducts();
        public Task<bool> AddSingleRequest(RequestLog requestLog);
        public Task SendEmail();
        public Task SendSms();
        public Task<bool> SaveAsync();
    }
}
