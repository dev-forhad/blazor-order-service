using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFullStackCrud.Shared.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public List<WindowDTO> Windows { get; set; }
    }

}
