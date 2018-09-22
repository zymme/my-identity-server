using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Saml;
using IdentityServer4.Saml.Models;
using IdentityServer4.Test;

namespace MyIdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // where to redirect after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" }, 

                    // where to redirect after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "https://peak10dev.service-now.com",
                    ClientName = "Peak10 Dev Service Now",
                    ProtocolType = IdentityServerConstants.ProtocolTypes.Saml2p,
                    AllowedScopes = { "openid", "profile" },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://peak10dev.service-now.com/consumer.do" },
                    //PostLogoutRedirectUris = { "https://peak10dev.service-now.com/navpage.do" }
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                }
            };

        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password", 
                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "testuser-vwestlab01",
                    Username = "test@vwestlab01.local",
                    Password = "demo1demo!",
                    ProviderName = "https://peak10dev.service-now.com/navpage.do",


                },
                new TestUser
                {
                    SubjectId = "dave.zimmer@flexential.com",
                    Username = "dave.zimmer@flexential.com",
                    Password = "demo1demo1",
                    ProviderName = "https://peak10dev.service-now.com/navpage.do",
                    Claims = new []
                    {
                        //new Claim(JwtClaimTypes.Subject, "dave.zimmer@flexential.com"),
                        new Claim(JwtClaimTypes.Name, "Dave Zimmer"),
                       

                    }
                   
                },
                new TestUser
                {
                    SubjectId = "Test.User@viawest.com",
                    Username = "Test.User@viawest.com",
                    Password = "demo2demo2",
                    ProviderName = "https://peak10dev.service-now.com/navpage.do",
                    Claims = new []
                    {
                        //new Claim(JwtClaimTypes.Subject, "dave.zimmer@flexential.com"),
                        new Claim(JwtClaimTypes.Name, "Test Zimmer"),

                    }
                },
                new TestUser
                {
                    SubjectId = "Madeup.TestUser@viawest.com",
                    Username = "Madeup.TestUser@viawest.com",
                    Password = "test123",
                    ProviderName = "https://peak10dev.service-now.com/navpage.do",
                    Claims = new []
                    {
                        //new Claim(JwtClaimTypes.Subject, "dave.zimmer@flexential.com"),
                        new Claim(JwtClaimTypes.Name, "Madeup TestUser Zimmer"),

                    }
                }
            };
        }

        public static IEnumerable<ServiceProvider> GetServiceProviders()
        {
            return new[]
            {
                new ServiceProvider
                {
                    EntityId = "https://peak10dev.service-now.com",
                    //SigningCertificates = clientcerts,
                    
                    RequireSamlRequestDestination = false,
                    AssertionConsumerServices = { new Service(SamlConstants.BindingTypes.HttpPost, "https://peak10dev.service-now.com/navpage.do") }
                }
            };
        }


    }




}
