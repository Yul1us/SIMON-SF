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
    //Esta es la Clase Concreta, Que implementa la Interfaz -> IProductCategoryRepository
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        //Traernos la Conexion y la Injectamos
        private readonly IDbConnection _dbConnection;
        public ProductCategoryRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;   
        }

        //El Metodo Asyncrono usara Dapper 
        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            //Paso1: La Consulta
            var sql = @"SELECT Id as Id, Name as Name
                        FROM ProductCategories ";

            //Paso2: Retornamos
            return await _dbConnection.QueryAsync<ProductCategory>(sql, new { });
        }
    }
}
