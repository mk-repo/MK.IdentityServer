using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace MK.IdentityServer.Host.Configuration
{
	public class Resources
	{
        public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new[]
			{
                // some standard scopes from the OIDC spec
                new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),

                // custom identity resource with some consolidated claims
                new IdentityResource("custom.profile", new[] { IdentityModel.JwtClaimTypes.Name, IdentityModel.JwtClaimTypes.Email, "location" })
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new[]
			{
                // simple version with ctor
                new ApiResource("api1", "Some API 1")
				{
                    // this is needed for introspection when using reference tokens
                    ApiSecrets = { new Secret("secret".Sha256()) }
				},
                
                // expanded version if more control is needed
                new ApiResource
				{
					Name = "api2",

					ApiSecrets =
					{
						new Secret("secret".Sha256())
					},

					UserClaims =
					{
						IdentityModel.JwtClaimTypes.Name,
						IdentityModel.JwtClaimTypes.Email
					},

					Scopes =
					{
						new Scope()
						{
							Name = "api2.full_access",
							DisplayName = "Full access to API 2",
						},
						new Scope
						{
							Name = "api2.read_only",
							DisplayName = "Read only access to API 2"
						}
					}
				}
			};
		}
	}
}
