using Ammo.Domain.Authentication;
using Dapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories
{
    public class UserRepository<TUser> : IUserStore<TUser, Guid>,
        IUserPasswordStore<TUser, Guid>,
        IUserEmailStore<TUser, Guid>,
        IUserSecurityStampStore<TUser, Guid>,
        IUserRoleStore<TUser, Guid>,
        IUserLockoutStore<TUser, Guid>,
        IUserPhoneNumberStore<TUser, Guid>,
        IUserTwoFactorStore<TUser, Guid>,
        IUserLoginStore<TUser, Guid>,
        IQueryableUserStore<TUser, Guid>
        where TUser : IdentityUser
    {
        private string _connection;

        public UserRepository(string connection)
        {
            _connection = connection;
        }

        /* IUserStore implementation for Identity Framework
        ---------------------------*/
        /// <summary>
        /// Creates a new user within the Ammo DB and scaffolds a profile for them
        /// </summary>
        /// <param name="user"></param>
        /// <returns>To BLL the async result</returns>
        public Task CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.Audit == null)
            {
                throw new ArgumentNullException("user.Audit");
            }

            //ensure UserId
            if (user.UserId == default(Guid))
            {
                user.UserId = Guid.NewGuid();
            }

            //convert to SQL min date
            var sqlMinDate = new DateTimeOffset(1753, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));
            if (user.LockoutEndDateUtc < sqlMinDate)
            {
                user.LockoutEndDateUtc = sqlMinDate;
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute(
                    "spUserCreate",
                    new
                    {
                        user.UserId,
                        user.Email,
                        user.EmailConfirmed,
                        user.PasswordHash,
                        user.SecurityStamp,
                        user.PhoneNumber,
                        user.PhoneNumberConfirmed,
                        user.TwoFactorEnabled,
                        user.LockoutEndDateUtc,
                        user.LockoutEnabled,
                        user.AccessFailedCount,
                        user.UserName,
                        CreateBy = user.Audit.ModifyBy,
                        ModifyBy = user.Audit.ModifyBy
                    },
                    null,
                    null,
                    System.Data.CommandType.StoredProcedure
               ));
            }
        }


        /// <summary>
        /// Logical deletion of a user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
                 
            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute("spUserDelete", user.UserId, null, null, CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// Retrieves a single user by their ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Dapper mapped User</returns>
        public Task<TUser> FindByIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_connection))
            {
                var result = connection.Query<TUser, IdentityProfile, TUser>("spUserGetById", (user, profile) =>
                {
                    user.Profile = profile;

                    return user;
                }, new { userId }, null, true, splitOn: "UserId", commandTimeout: null, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return Task.FromResult(result);
            }
        }

        /// <summary>
        /// Retrieves a User by their username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<TUser> FindByNameAsync(string userName)
        {
            using (var connection = new SqlConnection(_connection))
            {
                var result = connection.Query<TUser, IdentityProfile, TUser>("spUserGetByName", (user, profile) =>
                {
                    user.Profile = profile;

                    return user;
                }, new { userName }, null, true, splitOn: "UserId", commandTimeout: null, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return Task.FromResult(result);
            }
        }

        /// <summary>
        /// Asynchronous update user operation
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Result from Sql Server</returns>
        public Task UpdateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute("spUserUpdate", new
                {
                    //user
                    user.UserId,
                    user.Email,
                    user.EmailConfirmed,
                    user.PasswordHash,
                    user.SecurityStamp,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.TwoFactorEnabled,
                    user.LockoutEndDateUtc,
                    user.LockoutEnabled,
                    user.AccessFailedCount,
                    user.UserName,

                    //profile
                    user.Profile.FirstName,
                    user.Profile.MiddleName,
                    user.Profile.LastName,

                    //log
                    ModifyBy = (user.Audit == null) ? (Guid?)null : user.Audit.ModifyBy
                }, null, null, CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// Implementation of IDisposable interface
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection = null;
            }
        }

        /* IUserPasswordStore
        ---------------------------*/
        /// <summary>
        /// Gets a Password Hash for the specified User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
       
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        /// Checks whether the user has an empty hash
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(!String.IsNullOrEmpty(user.PasswordHash));
        }

        /// <summary>
        /// Sets a new password hash for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        /* IUserEmailStore
        ---------------------------*/
        /// <summary>
        /// Returns a user based upon their email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<TUser> FindByEmailAsync(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email");
            }
            
            using (var connection = new SqlConnection(_connection))
            {
                var result = connection.Query<TUser, IdentityProfile, TUser>("spUserGetByEmail", (user, profile) =>
                {
                    user.Profile = profile;

                    return user;
                }, new { email }, null, true, splitOn: "UserId", commandTimeout: null, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return Task.FromResult(result);
            }
        }

        /// <summary>
        /// Gets the Email for a specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetEmailAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Checks whether the user's email has been confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        /// Sets the User's email 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task SetEmailAsync(TUser user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Email = email;

            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets the status of the user's email Confirmed/Unconfirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.EmailConfirmed = confirmed;

            return Task.FromResult(0);
        }

        /* IUserSecurityStampStore
        ---------------------------*/
        /// <summary>
        /// Retrieves a security stamp for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>
        /// Sets a new security stamp for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        /* IUserRoleStore
        ---------------------------*/
        /// <summary>
        /// Adds a user to a specific role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute(
                    "spUserRoleCreate", 
                    new
                    {
                        user.UserId,
                        roleName
                    }, 
                    null, 
                    null, 
                    CommandType.StoredProcedure
               ));
            }
        }

        /// <summary>
        /// Retrieves a collection of roles
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult<IList<string>>(connection.Query<string>("spRoleGet", new
                {
                    user.UserId
                }, null, true, null, CommandType.StoredProcedure).ToList());
            }
        }

        /// <summary>
        /// Checks whether the specified user has a particular role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            var result = await GetRolesAsync(user);

            if (result == null || result.Count == 0)
            {
                return false;
            }

            return result.Contains<string>(roleName);
        }


        /// <summary>
        /// Removes the user from a specified role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (String.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute("spUserRoleDelete", new
                {
                    user.UserId,
                    roleName
                }, null, null, CommandType.StoredProcedure));
            }
        }


        /* IUserLockoutStore
        ---------------------------*/
        /// <summary>
        /// Determines how many occasions the users has failed to login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        /// Checks whether the user is Locked out
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        /// Determines the end of the lockout
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.LockoutEndDateUtc);
        }

        /// <summary>
        /// Increases the number of failed attempts
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount++;

            return Task.FromResult(user.AccessFailedCount);
        }


        /// <summary>
        /// Logically resets the failed login attempts
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task ResetAccessFailedCountAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.AccessFailedCount = 0;

            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets the user's state as Locked out
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.LockoutEnabled = enabled;

            return Task.FromResult(0);
        }

        /// <summary>
        /// The lockout is set as default SQL date and then processed by the BLL for the required lockout period
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <returns></returns>
        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var sqlMinDate = new DateTimeOffset(1753, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));

            if (lockoutEnd < sqlMinDate)
            {
                lockoutEnd = sqlMinDate;
            }

            user.LockoutEndDateUtc = lockoutEnd;

            return Task.FromResult(0);
        }


        /* IUserPhoneNumberStore
        ---------------------------*/
        /// <summary>
        /// Gets a user's registered phone number
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>
        /// Checks whether the user's phone number is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        /// Sets the user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumber = phoneNumber;

            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets the flag which determines if a user's phone number is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.PhoneNumberConfirmed = confirmed;

            return Task.FromResult(0);
        }

        /* IUserTwoFactorStore
        ---------------------------*/
        /// <summary>
        /// Gets the status of the user's TFA
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <summary>
        /// Sets the status of the user's TFA
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.TwoFactorEnabled = enabled;

            return Task.FromResult(0);
        }

        /* IUserLoginStore
        ---------------------------*/
        /// <summary>
        /// Creates User's third party logins
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Execute("spUserLoginCreate", new
                {
                    login.LoginProvider,
                    login.ProviderKey,
                    user.UserId
                }, null, null, CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// Finds a user based on login info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            
            var userId = default(Guid);

            using (var connection = new SqlConnection(_connection))
            {
                //get user id (could combine this into a single sql statement)
                userId = connection.Query<Guid>("spUserLoginGet", new
                {
                    login.LoginProvider,
                    login.ProviderKey,
                }, null, true, null, CommandType.StoredProcedure).SingleOrDefault();
            }

            if (userId != default(Guid))
            {
                return FindByIdAsync(userId);
            }

            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Gets all logins for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
        

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult<IList<UserLoginInfo>>(connection.Query<UserLoginInfo, dynamic, UserLoginInfo>("spUserLoginGetAll", (userLoginInfo, result) =>
                {
                    return new UserLoginInfo(result.LoginProvider, result.ProviderKey);
                }, new
                {
                    user.UserId
                }, null, true, splitOn: "UserId", commandTimeout: null, commandType: CommandType.StoredProcedure).ToList());
            }
        }

        /// <summary>
        /// Removes a third party login for a specific user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            using (var connection = new SqlConnection(_connection))
            {
                return Task.FromResult(connection.Query<UserLoginInfo>("spUserLoginDelete", new
                {
                    user.UserId,
                    login.LoginProvider,
                    login.ProviderKey
                }, null, true, null, CommandType.StoredProcedure).ToList() as IList<UserLoginInfo>);
            }
        }

        /* IQueryableUserStore
        ---------------------------*/
        /// <summary>
        /// Enables Users to become IQueryable
        /// </summary>
        public IQueryable<TUser> Users
        {
            get
            {
                using (var connection = new SqlConnection(_connection))
                {
                    var result = connection.Query<TUser, IdentityProfile, TUser>("spUserGetAll", (user, profile) =>
                    {
                        user.Profile = profile;

                        return user;
                    }, splitOn: "UserId");

                    return result.AsQueryable();
                }
            }
        }
    }
}
