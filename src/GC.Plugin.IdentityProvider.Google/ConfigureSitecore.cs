using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Runtime.Configuration;
using GC.Plugin.IdentityProvider.Google.Configuration;

namespace GC.Plugin.IdentityProvider.Google
{
	public class ConfigureSitecore
	{
		private readonly ILogger<ConfigureSitecore> _logger;
		private readonly AppSettings _appSettings;

		public ConfigureSitecore(ISitecoreConfiguration configuration, ILogger<ConfigureSitecore> logger)
		{
			_logger = logger;
			_appSettings = new AppSettings();
			configuration.GetSection(AppSettings.SectionName).Bind(this._appSettings.GoogleIdentityProvider);
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var googleProvider = _appSettings.GoogleIdentityProvider;

			if (!googleProvider.Enabled)
				return;

			_logger.LogDebug($"Configure \'{googleProvider.DisplayName}\'. AuthenticationScheme = {googleProvider.AuthenticationScheme}, ClientId = {googleProvider.ClientId}, ClientSecret = {googleProvider.ClientSecret}");
			new AuthenticationBuilder(services).AddGoogle(googleProvider.AuthenticationScheme, googleProvider.DisplayName, options =>
			{
				options.SignInScheme = "idsrv.external";
				options.ClientId = googleProvider.ClientId;
				options.ClientSecret = googleProvider.ClientSecret;
			});
		}
	}
}
