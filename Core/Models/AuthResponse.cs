using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AuthResponse
    {
        public bool IsAuthenticated { get; set; }
        public UserInformation UserInfo { get; set; }
    }
}
