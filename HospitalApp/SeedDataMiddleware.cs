﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HospitalApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class SeedDataMiddleware
{
    private readonly RequestDelegate _next;

    public SeedDataMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;
            var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scopedProvider.GetRequiredService<UserManager<IdentityUser>>();
            var userStore = scopedProvider.GetRequiredService<IUserStore<IdentityUser>>();

            // Checking and creating roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Doctor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Doctor"));
            }
            if (!await roleManager.RoleExistsAsync("Patient"))
            {
                await roleManager.CreateAsync(new IdentityRole("Patient"));
            }
            // Fetching admin users from configuration
            var adminUsersConfig = configuration.GetSection("AdminUsers").GetChildren();
            foreach (var adminUserConfig in adminUsersConfig)
            {
                var adminEmail = adminUserConfig["Email"];
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                var user = Activator.CreateInstance<ApplicationUser>();
                if (adminUser == null)
                {
                    await userStore.SetUserNameAsync(user, adminEmail, CancellationToken.None);
                    user.Name = adminUserConfig["FullName"];
                    user.Email = adminEmail;
                    user.PhoneNumber = adminUserConfig["PhoneNumber"];
                    string adminPassword = adminUserConfig["Password"];
                    var result = await userManager.CreateAsync(user, adminPassword);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }

        await _next(context);
    }
}
