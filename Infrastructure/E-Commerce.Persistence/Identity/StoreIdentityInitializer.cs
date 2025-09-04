using E_Commerce.Domain.Contracts.Persistence.DbInitializer;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Persistence.Common;
using E_Commerce.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Identity
{
    internal class StoreIdentityInitializer : DbInitializer, IStoreIdentityInitializer
    {
        private readonly StoreIdentityDbContext _storeIdentityDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StoreIdentityInitializer(StoreIdentityDbContext storeIdentityDbContext,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager) : base(storeIdentityDbContext)
        {
            _storeIdentityDbContext = storeIdentityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
 


        public override async Task SeedAsync()
        {
            //Roles
            var roleAdmin = new IdentityRole("Admin");
            var roleSuperAdmin = new IdentityRole("SuperAdmin");

            await  _roleManager.CreateAsync(roleAdmin);
            await _roleManager.CreateAsync(roleSuperAdmin);
            //Users 
            var userAdmin = new ApplicationUser()
            {
                DisplayName = "Mostafa",
                UserName = "Mostafa.Ahmed",
                Email = "mostafaaahmed022@gmail.com",
                PhoneNumber = "01033759259"
            };
            var userSuperAdmin = new ApplicationUser()
            {
                DisplayName = "Kareem Belal",
                UserName = "Kareem.Belal",
                Email = "KareemBelal@gmail.com",
                PhoneNumber = "01033759299"
            };
            

            await _userManager.CreateAsync(userAdmin, "P@ssw0rd");
            await _userManager.CreateAsync(userSuperAdmin, "P@ssw0rd");

            //Assign Users To Roles

            await _userManager.AddToRoleAsync(userAdmin, "Admin");
            await _userManager.AddToRoleAsync(userSuperAdmin, "SuperAdmin");



        }
    }
}
