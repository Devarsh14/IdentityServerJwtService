
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


using IdentityModel;
using IdentityServerJwtService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace PasswordHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=A000165Identity;Trusted_Connection=True;MultipleActiveResultSets=true"));
            Console.WriteLine("DbContextAccesedfromth console.");
            services.AddLogging();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            var provider = services.BuildServiceProvider();
            var writer = provider.GetService<ApplicationDbContext>();
            var userservice = provider.GetService<UserManager<ApplicationUser>>();

            ApplicationUser usercreate = new ApplicationUser();
            usercreate.UserName = "dshukla";


            userservice.CreateAsync(usercreate, "Dcs1989");


            Console.WriteLine("New user created");
            Console.ReadLine();

            var userMgr = provider.GetRequiredService<UserManager<ApplicationUser>>();
            var alice = userMgr.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice"
                };
                var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                alice = userMgr.FindByNameAsync("alice").Result;

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                                new Claim(JwtClaimTypes.Name, "Alice Smith"),
                                new Claim(JwtClaimTypes.GivenName, "Alice"),
                                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                                new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                                new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                                new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                            }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Console.WriteLine("alice created");
            }
            else
            {
                Console.WriteLine("alice already exists");
            }


        }
    }
}

