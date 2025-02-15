using Blazor.SIMONStore.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    //Esta es la Clase Concreta, Que implementa la Interfaz -> IOrderRepository
    public class PaymentOrderRepository : IPaymentOrderRepository
    {
        //Traernos la Conexion y la Injectamos
         private readonly IDbConnection _dbConnection;
         public PaymentOrderRepository(IDbConnection dbConnection)
         {
             //Asigna a la Variable Global, La variable Local
             //Ya Tenemos Injectada la Conexion...
             _dbConnection = dbConnection;
         }
        /*
        public async Task DeleteOrder(int id)
        {
            //Paso1: La Consulta de Acción Usando Dapper
            var sql = @"
                        DELETE FROM Orders
                        WHERE Id = @Id ";


            //Paso2: Como es un DELETE -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            //No hace falta el Retorn, y no retotnaremos nada, ya que estamos en un Task.
            await _dbConnection.ExecuteAsync(sql, 
                new {Id = @id });

        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var sql = @"SELECT	 O.Id
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
                       FROM Orders AS O INNER JOIN Clients AS C ON O.ClientId = C.Id 
                       ORDER BY O.Id DESC";
            return await _dbConnection.QueryAsync<Order>(sql, new { });
        }

        public async Task<Order> GetDetails(int id)
        {
            //Aca se deben retornar los Detalles de la Orden pasada en el Partemetro Id
            var sql = @"SELECT	 Id
                                ,OrderNumber
                                ,ClientId
                                ,OrderDate
                                ,DeliveryDate
                                ,Total
                       FROM Orders 
                       WHERE Id=@Id ";
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
            //Paso1: La Consulta Usando Dapper
            var sql = @"
                        SELECT MAX (OrderNumber) + 1
                        FROM Orders ";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            return await _dbConnection.QueryFirstAsync<int>(sql,new { });
        }

        //El Metodo Asyncrono usara Dapper 
        public async Task<bool> InsertOrder(Order order)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
                        INSERT INTO Orders ( [OrderNumber], [ClientId], [OrderDate], [DeliveryDate], [Total], [Email], [EmailCliente], [NombreCliente], [RifCliente], [SIMONIdCliente], [Status], [OrderNumberSIMONId] )
                        VALUES(@OrderNumber, @ClientId, @OrderDate, @DeliveryDate, @Total, @Email, @EmailCliente, @NombreCliente, @RifCliente, @SIMONIdCliente, @Status, @OrderNumberSIMONId) 
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
                    order.OrderNumberSIMONId
                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
                        UPDATE Orders
                        SET ClientId=@ClientId, 
                            OrderDate= @OrderDate, 
                            DeliveryDate=@DeliveryDate,
                            Total=@Total
                        WHERE Id = @Id ";

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
                    order.OrderNumberSIMONId
                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }*/
        public async Task DeletePaymentOrder(int id)
        {
            //Paso1: La Consulta de Acción Usando Dapper
            var sql = @"
                         DELETE FROM PaymentOrders
                         WHERE Id = @Id ";


            //Paso2: Como es un DELETE -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            //No hace falta el Retorn, y no retotnaremos nada, ya que estamos en un Task.
            await _dbConnection.ExecuteAsync(sql,
                new { Id = @id });
        }

        public async Task<IEnumerable<PaymentOrders>> GetAll()
        {
            var sql = @"SELECT Id
                          ,PayNumber
                          ,ClientId
                          ,PayDate
                          ,Methodpayment
                          ,Total
                          ,Comment
                          ,IdSeller
                          ,PayNumberSIMONId
                          ,Ref
                      FROM PaymentOrders ORDER BY Id DESC";
            return await _dbConnection.QueryAsync<PaymentOrders>(sql, new { });
        }

        public async Task<PaymentOrders> GetDetails(int id)
        {
            //Aca se deben retornar los Detalles de la Orden pasada en el Partemetro Id
            var sql = @"SELECT Id
                          ,PayNumber
                          ,ClientId
                          ,PayDate
                          ,Methodpayment
                          ,Total
                          ,Comment
                          ,IdSeller
                          ,PayNumberSIMONId
                          ,Ref
                      FROM PaymentOrders
                       WHERE Id=@Id ";
            return await _dbConnection.QueryFirstOrDefaultAsync<PaymentOrders>(sql,
                new { Id = id });
        }

        public async Task<int> GetNextId()
        {

            //Paso1: La Consulta Usando Dapper
            var sql = @" SELECT IDENT_CURRENT('PaymentOrders') + 1";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            return await _dbConnection.QueryFirstAsync<int>(sql, new { });
        }

        public async Task<int> GetNextNumber()
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
                        SELECT MAX (PayNumber) + 1
                        FROM PaymentOrders ";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            return await _dbConnection.QueryFirstAsync<int>(sql, new { });
        }

        public async Task<bool> InsertOrder(PaymentOrders order)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
          INSERT INTO PaymentOrders (PayNumber, ClientId, PayDate, Methodpayment, Total, Comment, IdSeller, PayNumberSIMONId, Ref)
          VALUES (@PayNumber, @ClientId, @PayDate, @Methodpayment, @Total, @Comment, @IdSeller, @PayNumberSIMONId, @Ref)";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    order.ClientId,
                    order.PayNumber,
                    order.PayDate,
                    order.Methodpayment,
                    order.Total,
                    order.Comment,
                    order.IdSeller,
                    order.PayNumberSIMONId,
                    order.Ref
                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }

        public async Task<bool> UpdateOrder(PaymentOrders order)
        {
            //Paso1: La Consulta Usando Dapper
        var sql = @"UPDATE PaymentOrders
               SET PayNumber = @PayNumber
                  ,ClientId = @ClientId
                  ,PayDate = @PayDate
                  ,Methodpayment = @Methodpayment
                  ,Total = @Total
                  ,Comment = @Comment
                  ,IdSeller = @IdSeller
                  ,PayNumberSIMONId = @PayNumberSIMONId
                  ,Ref = @Ref
             WHERE Id=@Id";
            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    order.Id,
                    order.PayNumber,
                    order.ClientId,
                    order.PayDate,
                    order.Methodpayment,
                    order.Total,
                    order.Comment,
                    order.IdSeller,
                    order.PayNumberSIMONId,
                    order.Ref
                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }
    }
}
