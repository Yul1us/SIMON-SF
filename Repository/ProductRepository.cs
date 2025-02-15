using Blazor.SIMONStore.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blazor.SIMONStore.Repositories
{
    //Esta es la Clase Concreta, Que implementa la Interfaz -> IProductRepository
    public class ProductRepository : IProductRepository
    {
        //Injectar la Dependencia de la BD...
        //Traernos la Conexion y la Injectamos
        private readonly IDbConnection _dbConnection;
        public ProductRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Product>> Getall()
        {
            var sql = @"SELECT   p.Id, p.Name, p.Price, p.CategoryId, p.ImageUrl, p.DescripcionCorta, p.DescripcionProducto, p.ProductSIMONId, p.Status, p.Bloqueo, p.Linea, p.PVP1, p.PVP2, p.PVP3, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
FROM     Products AS p INNER JOIN
             Tab_Inventario AS i ON p.Id = i.TxtCodProduct";
            return await _dbConnection.QueryAsync<Product>(sql, new { });
        }

        //El Metodo Asyncrono usara Dapper 
        public async Task<IEnumerable<Product>> GetByCategory(int productCategoryId)
        {

            //Paso1: La Consulta
            //Se usa esta Tecnica para que el binding sea correcto -> CategoryId AS ProductCategoryId
            var sql = @"SELECT p.Id, p.Name, p.Price, p.CategoryId, p.ImageUrl, p.DescripcionCorta, p.DescripcionProducto, p.ProductSIMONId, p.Status, p.Bloqueo, p.Linea, p.PVP1, p.PVP2, p.PVP3, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
                     FROM   Products AS p INNER JOIN
                  Tab_Inventario AS i ON p.Id = i.TxtCodProduct
                        WHERE CategoryId = @Id ";

            //Paso2: Retornamos, Como hay un Parametro en la Consulta => @Id -> Id = productCategoryId
            //Para traer un listado de tosos los itemas se usa QueryAsync
            return await _dbConnection.QueryAsync<Product>(sql, new { Id = productCategoryId });
        }

        public async Task<Product> GetDetails(int productId)
        {
            //Paso1: La Consulta
            //Se usa esta Tecnica para que el binding sea correcto -> CategoryId AS ProductCategoryId
            var sql = @"SELECT p.Id, p.Name, p.Price, p.CategoryId, p.ImageUrl, p.DescripcionCorta, p.DescripcionProducto, p.ProductSIMONId, p.Status, p.Bloqueo, p.Linea, p.PVP1, p.PVP2, p.PVP3, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
                    FROM   Products AS p INNER JOIN
                  Tab_Inventario AS i ON p.Id = i.TxtCodProduct
                        WHERE Id = @Id ";

            //Paso2: Retornamos, Como hay un Parametro en la Consulta => @Id -> Id = productId
            //Para traer un solo items se usa QueryFirstOrDefaultAsync
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = productId });
        }

        public async Task<Product> GetDetails(int Id, int tipo_precio, int num_tipo_cliente)
        {
            if (num_tipo_cliente == 1 || num_tipo_cliente == 2 || num_tipo_cliente == 3 || num_tipo_cliente == 7 || num_tipo_cliente == 8)
            {


                if (tipo_precio == 6)
                {


                    var sql = @"SELECT p.Id, p.Name, pvp.PVP01 AS Price, p.CategoryId, p.ImageUrl, p.DescripcionCorta, p.DescripcionProducto, p.ProductSIMONId, p.Status, p.Bloqueo, p.Linea, p.PVP1, p.PVP2, p.PVP3, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
                    FROM   Products AS p INNER JOIN
                  Tab_Inventario AS i ON p.Id = i.TxtCodProduct INNER JOIN View_LISTA_PRRECIOS_VS_TP_CLIENTES_PVP01_01 AS pvp on p.Id = pvp.CODPROD
           
             Where PVP.CodTipCtl = @num_tipo_cliente and p.Id = @Id ";


                    return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = Id, num_tipo_cliente = num_tipo_cliente });

                    //lista 1
                }

                if (tipo_precio == 2)
                {



                    var sql = @"SELECT p.Id, p.Name, pvp.PVP02 AS Price, p.CategoryId, p.ImageUrl, p.DescripcionCorta, p.DescripcionProducto, p.ProductSIMONId, p.Status, p.Bloqueo, p.Linea, p.PVP1, p.PVP2, p.PVP3, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
                    FROM   Products AS p INNER JOIN
                  Tab_Inventario AS i ON p.Id = i.TxtCodProduct INNER JOIN View_LISTA_PRRECIOS_VS_TP_CLIENTES_PVP02_01 AS pvp on p.Id = pvp.CODPROD
           
             Where pvp.CodTipCtl = @num_tipo_cliente and p.Id = @Id ";

                    return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = Id, num_tipo_cliente = num_tipo_cliente });
                    //lista 2

                }

                if (tipo_precio == 4)
                {


                    var sql = @"SELECT p.Id, p.Name, pvp.PVP03 AS Price, p.CategoryId, p.ImageUrl, p.DescripcionCorta, p.DescripcionProducto, p.ProductSIMONId, p.Status, p.Bloqueo, p.Linea, p.PVP1, p.PVP2, p.PVP3, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
                    FROM   Products AS p INNER JOIN
                  Tab_Inventario AS i ON p.Id = i.TxtCodProduct INNER JOIN View_LISTA_PRRECIOS_VS_TP_CLIENTES_PVP03_01 AS pvp on p.Id = pvp.CODPROD
             Where pvp.CodTipCtl = @num_tipo_cliente and p.Id = @Id ";

                    return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = Id, num_tipo_cliente = num_tipo_cliente });
                    //  lista 3

                }
                else
                {
                    var sql = @"SELECT p.Id, p.Name, pvp.PVP03 AS Price, p.CategoryId, p.ImageUrl, p.DescripcionCorta, p.DescripcionProducto, p.ProductSIMONId, p.Status, p.Bloqueo, p.Linea, p.PVP1, p.PVP2, p.PVP3, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
                    FROM   Products AS p INNER JOIN
                  Tab_Inventario AS i ON p.Id = i.TxtCodProduct INNER JOIN View_LISTA_PRRECIOS_VS_TP_CLIENTES_PVP03_01 AS pvp on p.Id = pvp.CODPROD
           
             Where PVP.CodTipCtl =  @num_tipo_cliente and p.Id = @Id ";


                    return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = Id, num_tipo_cliente = num_tipo_cliente });

                    //  lista 3
                }
            }
            else
            {
                var sql = @"SELECT p.Id, p.Name, pvp.PVP03 AS Price, p.CategoryId, p.ImageUrl, p.DescripcionCorta, p.DescripcionProducto, p.ProductSIMONId, p.Status, p.Bloqueo, p.Linea, p.PVP1, p.PVP2, p.PVP3, i.FacturaPor, i.TxtUnidadMedida, (i.Prom_Peso_Min + i.Prom_Peso_Max) / 2.0 AS promedio_peso
                    FROM   Products AS p INNER JOIN
                  Tab_Inventario AS i ON p.Id = i.TxtCodProduct INNER JOIN View_LISTA_PRRECIOS_VS_TP_CLIENTES_PVP03_01 AS pvp on p.Id = pvp.CODPROD
           
             Where PVP.CodTipCtl = 2 and p.Id = @Id ";


                return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = Id, num_tipo_cliente = num_tipo_cliente });

                //  lista 3
            }

        }

        public async Task<bool> InsertOrder(Product product)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
          INSERT INTO Products (Name, Price, CategoryId, ImageUrl, DescripcionCorta, DescripcionProducto, ProductSIMONId)
          VALUES (@Name, @Price, @CategoryId, @ImageUrl, @DescripcionCorta, @DescripcionProducto, @ProductSIMONId)";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    
                    product.Name,
                    product.Price,
                    product.CategoryId,
                    product.ImageUrl,
                    product.DescripcionCorta,
                    product.DescripcionProducto,
                    product.ProductSIMONId

                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }

        public async Task<bool> UpdateOrder(Product product)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"UPDATE Products
                        SET
                            Name = @Name,
                            Price = @Price,
                            CategoryId = @ProductCategoryId,
                            ImageUrl = @ImageUrl,
                            DescripcionCorta = @DescripcionCorta,
                            DescripcionProducto = @DescripcionProducto,
                            ProductSIMONId = @ProductSIMONId
                        WHERE
                            Id = @Id";
            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    product.Id,
                    product.Name,
                    product.Price,
                    product.CategoryId,
                    product.ImageUrl,
                    product.DescripcionCorta,
                    product.DescripcionProducto,
                    product.ProductSIMONId
                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }
    }
}
