using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sid { get; set; }
        public string DisplayName { get; set; }
        public Guid Guid { get; set; }
    }
}
