using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ammo.Domain.Authentication;
using System.Data;

namespace Ammo.Domain.Repositories
{
    public class RoleRepository<TRole> : IRoleStore<TRole, Guid>, IQueryableRoleStore<TRole, Guid>
        where TRole : IdentityRole
    {
        private string _connection;

        public RoleRepository(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Creates a new role 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (role.RoleId == default(Guid))
            {
                role.RoleId = Guid.NewGuid();
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute("spRoleCreate", role, null, null, CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// Deletes a Role 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute("spRoleDelete", role, null, null, CommandType.StoredProcedure));
            }
        }


        /// <summary>
        /// Finda role by it's Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Task<TRole> FindByIdAsync(Guid roleId)
        {
            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult<TRole>(connection.Query<TRole>("spRoleGetById", roleId).SingleOrDefault());
            }
        }

        /// <summary>
        /// Gets a role by it's name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task<TRole> FindByNameAsync(string roleName)
        {
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult<TRole>(connection.Query<TRole>("spRoleGetByName", new { Name = roleName }, null, true, null, CommandType.StoredProcedure).SingleOrDefault());
            }
        }

        /// <summary>
        /// Updates a Role 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute("spRoleUpdate", role, null, null, CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// Implementation of the IDisposable interface
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection = null;
            }
        }

        /* IQueryableRoleStore
        ---------------------------*/
        /// <summary>
        /// Creates an IQueryable from Roles
        /// </summary>
        public IQueryable<TRole> Roles
        {
            get
            {
                using (var connection = new SqlConnection(_connection))
                {
                    var result = connection.Query<TRole>("spRoleGetAll", null, null, true, null, CommandType.StoredProcedure);

                    return result.AsQueryable();
                }
            }
        }
    }
}
