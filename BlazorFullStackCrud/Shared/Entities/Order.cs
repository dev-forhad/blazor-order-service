using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name for the order")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a state for the order")]
        public string State { get; set; }

    }

}
