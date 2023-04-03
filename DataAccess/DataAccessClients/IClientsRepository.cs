using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessClients
{
    public interface IClientsRepository
    {
        List<ClientME> GetClients();

        ClientME GetClientById(int id);

        int CreateClient(ClientME client);

        int ModifyClient(ClientME client);

        int DeleteClient(int id);
    }
}
