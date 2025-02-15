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
    
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRepository(IDbConnection dbConnection)
        {
            //Asigna a la Variable Global, La variable Local
            //Ya Tenemos Injectada la Conexion...
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<User>> Getall()
        {
            var sql = @"SELECT Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed,
            PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed,
            TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount,
            NombreCompleto, UserSIMONId FROM AspNetUsers;";
            return await _dbConnection.QueryAsync<User>(sql, new { });
        }

        public async Task<IEnumerable<Roles>> GetallRoles()
        {
            var sql = @"SELECT 
                            Id,
                            Name,
                            NormalizedName,
                            ConcurrencyStamp
                        FROM 
                            AspNetRoles;";
            return await _dbConnection.QueryAsync<Roles>(sql, new { });
        }

        public async Task<IEnumerable<UserRole>> GetallUserRoles()
        {
            var sql = @"SELECT UserId, RoleId 
                        FROM
                            AspNetUserRoles;";
            return await _dbConnection.QueryAsync<UserRole>(sql, new { });
        }

        public Task<User> GetDetails(int userid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Roles>> GetUserRoles(string userId)
        {
            
            string sql = @" SELECT r.Id, r.Name 
                        FROM AspNetRoles r
                        INNER JOIN AspNetUserRoles ur ON r.Id = ur.RoleId 
                        WHERE ur.UserId = @UserId
                    "; 
            var roles = await _dbConnection.QueryAsync<Roles>(sql, new { UserId = userId });
            return roles;
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

        public async Task<bool> UpdateOrder(UserRole userRole)
        {
            //Paso1: La Consulta Usando Dapper
            var sql = @"
                        UPDATE AspNetUserRoles
                        SET   RoleId= @RoleId 
                        WHERE UserId = @UserId ";

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
    }


}
