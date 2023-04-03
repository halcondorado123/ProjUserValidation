using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ClientME
    {
		public int ClientId { get; set; }
		public RolesME RolId { get; set; }
		public ClientIdentiME Identification { get; set; }
		public string IdentiNumber { get; set; }
		public string ClientName { get; set; }
		public string ClientLastName { get; set; }
		public GenreME GenreId { get; set; }
		public RelationshipME RelatId { get; set; }
		public int ClientAge { get; set; }
		public DateTime ClientBirthday { get; set; }
		public UnderAgeME UnderAgeId { get; set; }

		public int UsuId { get; set; }

	}
}
