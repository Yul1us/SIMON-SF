
using Blazor.SIMONStore.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    //Esta es la Clase Concreta, Que implementa la Interfaz -> IClientRepository
    public class ClientRepository : IClientRepository
    {
        //Injectar la Dependencia de la BD...
        //Traernos la Conexion y la Injectamos
        private readonly IDbConnection _dbConnection;
        public ClientRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }

        //El Metodo Asyncrono usara Dapper 
        public async Task<IEnumerable<Blazor.SIMONStore.Shared.Client>> GetAll()
        {
            //Paso1: La Consulta
            var sql = @"SELECT   Id,
                            FirstName,
                            LastName,
                            BirthDate,
                            Phone,
                            Address,
                            ClienteSIMONId,
                            RIFCliente,
                            NombreCliente,
                            Telefono,
                            Email
                        FROM  Clients
                        ORDER BY NombreCliente";

            //Paso2: Retornamos, Como hay un Parametro en la Consulta => @Id -> Id = productCategoryId
            return await _dbConnection.QueryAsync<Blazor.SIMONStore.Shared.Client>(sql, new { });
            //Se debe agregar en el Contenedor de Depencencias...del Servidor Startup
        }
    }
}
