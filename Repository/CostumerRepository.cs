using Blazor.SIMONStore.Client.Models;
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
    public class CostumerRepository : ICostumerRepository
    {
        private readonly IDbConnection _dbConnection;
        public CostumerRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Costumer>> GetAll()
        {
            var sql = @"SELECT CTL.Id, CTL.TxtCedula, CTL.TxtApellidos, CTL.Tipo_Precio, CTL.Num_Vendedor, VDOR.TxtApenomb, CTL.Tipo_Cliente, CTL.Nombre_Tipo_Precio, CTL.Num_Tipo_Cliente FROM Tab_Productores AS CTL INNER JOIN Tab_Tecnicos AS VDOR ON CTL.Num_Vendedor = VDOR.NumCodigo WHERE (CTL.Status <> 9);";
            return await _dbConnection.QueryAsync<Costumer>(sql, new { });
        }

        public async Task<IEnumerable<Costumer>> GetBySeller(int id)
        {
            var sql = @"SELECT CTL.Id, CTL.TxtCedula, CTL.TxtApellidos, CTL.Tipo_Precio, CTL.Num_Vendedor, VDOR.TxtApenomb, CTL.Tipo_Cliente, CTL.Nombre_Tipo_Precio, CTL.Num_Tipo_Cliente FROM Tab_Productores AS CTL INNER JOIN Tab_Tecnicos AS VDOR ON CTL.Num_Vendedor = VDOR.NumCodigo WHERE (CTL.Status <> 9) AND Num_Vendedor = @Id ;";
            return await _dbConnection.QueryAsync<Costumer>(sql, new { Id = id });
        }

        public async Task<Costumer> GetDetail(int Id)
        {
            var sql = @"SELECT CTL.Id, CTL.TxtCedula, CTL.TxtApellidos, CTL.Tipo_Precio, CTL.Num_Vendedor, VDOR.TxtApenomb, CTL.Tipo_Cliente, CTL.Nombre_Tipo_Precio, CTL.Num_Tipo_Cliente FROM Tab_Productores AS CTL INNER JOIN Tab_Tecnicos AS VDOR ON CTL.Num_Vendedor = VDOR.NumCodigo WHERE (CTL.Status <> 9) AND CTL.Id =@Id ;";
            return await _dbConnection.QueryFirstOrDefaultAsync<Costumer>(sql, new { Id = Id });
        }
    }
}
