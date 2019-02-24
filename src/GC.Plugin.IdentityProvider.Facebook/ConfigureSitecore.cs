using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Runtime.Configuration;
using GC.Plugin.IdentityProvider.Facebook.Configuration;

namespace GC.Plugin.IdentityProvider.Facebook
{
    public class ConfigureSitecore
    {
        private readonly ILogger<ConfigureSitecore> _logger;
        private readonly AppSettings _appSettings;

        public ConfigureSitecore(ISitecoreConfiguration configuration, ILogger<ConfigureSitecore> logger)
        {
            _logger = logger;
            _appSettings = new AppSettings();
            configuration.GetSection(AppSettings.SectionName).Bind(this._appSettings.FacebookIdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var facebookProvider = _appSettings.FacebookIdentityProvider;

            if (!facebookProvider.Enabled)
                return;

            _logger.LogDebug($"Configure \'{facebookProvider.DisplayName}\'. AuthenticationScheme = {facebookProvider.AuthenticationScheme}, ApplicationId = {facebookProvider.ApplicationId}, ApplicationSecret = {facebookProvider.ApplicationSecret}");
            new AuthenticationBuilder(services).AddFacebook(facebookProvider.AuthenticationScheme, facebookProvider.DisplayName, options =>
            {
                options.SignInScheme = "idsrv.external";
                options.AppId = facebookProvider.ApplicationId;
                options.AppSecret = facebookProvider.ApplicationSecret;
            });
        }
    }
}
