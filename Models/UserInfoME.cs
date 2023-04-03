using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserInfoME
    {
        public int UsuId { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public ClientME UsuClient { get; set; }

    }
}
