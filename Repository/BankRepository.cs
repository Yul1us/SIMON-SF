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
    public class BankRepository : IBankRepository
    {

        private readonly IDbConnection _dbConnection;
        public BankRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Bank>> GetAll()
        {
            //var sql = @"SELECT CodBanco, Nombre, TipoCuenta
            //        FROM   Tab_Bancos
            //        WHERE (Status <> 9) AND (Ing_Cheques = 0)";

            var sql = @"SELECT CodBanco,Representante AS Nombre, TipoCuenta, Id_Moneda, Sg_Moneda
                    FROM   Tab_Bancos
                    WHERE (Status <> 9) AND (Ing_Cheques = 1)";
            return await _dbConnection.QueryAsync<Bank>(sql, new { });
        }



    }
}
