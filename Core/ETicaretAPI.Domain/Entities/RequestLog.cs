using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class RequestLog:BaseEntity
    {
        public bool IsSuccessfull { get; set; }
    }
}
