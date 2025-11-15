using Blazor.SIMONStore.Shared;
using Dapper;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    //Esta es la Clase Concreta, Que implementa la Interfaz -> IOrderRepository
    public class OrderRepository : IOrderRepository
    {
        //Traernos la Conexion y la Injectamos
        private readonly IDbConnection _dbConnection;
        public OrderRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }

        public async Task DeleteOrder(int id)
        {
            ////Paso1: La Consulta de Acción Usando Dapper
            //var sql = @"
            //            DELETE FROM Orders
            //            WHERE Id = @Id ";


            ////Paso2: Como es un DELETE -> Se usa ExecuteAsync
            ////ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            ////No hace falta el Retorn, y no retotnaremos nada, ya que estamos en un Task.
            //await _dbConnection.ExecuteAsync(sql, 
            //    new {Id = @id });
            //Paso1: La Consulta Usando Dapper
            var sql = @" UPDATE Orders SET Status = 9 WHERE Id = @Id";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            await _dbConnection.ExecuteAsync(sql,
            new { Id = @id });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
          

        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var sql = @"SELECT	 Id
                          ,OrderNumber
                          ,ClientId
                          ,OrderDate
                          ,DeliveryDate
                          ,Total
                          ,Email
                          ,EmailCliente
                          ,NombreCliente
                          ,RifCliente
                          ,SIMONIdCliente
                          ,Status
                          ,OrderNumberSIMONId
                          ,IdVendedor
                          ,IdTpCliente
                          ,IdTpPrecio
                          ,Comentario
                          ,Fecha_Dia
                          ,id_moneda_cambio
                          ,Tasa_Cambio
                       FROM Orders WHERE Status != 9
                       ORDER BY Id DESC ";
            return await _dbConnection.QueryAsync<Order>(sql, new { });
        }

        public async Task<IEnumerable<Order>> GetAllByIdClient(int id)
        {
            var sql = @"SELECT	  Id
		                        ,OrderNumber
		                        ,ClientId
		                        ,OrderDate
		                        ,DeliveryDate
		                        ,Total
		                        ,C.Email
		                        ,C.NombreCliente
		                        ,C.FirstName AS ClientName
		                        ,Status
		                        ,OrderNumberSIMONId
                                ,IdVendedor
                                ,IdTpCliente
                                ,IdTpPrecio
                                ,Comentario
                              ,Fecha_Dia
                              ,id_moneda_cambio
                              ,Tasa_Cambio
                       FROM Orders AS O INNER JOIN Clients AS C ON O.ClientId = C.Id WHERE O.ClientId = @Id AND  Status != 9
                       ORDER BY O.Id DESC  ";
            return await _dbConnection.QueryAsync<Order>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Order>> GetAllByIdSeller(int id)
        {
            var sql = @"SELECT Id
		                      ,OrderNumber
                              ,ClientId
                              ,OrderDate
                              ,DeliveryDate
                              ,Total
                              ,Email
                              ,EmailCliente
                              ,NombreCliente
                              ,RifCliente
                              ,SIMONIdCliente
                              ,Status
                              ,OrderNumberSIMONId
                              ,IdVendedor
                              ,IdTpCliente
                              ,IdTpPrecio
                              ,Comentario
                          ,Fecha_Dia
                          ,id_moneda_cambio
                          ,Tasa_Cambio
                       FROM Orders WHERE IdVendedor = @Id AND Status != 9 
                       ORDER BY Id DESC   ";
            return await _dbConnection.QueryAsync<Order>(sql, new { Id = id });
        }

        public async Task<Order> GetDetails(int id)
        {
            //Aca se deben retornar los Detalles de la Orden pasada en el Partemetro Id
            var sql = @"SELECT Id
		                      ,OrderNumber
                              ,ClientId
                              ,OrderDate
                              ,DeliveryDate
                              ,Total
                              ,Email
                              ,EmailCliente
                              ,NombreCliente
                              ,RifCliente
                              ,SIMONIdCliente
                              ,Status
                              ,OrderNumberSIMONId
                              ,IdVendedor
                              ,IdTpCliente
                              ,IdTpPrecio
                              ,Comentario
                          ,Fecha_Dia
                          ,id_moneda_cambio
                          ,Tasa_Cambio
                       FROM Orders 
                       WHERE Id=@Id  AND Status != 9 ";
            return await _dbConnection.QueryFirstOrDefaultAsync<Order>(sql, 
                new { Id = id });
        }

        //Esta es la Clase Concreta,Para obtener el Proximo Id de Orden
        public async Task<int> GetNextId()
        {

            //Paso1: La Consulta Usando Dapper
            var sql = @" SELECT IDENT_CURRENT('Orders') + 1";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            return await _dbConnection.QueryFirstAsync<int>(sql, new { });
        }

        //Esta es la Clase Concreta,Para obtener el Proximo Numero de Orden
        public async Task<int> GetNextNumber()
        {
            // Paso 1: La Consulta Usando Dapper
            var sql = @"
                SELECT COALESCE(MAX(OrderNumber), 0) + 1
                FROM Orders";

            // Paso 2: Usar QueryFirstAsync para obtener el resultado
            var result = await _dbConnection.QueryFirstAsync<int>(sql);

            // Si el resultado es 0 (cuando la tabla está vacía), asigna 1
            if (result == 0)
            {
                result = 1;
            }

            return result;

        }

        //El Metodo Asyncrono usara Dapper 
        public async Task<bool> InsertOrder(Order order)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
                        INSERT INTO Orders ( [OrderNumber], [ClientId], [OrderDate], [DeliveryDate], [Total], [Email], [EmailCliente], [NombreCliente], [RifCliente], [SIMONIdCliente], [Status], [OrderNumberSIMONId] ,[IdVendedor],[IdTpCliente] ,[IdTpPrecio], [Comentario],[Fecha_Dia],[id_moneda_cambio],[Tasa_Cambio] )
                        VALUES(@OrderNumber, @ClientId, @OrderDate, @DeliveryDate, @Total, @Email, @EmailCliente, @NombreCliente, @RifCliente, @SIMONIdCliente, @Status, @OrderNumberSIMONId,@IdVendedor,@IdTpCliente,@IdTpPrecio,@Comentario,@Fecha_Dia,@id_moneda_cambio,@Tasa_Cambio) 
                        ";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result =  await _dbConnection.ExecuteAsync(sql, 
                new 
                { 
                    order.OrderNumber,
                    order.ClientId,
                    order.OrderDate,
                    order.DeliveryDate,
                    order.Total,
                    order.Email,
                    order.EmailCliente,
                    order.NombreCliente,
                    order.RIFCliente,
                    order.SIMONIdCliente,
                    order.Status,
                    order.OrderNumberSIMONId,
                    order.IdVendedor,
                    order.IdTpCliente,
                    order.IdTpPrecio,
                    order.Comentario,
                    order.Fecha_Dia,
                    order.id_moneda_cambio,
                    order.Tasa_Cambio
                  

                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @" UPDATE Orders SET ClientId = @ClientId,NombreCliente = @NombreCliente, OrderDate = @OrderDate, DeliveryDate = @DeliveryDate, 
            Total = @Total, IdVendedor = @IdVendedor, IdTpCliente = @IdTpCliente, 
            IdTpPrecio = @IdTpPrecio, Comentario = @Comentario,Fecha_Dia=@Fecha_Dia,id_moneda_cambio=@id_moneda_cambio,Tasa_Cambio=@Tasa_Cambio WHERE Id = @Id";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    order.Id,
                    order.OrderNumber,
                    order.ClientId,
                    order.OrderDate,
                    order.DeliveryDate,
                    order.Total,
                    order.Email,
                    order.EmailCliente,
                    order.NombreCliente,
                    order.RIFCliente,
                    order.SIMONIdCliente,
                    order.Status,
                    order.OrderNumberSIMONId,
                    order.IdVendedor,
                    order.IdTpCliente,
                    order.IdTpPrecio,
                    order.Comentario,
                    order.Fecha_Dia,
                    order.id_moneda_cambio,
                    order.Tasa_Cambio

                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }
        public async Task<Tasa> GetTasa(DateTime? fecha)
        {


            var sql = @" SELECT   TOP (1) Id, Fecha_Dia AS Dia, Valor_Moneda_Origen AS Valor
                                FROM     Tab_Tasa_Cambio Where Fecha_Dia = @Fecha
                                ORDER BY Dia DESC";
            var Tasa = await _dbConnection.QueryFirstOrDefaultAsync<Tasa>(sql, new { Fecha = fecha });
            if (Tasa is null)
            {
                sql = @" SELECT   TOP (1) Id, Fecha_Dia AS Dia, Valor_Moneda_Origen AS Valor
                                FROM     Tab_Tasa_Cambio 
                                ORDER BY Dia DESC";
                Tasa = await _dbConnection.QueryFirstOrDefaultAsync<Tasa>(sql, new { });
            }
            return Tasa;
        }
    }
}
