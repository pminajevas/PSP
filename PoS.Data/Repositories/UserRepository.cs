using PoS.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using PoS.Data.Mapper;
using PoS.Shared.Utilities;
using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;
using PoS.Shared.InnerDTOs;

namespace PoS.Data.Repositories
{
    public class UserRepository
    {
        private readonly PoSDbContext _dbcontext;

        public UserRepository(PoSDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Customers

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(Filter filter)
        {
            IQueryable<Customer> query = _dbcontext.Customers;

            foreach (var filterName in filter.Parameters.Keys)
            {
                var filterValue = filter.Parameters[filterName];
                if (filterName == "BusinessId" && filterValue != null)
                {
                    query = query.Where(customer => customer.BusinessId != null && customer.BusinessId == (Guid)filterValue);
                }
                else if (filterName == "LoyaltyId" && filterValue != null)
                {
                    query = query.Where(customer => customer.LoyaltyId != null && customer.BusinessId == (Guid)filterValue);
                }
            } 
            if (filter.Contains("Sorting") && filter.Parameters["Sorting"] is string sorting)
            {
                if (!new[] { "asc", "desc", "ascending", "descending" }.Contains(sorting))
                {
                    sorting = "asc";
                }
                if (filter.Contains("OrderBy") && filter.Parameters["OrderBy"] is string fieldName)
                {
                    var propertyMap = new Dictionary<string, string>
                    {
                        { "firstname", "FirstName" },
                        { "lastname", "LastName" },
                        { "birthday", "Birthday" },
                        { "address", "Address" },
                        { "points", "Points" }
                    };

                    if (propertyMap.TryGetValue(fieldName.ToLower(), out var propertyName))
                    {
                        var orderExpression = $"{propertyName} {sorting.ToLower()}";
                        query = query.OrderBy(orderExpression);
                    }
                }
            }

            if (filter.Contains("PageIndex") && filter.Parameters["PageIndex"] is int pageIndex)
            {
                query = query.Skip(pageIndex);
            }

            if (filter.Contains("PageSize") && filter.Parameters["PageSize"] is int pageSize)
            {
                query = query.Take(pageSize);
            }

            var customerList = await query.ToListAsync();
            return Mapping.Mapper.Map<List<Customer>, List<CustomerResponse>>(customerList);
        }


        public async Task<CustomerResponse?> GetCustomerByIdAsync(Guid customerId)
        {

            var customer = await _dbcontext.Customers.FindAsync(customerId);
            return Mapping.Mapper.Map<CustomerResponse>(customer);
        }

        public async Task<bool> LogoutUserAsync(Guid businessId, string loginName)
        {
            var user = await GetUserByLoginNameAndBusiness(businessId, loginName);
            if (user == null)
            {
                return false;
            }
            else
            {
                //var something = await _dbcontext.UserLogins.ToListAsync();
                var nonLoggedOutUser = await _dbcontext.UserLogins
                    .FirstOrDefaultAsync(userLogin => userLogin.UserId == user.UserId && userLogin.LogoutDate == userLogin.LoginDate);

                if(nonLoggedOutUser != null)
                {
                    nonLoggedOutUser.LogoutDate = DateTime.Now;
                    await _dbcontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<CustomerResponse?> AddCustomerAsync(CustomerRequest cutomerRequest, byte[] passwordHash, byte[] passwordSalt)
        {

            var customer = Mapping.Mapper.Map<Customer>(cutomerRequest);

            var user = Mapping.Mapper.Map<User>(cutomerRequest);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleName = "Customer";

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
           
            customer.UserId = user.Id;
            var userLogin = new UserLogin
            {
                UserId = user.Id,
                LoginDate = DateTime.Now,
                LogoutDate = DateTime.Now,
            };

            _dbcontext.Customers.Add(customer);
            _dbcontext.UserLogins.Add(userLogin);
            await _dbcontext.SaveChangesAsync();

            return Mapping.Mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse?> UpdateCustomerAsync(CustomerRequest updatedCustomerDTO)
        {
            var user = await GetUserByLoginNameAndBusiness(updatedCustomerDTO.BusinessId, updatedCustomerDTO.LoginName);
            var updatedCustomer = Mapping.Mapper.Map<Customer>(updatedCustomerDTO);
            var existingCustomer = await _dbcontext.Customers.FirstOrDefaultAsync(customer => customer.UserId == user.UserId);

            if (existingCustomer != null)
            {
                updatedCustomer.UserId = existingCustomer.UserId;
                updatedCustomer.Id = existingCustomer.Id;
                _dbcontext.Entry(existingCustomer).CurrentValues.SetValues(updatedCustomer);
                await _dbcontext.SaveChangesAsync();

                return Mapping.Mapper.Map<CustomerResponse>(existingCustomer);
            }
            return null;
        }



        public async Task<bool> DeleteCustomerAsync(Guid customerId)
        {
            var customer = await _dbcontext.Customers.FirstOrDefaultAsync(customer => customer.Id == customerId);
            if (customer != null)
            {
                _dbcontext.Customers.Remove(customer);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CustomerResponse?> GetCustomerByUserIdAsync(Guid userId)
        {
            var customer = await _dbcontext.Customers.FirstOrDefaultAsync(customer => customer.UserId == userId);
            return Mapping.Mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse?> LoginCutomerWithLoginNameAsync(Guid businessId, string loginName)
        {
            var user = await GetUserByLoginNameAndBusiness(businessId, loginName);
            if (user != null)
            {
                var isUserLoggedOut = !await _dbcontext.UserLogins
                    .Where(userLogin => userLogin.UserId == user.UserId)
                    .AnyAsync(userLogin => userLogin.LogoutDate == userLogin.LoginDate);
                if (isUserLoggedOut)
                {
                    var currentTime = DateTime.UtcNow;
                    _dbcontext.UserLogins.Add(new UserLogin
                    {
                        UserId = user.UserId,
                        LoginDate = currentTime,
                        LogoutDate = currentTime
                    });
                    await _dbcontext.SaveChangesAsync();

                    return await GetCustomerByUserIdAsync((Guid)user.UserId!);
                }
                else return null;
            }
            return null;
        }

        // Staff

        public async Task<List<StaffResponse>> GetStaffMembersAsync()
        {
            var staffList = await _dbcontext.StaffMembers.ToListAsync();
            return Mapping.Mapper.Map<List<Staff>, List<StaffResponse>>(staffList);
        }

        public async Task<IEnumerable<StaffResponse>> GetAllStaffAsync(Filter filter)
        {
            IQueryable<Staff> query = _dbcontext.StaffMembers;

            foreach (var filterName in filter.Parameters.Keys)
            {
                var filterValue = filter.Parameters[filterName];
                if (filterName == "BusinessId" && filterValue != null)
                {
                    query = query.Where(staff => staff.BusinessId != null && staff.BusinessId == (Guid)filterValue);
                }
                else if (filterName == "RoleName" && filterValue != null)
                {
                    query = query.Where(staff => staff.RoleName != null && staff.RoleName == filterValue);
                }
                else if (filterName == "FirsName" && filterValue != null)
                {
                    query = query.Where(staff => staff.FirstName != null && staff.FirstName == filterValue);
                }
                else if (filterName == "LastName" && filterValue != null)
                {
                    query = query.Where(staff => staff.LastName != null && staff.LastName == filterValue);
                }
                else if (filterName == "PhoneNumber" && filterValue != null)
                {
                    query = query.Where(staff => staff.PhoneNumber != null && staff.PhoneNumber == filterValue);
                }
                else if (filterName == "Email" && filterValue != null)
                {
                    query = query.Where(staff => staff.Email != null && staff.Email == filterValue);
                }
                else if (filterName == "HireDate" && filterValue != null)
                {
                    query = query.Where(staff => staff.HireDate != null && staff.HireDate == (DateTime)filterValue);
                }


            }
            if (filter.Contains("Sorting") && filter.Parameters["Sorting"] is string sorting)
            {
                if (!new[] { "asc", "desc", "ascending", "descending" }.Contains(sorting))
                {
                    sorting = "asc";
                }
                if (filter.Contains("OrderBy") && filter.Parameters["OrderBy"] is string fieldName)
                {
                    var propertyMap = new Dictionary<string, string>
                    {
                        { "roleid", "RoleId" },
                        { "firstname", "FirstName" },
                        { "lastname", "LastName" },
                        { "birthday", "Birthday" },
                        { "rolename", "RoleName" },
                        { "phonenumber", "PhoneNumber" },
                        { "email", "Email" },
                        { "hiredate", "HireDate" },
                    };

                    if (propertyMap.TryGetValue(fieldName.ToLower(), out var propertyName))
                    {
                        var orderExpression = $"{propertyName} {sorting.ToLower()}";
                        query = query.OrderBy(orderExpression);
                    }
                }
            }

            if (filter.Contains("PageIndex") && filter.Parameters["PageIndex"] is int pageIndex)
            {
                query = query.Skip(pageIndex);
            }

            if (filter.Contains("PageSize") && filter.Parameters["PageSize"] is int pageSize)
            {
                query = query.Take(pageSize);
            }

            var employeeList = await query.ToListAsync();
            return Mapping.Mapper.Map<List<Staff>, List<StaffResponse>>(employeeList);
        }


        public async Task<StaffResponse?> GetStaffByIdAsync(Guid staffId)
        {
            var staff = await _dbcontext.Roles.FindAsync(staffId);
            return Mapping.Mapper.Map<StaffResponse>(staff);
        }


        public async Task AddStaffAsync(StaffRequest staffDTO)
        {
            var staff = Mapping.Mapper.Map<Staff>(staffDTO);
            _dbcontext.StaffMembers.Add(staff);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<StaffResponse?> AddStaffAsync(StaffRequest employeeRequest, byte[] passwordHash, byte[] passwordSalt)
        {

            var employee = Mapping.Mapper.Map<Staff>(employeeRequest);

            var user = Mapping.Mapper.Map<User>(employeeRequest);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleName = employee.RoleName;

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            employee.UserId = (Guid)user.Id!;
            var currentTime = DateTime.UtcNow;
            var userLogin = new UserLogin
            {
                UserId = user.Id,
                LoginDate = currentTime,
                LogoutDate = currentTime,
            };

            _dbcontext.StaffMembers.Add(employee);
            _dbcontext.UserLogins.Add(userLogin);
            await _dbcontext.SaveChangesAsync();

            return Mapping.Mapper.Map<StaffResponse>(employee);
        }

        public async Task<bool> UpdateStaffAsync(Guid staffId, StaffRequest updatedStaffDTO)
        {
            var updatedStaff = Mapping.Mapper.Map<Staff>(updatedStaffDTO);
            var existingStaff = await _dbcontext.StaffMembers.FindAsync(updatedStaff.Id);

            if (existingStaff != null)
            {
                _dbcontext.Entry(existingStaff).CurrentValues.SetValues(updatedStaff);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteStaffAsync(Guid staffId)
        {
            var staff = await _dbcontext.StaffMembers.FindAsync(staffId);
            if (staff != null)
            {
                _dbcontext.StaffMembers.Remove(staff);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<StaffResponse?> LoginStaffAsync(Guid businessId, string loginName)
        {
            var user = await GetUserByLoginNameAndBusiness(businessId, loginName);
            if (user != null)
            {
                var isUserLoggedOut = !await _dbcontext.UserLogins
                    .Where(userLogin => userLogin.UserId == user.UserId)
                    .AnyAsync(userLogin => userLogin.LogoutDate == userLogin.LoginDate);
                if (isUserLoggedOut)
                {
                    var currentTime = DateTime.UtcNow;
                    _dbcontext.UserLogins.Add(new UserLogin
                    {
                        UserId = user.UserId,
                        LoginDate = currentTime,
                        LogoutDate = currentTime
                    });
                    await _dbcontext.SaveChangesAsync();

                    return await GetStaffByUserIdAsync((Guid)user.UserId!);
                }
                else return null;
            }
            return null;
        }

        public async Task<StaffResponse?> GetStaffByUserIdAsync(Guid userId)
        {
            var employee = await _dbcontext.StaffMembers.FirstOrDefaultAsync(employee => employee.UserId == userId);
            return Mapping.Mapper.Map<StaffResponse>(employee);
        }

        public async Task<StaffResponse?> UpdateStaffAsync(StaffRequest updatedEmployeeDTO)
        {
            var user = await GetUserByLoginNameAndBusiness(updatedEmployeeDTO.BusinessId, updatedEmployeeDTO.LoginName);
            var updatedEmployee = Mapping.Mapper.Map<Customer>(updatedEmployeeDTO);
            var existingEmployee = await _dbcontext.StaffMembers.FirstOrDefaultAsync(employee => employee.UserId == user.UserId);

            if (existingEmployee != null)
            {
                updatedEmployee.UserId = existingEmployee.UserId;
                updatedEmployee.Id = existingEmployee.Id;
                _dbcontext.Entry(existingEmployee).CurrentValues.SetValues(updatedEmployee);
                await _dbcontext.SaveChangesAsync();

                return Mapping.Mapper.Map<StaffResponse>(existingEmployee);
            }
            return null;
        }

        // Roles

        public async Task<List<RoleResponse>> GetRolesAsync(Filter filter)
        {
            IQueryable<Role> query = _dbcontext.Roles;

            if (filter.Contains("RoleId") && filter.Parameters["RoleId"] is Guid roleId)
            {
                query = query.Where(role => role.Id == roleId);
            }
            if (filter.Contains("RoleName") && filter.Parameters["RoleName"] is string roleName)
            {
                query = query.Where(role => role.RoleName == roleName);
            }

            if (filter.Contains("Sorting") && filter.Parameters["Sorting"] is string sorting)
            {
                if (!new[] { "asc", "desc", "ascending", "descending" }.Contains(sorting))
                {
                    sorting = "asc";
                }
                if (filter.Contains("OrderBy") && filter.Parameters["OrderBy"] is string fieldName)
                {
                    var propertyMap = new Dictionary<string, string>
                    {
                        { "description", "Description" },
                        { "rolename", "RoleName" }
                    };

                    if (propertyMap.TryGetValue(fieldName.ToLower(), out var propertyName))
                    {
                        var orderExpression = $"{propertyName} {sorting.ToLower()}";
                        query = query.OrderBy(orderExpression);
                    }
                }
            }

            if (filter.Contains("PageIndex") && filter.Parameters["PageIndex"] is int pageIndex)
            {
                query = query.Skip(pageIndex);
            }

            if (filter.Contains("PageSize") && filter.Parameters["PageSize"] is int pageSize)
            {
                query = query.Take(pageSize);
            }

            var roleList = await query.ToListAsync();
            return Mapping.Mapper.Map<List<Role>, List<RoleResponse>>(roleList);

        }

        public async Task<RoleResponse?> GetRoleAsync(string roleName)
        {
            var role = await _dbcontext.Roles.FirstOrDefaultAsync(role => role.RoleName == roleName);
            return Mapping.Mapper.Map<RoleResponse>(role);
        }

        public async Task<RoleResponse?> CreateRoleAsync(RoleRequest roleDTO)
        {
            var role = Mapping.Mapper.Map<Role>(roleDTO);
            if(!await _dbcontext.Roles.AnyAsync(role => role.RoleName == roleDTO.RoleName))
            {
                _dbcontext.Roles.Add(role);
                await _dbcontext.SaveChangesAsync();
                return Mapping.Mapper.Map<RoleResponse>(role);
            }
            return null;
        }

        public async Task<RoleResponse?> UpdateRoleAsync(RoleRequest updatedRoleDTO)
        {
            var updatedRole = Mapping.Mapper.Map<Role>(updatedRoleDTO);
            var existingRole = await _dbcontext.Roles.FirstOrDefaultAsync(role => role.RoleName == updatedRoleDTO.RoleName);
            if (existingRole != null)
            {
                updatedRole.Id = existingRole.Id;
                _dbcontext.Entry(existingRole).CurrentValues.SetValues(updatedRole);
                await _dbcontext.SaveChangesAsync();
                return Mapping.Mapper.Map<RoleResponse>(existingRole); ;
            }
            return null;
        }

        public async Task<bool> IsRoleUsedAsync(string roleName)
        {
            if(await _dbcontext.Users.AnyAsync(user => user.RoleName == roleName))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            var role = await _dbcontext.Roles.FirstOrDefaultAsync(role => role.RoleName == roleName);
            if (role != null)
            {
                _dbcontext.Roles.Remove(role);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
        //Admin

        public async Task<UserResponse?> GetAdminAsync()
        {
            var user = await _dbcontext.Users.SingleOrDefaultAsync(user => user.RoleName == "Admin");
            return Mapping.Mapper.Map<UserResponse>(user);
        }


        //Users

        public async Task<UserResponse?> CreateUserAsync(UserInner user)
        {
            var newUser = Mapping.Mapper.Map<User>(user);
            _dbcontext.Users.Add(newUser);
            await _dbcontext.SaveChangesAsync();
            return Mapping.Mapper.Map<UserResponse>(newUser);
        }

        public async Task<UserInner?> GetUserByLoginNameAndBusiness(Guid businessId, string loginName)
        {


            var user = await _dbcontext.Users
                .Where(user => user.LoginName == loginName &&
                    (_dbcontext.Customers.Any(customer => customer.BusinessId == businessId && customer.UserId == user.Id) ||
                    _dbcontext.StaffMembers.Any(employee => employee.BusinessId == businessId && employee.UserId == user.Id)))
                .SingleOrDefaultAsync();

            if (user == null){
                return null;
            }

            else{
                var userInner = Mapping.Mapper.Map<UserInner>(user);
                userInner.BusinessId = businessId;
                return userInner;
            }     
        }

        public async Task<UserInner?> GetUserByLoginNameAndRole(string roleName, string loginName)
        {


            var user = await _dbcontext.Users
                .SingleOrDefaultAsync(user => user.LoginName == loginName && user.RoleName == roleName);

            if (user == null)
            {
                return null;
            }

            else
            {
                var userInner = Mapping.Mapper.Map<UserInner>(user);
                return userInner;
            }
        }

        public async Task<IEnumerable<UserLoginResponse>> GetUserLoginSessionsAsync(Guid userId)
        {
            IQueryable<UserLogin> query = _dbcontext.UserLogins;
            var sessionList =  await query.Where(UserLogin => UserLogin.UserId == userId).ToListAsync();
            return Mapping.Mapper.Map<List<UserLogin>, List<UserLoginResponse>>(sessionList);
        }


    }
}
