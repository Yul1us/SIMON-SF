using Blazor.SIMONStore.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {

        //Traernos la Conexion y la Injectamos
        private readonly IDbConnection _dbConnection;
        public OrderProductRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }

        public async Task<bool> DeleteOrderProductByOrder(int orderId)
        {
            var sql = @"DELETE FROM OrderProducts
                        WHERE OrderId = @Id ";
            var result = await _dbConnection.ExecuteAsync(sql,
                new { Id = orderId });
            return result > 0;
        }

        public async Task<IEnumerable<Product>> GetByOrder(int orderId)
        {
            //var sql = @"SELECT	 Id
		          //              ,Name
		          //              ,ValorUnidadTeorico AS Price
		          //              ,CategoryId AS ProductCategoryId
		          //              ,Quantity
		          //              ,DescripcionCorta
		          //              ,DescripcionProducto
		          //              ,ProductSIMONId
            //            FROM OrderProducts INNER JOIN
	           //              Products AS P ON P.Id = ProductId
            //            WHERE OrderId = @Id ";

            var sql = @"SELECT	 Id
		                        ,Name
		                        ,PVP AS Price
		                        ,CategoryId AS ProductCategoryId
		                        ,Quantity
                    ,i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
                        FROM OrderProducts INNER JOIN
	                         Products AS P ON P.Id = ProductId INNER JOIN  Tab_Inventario AS i ON p.Id = i.TxtCodProduct
                        WHERE OrderId = @Id ";

            return await _dbConnection.QueryAsync<Product>(sql,
                new { Id = orderId });

        }
        /*SELECT p.Id, p.Name, p.Price, p.CategoryId, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
        FROM Products AS p INNER JOIN
                  Tab_Inventario AS i ON p.Id = i.TxtCodProduct
                        WHERE Id = @Id*/

        public async Task<bool> InsertOrderProduct(int orderId, Product product)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
                        INSERT INTO OrderProducts ([OrderId], [ProductId], [Quantity], [Unidad], [ValorUnidadTeorico], [TotValorUnidadTeorico], [TotValorUnidadReal], [ValorUnidadReal], [PVP], [QuantityReal])
                        VALUES(@OrderId, @ProductId, @Quantity,@Unidad, @ValorUnidadTeorico, @ValorUnidadTeorico,@ValorUnidadTeorico,@ValorUnidadTeorico,@pvp,@ValorUnidadTeorico)";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    OrderId = orderId,
                    ProductId = product.Id,
                    Quantity = product.Quantity,
                    Unidad=product.TxtUnidadMedida,
                    ValorUnidadTeorico = product.Quantity * product.Price,
                    pvp = product.Price


                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;

        }
    }
}
