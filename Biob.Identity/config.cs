﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Biob.Identity
{
    public class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "Jason",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "Jason S"),
                        new Claim(JwtClaimTypes.PhoneNumber, "1234567"),
                        new Claim(JwtClaimTypes.Role, "customer"),
                    }
                },
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "Mikkel",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "Mikkel H"),
                        new Claim(JwtClaimTypes.PhoneNumber, "1234567"),
                        new Claim(JwtClaimTypes.Role, "admin"),
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "Biob.Web",
                    DisplayName = "BioB web api",
                    UserClaims = new List<string>  { JwtClaimTypes.Role },
                    ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                    Scopes = new List<Scope>
                    {
                        new Scope("read"),
                        new Scope("write")
                    }

                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "Biob",
                    ClientName = "Biob",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RedirectUris =
                    {
                        "https://localhost:44355/signin-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.Role,
                        "read"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource(JwtClaimTypes.PhoneNumber, "Your phone number", new List<string> {  JwtClaimTypes.PhoneNumber}),
                new IdentityResource(JwtClaimTypes.Role, "Your role(s)", new List<string>() { JwtClaimTypes.Role })
            };
        }
    }
}