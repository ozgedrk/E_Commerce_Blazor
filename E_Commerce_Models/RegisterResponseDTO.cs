using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Models
{
    public class RegisterResponseDTO
    {
        public bool IsRegisterationSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }

    }
}
