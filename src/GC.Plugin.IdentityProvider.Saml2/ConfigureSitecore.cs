using System;
using GC.Plugin.IdentityProvider.Saml2.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Runtime.Configuration;
using Sustainsys.Saml2.Configuration;
using Sustainsys.Saml2.Metadata;

namespace GC.Plugin.IdentityProvider.Saml2
{
    public class ConfigureSitecore
    {
        private readonly ILogger<ConfigureSitecore> _logger;
        private readonly AppSettings _appSettings;

        public ConfigureSitecore(ISitecoreConfiguration configuration, ILogger<ConfigureSitecore> logger)
        {
            _logger = logger;
            _appSettings = new AppSettings();
            configuration.GetSection(AppSettings.SectionName).Bind(this._appSettings.Saml2IdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var saml2IdentityProvider = _appSettings.Saml2IdentityProvider;

            if (!saml2IdentityProvider.Enabled)
                return;

            _logger.LogDebug($"Configure \'{saml2IdentityProvider.DisplayName}\'. AuthenticationScheme = {saml2IdentityProvider.AuthenticationScheme}");
            new AuthenticationBuilder(services).AddSaml2(saml2IdentityProvider.AuthenticationScheme, saml2IdentityProvider.DisplayName, options =>
            {
                options.SignInScheme = "idsrv.external";
                options.SPOptions = new SPOptions {
                    EntityId = new EntityId(saml2IdentityProvider.ServiceProviderConfiguration.EntityId),
                    ReturnUrl = new Uri(saml2IdentityProvider.ServiceProviderConfiguration.ReturnUrl)
                };
                options.IdentityProviders.Add(new Sustainsys.Saml2.IdentityProvider(new EntityId(saml2IdentityProvider.IdentityProviderConfiguration.EntityId), options.SPOptions) 
                {
                    MetadataLocation = saml2IdentityProvider.IdentityProviderConfiguration.MetadataLocation
                });
            });
        }
    }
}
