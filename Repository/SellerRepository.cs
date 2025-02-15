using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using Dapper;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    
    public class SellerRepository : ISellerRepository
    {
        private readonly IDbConnection _dbConnection;
        public SellerRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }
     
        public async Task<bool> InsertOrder(UserRole userRole)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"INSERT INTO AspNetUserRoles (UserId, RoleId)
                    VALUES (@UserId, @RoleId); ";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    userRole.UserId,
                    userRole.RoleId,
                  
                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }


        public async Task<IEnumerable<Seller>> Getall()
        {
            var sql = @"SELECT NumCodigo,[TxtApenomb],TxtEmail,Bloqueo
                    FROM   Tab_Tecnicos
                    WHERE (Status <> 9) AND (Bloqueo = 0)";
            return await _dbConnection.QueryAsync<Seller>(sql, new { });
        }

        public async Task<Seller> GetDetails(int Sellerid)
        {
            //Aca se deben retornar los Detalles de la Orden pasada en el Partemetro Id
            var sql = @"SELECT NumCodigo,[TxtApenomb],TxtEmail,Bloqueo
                    FROM   Tab_Tecnicos
                    WHERE (Status <> 9) AND (Bloqueo = 0) AND NumCodigo=@Id ";
            return await _dbConnection.QueryFirstOrDefaultAsync<Seller>(sql, new { Id = Sellerid });
        }

        public async Task<Seller> GetDetailsbyEmail(string email)
        {
            //Aca se deben retornar los Detalles de la Orden pasada en el Partemetro Id
            var sql = @"SELECT NumCodigo,[TxtApenomb],TxtEmail,Bloqueo
                    FROM   Tab_Tecnicos
                    WHERE (Status <> 9) AND (Bloqueo = 0) AND TxtEmail=@TxtEmail";
            return await _dbConnection.QueryFirstOrDefaultAsync<Seller>(sql, new { TxtEmail = email });
        }

        public async Task<bool> UpdateSeller(Seller seller)
        {
            var sql = @"UPDATE Tab_Tecnicos
               SET TxtEmail = @TxtEmail
          
             WHERE NumCodigo=@NumCodigo";
            var result = await _dbConnection.ExecuteAsync(sql,
             new
             {
                 seller.NumCodigo,
                 seller.TxtEmail,
             });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }
    }


}
