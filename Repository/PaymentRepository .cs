using Blazor.SIMONStore.Client.Models;
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
    public class PaymentRepository : IPaymentRepository
    {

        private readonly IDbConnection _dbConnection;
        public PaymentRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }

        public async Task DeletePayment(int id)
        {
            //Paso1: La Consulta de Acción Usando Dapper
            var sql = @"
                         DELETE FROM Tab_Bancos_Tran
                         WHERE Id = @Id ";


            //Paso2: Como es un DELETE -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            //No hace falta el Retorn, y no retotnaremos nada, ya que estamos en un Task.
            await _dbConnection.ExecuteAsync(sql,
                new { Id = @id });
        }
    

        public async Task<IEnumerable<payment>> GetAll()
        {
            var sql = @"SELECT   Id, Fecha, Referencia, Descripcion, Cod_Beneficiario, Beneficiario, Cod_Banco, Banco, Sg_Moneda, Debe, Haber, Status, Usuario, Fecha_Creacion, Signo, ImageUrl, Tasa, Monto_Divisa, Comentario
                        FROM     Tab_Bancos_Tran
                        WHERE   (Signo = '+')
                        ORDER BY Id DESC";
            return await _dbConnection.QueryAsync<payment>(sql, new { });
        }

        public  async Task<IEnumerable<payment>> Getbyclient(int id)
        {
            var sql = @"SELECT  Id, Fecha, Referencia, Descripcion, Cod_Beneficiario, Beneficiario, Cod_Banco, Banco, Sg_Moneda, Debe, Haber, Status, Usuario, Fecha_Creacion, Signo , ImageUrl,Tasa, Monto_Divisa, Comentario
                        FROM   Tab_Bancos_Tran
                        WHERE (Signo = '+') AND Cod_Beneficiario=@Id ORDER BY Id DESC";
            return await _dbConnection.QueryAsync<payment>(sql, new { Id = id });
        }

        public async Task<IEnumerable<payment>> GetbySeller(string usuario)
        {
            var sql = @"SELECT  Id, Fecha, Referencia, Descripcion, Cod_Beneficiario, Beneficiario, Cod_Banco, Banco, Sg_Moneda, Debe, Haber, Status, Usuario, Fecha_Creacion, Signo, ImageUrl, Tasa, Monto_Divisa, Comentario
                        FROM   Tab_Bancos_Tran
                        WHERE (Signo = '+') AND usuario=@Usuario ORDER BY Id DESC";
            return await _dbConnection.QueryAsync<payment>(sql, new { Usuario = usuario });
        }

        public async Task<payment> GetDetails(int id)
        {
            var sql = @"SELECT  Id, Fecha, Referencia, Descripcion, Cod_Beneficiario, Beneficiario, Cod_Banco, Banco, Sg_Moneda, Debe, Haber, Status, Usuario, Fecha_Creacion, Signo, ImageUrl, Tasa, Monto_Divisa, Comentario
                        FROM   Tab_Bancos_Tran
                        WHERE (Signo = '+') AND Id=@Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<payment>(sql, new { Id = id });
        }

        public async Task<int> GetNextId()
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @" SELECT IDENT_CURRENT('Tab_Bancos_Tran') + 1";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            return await _dbConnection.QueryFirstAsync<int>(sql, new { });
        }

        public async Task<int> GetNextNumber()
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
                        SELECT MAX (N_Doc) + 1
                    FROM Tab_Bancos_Tran ";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            return await _dbConnection.QueryFirstAsync<int>(sql, new { });
        }

        public async Task<Tasa> GetTasa(DateTime? fecha)
        {


                            var sql = @" SELECT   TOP (1) Id, Fecha_Dia AS Dia, Valor_Moneda_Origen AS Valor
                                FROM     Tab_Tasa_Cambio Where Fecha_Dia = @Fecha
                                ORDER BY Dia DESC";
            var  Tasa = await _dbConnection.QueryFirstOrDefaultAsync<Tasa>(sql, new { Fecha = fecha });
            if (Tasa is null)
            {
                 sql = @" SELECT   TOP (1) Id, Fecha_Dia AS Dia, Valor_Moneda_Origen AS Valor
                                FROM     Tab_Tasa_Cambio 
                                ORDER BY Dia DESC";
                  Tasa = await _dbConnection.QueryFirstOrDefaultAsync<Tasa>(sql, new {  });
            }
            return Tasa;
        }

        public async Task<bool> InsertOrder(payment order)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"INSERT INTO Tab_Bancos_Tran ( Fecha, Referencia, Descripcion, Cod_Beneficiario, Beneficiario, Cod_Banco, Banco, Sg_Moneda, Debe, Haber, Status, Usuario, Fecha_Creacion, Signo, ImageUrl,Tasa,Monto_Divisa, Comentario)
        VALUES ( @Fecha, @Referencia, @Descripcion, @Cod_Beneficiario, @Beneficiario,@Cod_Banco, @Banco, @Sg_Moneda, @Debe,@Haber, @Status, @Usuario,@Fecha_Creacion, @Signo, @ImageUrl,@Tasa,@Monto_Divisa, @Comentario)";

            //Paso2: Como es un INSERT -> Se usa ExecuteAsync
            //ya que ExecuteAsync Retorna el Numero de Filas Afectadas...
            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
          
                    order.Fecha,
                    order.Referencia,
                    order.Descripcion,
                    order.Cod_Beneficiario,
                    order.Beneficiario,
                    order.Cod_Banco,
                    order.Banco,
                    order.Sg_Moneda,
                    order.Debe,
                    order.Haber,
                    order.Status,
                    order.Usuario,
                    order.Fecha_Creacion,
                    order.Signo,
                    order.ImageUrl,
                    order.Tasa,
                    order.Monto_Divisa,
                    order.Comentario
                });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }

        public async Task<bool> UpdateOrder(payment order)
        {
            var sql = @"UPDATE Tab_Bancos_Tran
               SET Fecha = @Fecha
                  ,Referencia = @Referencia
                  ,Descripcion = @Descripcion
                  ,Cod_Beneficiario = @Cod_Beneficiario
                  ,Beneficiario = @Beneficiario
                  ,Cod_Banco = @Cod_Banco
                  ,Banco = @Banco
                  ,Sg_Moneda = @Sg_Moneda
                  ,Debe = @Debe
                  ,Haber = @Haber
                  ,status = @Status
                  ,Usuario = @Usuario
                  ,Fecha_Creacion = @Fecha_Creacion
                  ,Signo = @Signo
                  ,ImageUrl = @ImageUrl
                  ,Tasa = @Tasa
                  ,Monto_Divisa=@Monto_Divisa
                  ,Comentario=@Comentario
             WHERE Id=@Id";
            var result = await _dbConnection.ExecuteAsync(sql,
             new
             {
                 order.Id,
                 order.Fecha,
                 order.Referencia,
                 order.Descripcion,
                 order.Cod_Beneficiario,
                 order.Beneficiario,
                 order.Cod_Banco,
                 order.Banco,
                 order.Sg_Moneda,
                 order.Debe,
                 order.Haber,
                 order.Status,
                 order.Usuario,
                 order.Fecha_Creacion,
                 order.Signo,
                 order.ImageUrl,
                 order.Tasa,
                 order.Monto_Divisa,
                 order.Comentario
          

             });
            //Como Task<bool> entonces se evalua y retorna un true o false -> return result > 0;
            //El ExecuteAsync siempre retorna el numero de filas afectadas...
            return result > 0;
        }
    }
}
