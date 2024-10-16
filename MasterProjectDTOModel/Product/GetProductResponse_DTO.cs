using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterProjectDTOModel.Product
{
    public class GetProductResponse_DTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
    }
}
