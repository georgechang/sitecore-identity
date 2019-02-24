using System;
using GC.Plugin.IdentityProvider.WsFederation.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Runtime.Configuration;

namespace GC.Plugin.IdentityProvider.WsFederation
{
    public class ConfigureSitecore
    {
        private readonly ILogger<ConfigureSitecore> _logger;
        private readonly AppSettings _appSettings;

        public ConfigureSitecore(ISitecoreConfiguration configuration, ILogger<ConfigureSitecore> logger)
        {
            _logger = logger;
            _appSettings = new AppSettings();
            configuration.GetSection(AppSettings.SectionName).Bind(this._appSettings.WsFederationIdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var wsFederationIdentityProvider = _appSettings.WsFederationIdentityProvider;

            if (!wsFederationIdentityProvider.Enabled)
                return;

            var replyUri = new Uri(wsFederationIdentityProvider.Wtrealm);

            _logger.LogDebug($"Configure \'{wsFederationIdentityProvider.DisplayName}\'. AuthenticationScheme = {wsFederationIdentityProvider.AuthenticationScheme}, MetadataAddress = {wsFederationIdentityProvider.MetadataAddress}, Wtrealm = {wsFederationIdentityProvider.Wtrealm}");
            new AuthenticationBuilder(services).AddWsFederation(wsFederationIdentityProvider.AuthenticationScheme, wsFederationIdentityProvider.DisplayName, options =>
            {
                options.SignInScheme = "idsrv.external";
                options.MetadataAddress = wsFederationIdentityProvider.MetadataAddress;
                options.Wtrealm = wsFederationIdentityProvider.Wtrealm;
                options.Wreply = $"{replyUri.Scheme}{Uri.SchemeDelimiter}{replyUri.Authority}/{wsFederationIdentityProvider.AuthenticationScheme}";
            });
        }
    }
}
