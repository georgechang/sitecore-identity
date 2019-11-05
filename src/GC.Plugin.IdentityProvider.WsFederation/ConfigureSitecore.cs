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
			// inject Sitecore Host logging service
			_logger = logger;

			// fetch settings from configuration files
			_appSettings = new AppSettings();
			configuration.GetSection(AppSettings.SectionName).Bind(this._appSettings.WsFederationIdentityProvider);
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// use the loaded settings object
			var wsFederationIdentityProvider = _appSettings.WsFederationIdentityProvider;

			if (!wsFederationIdentityProvider.Enabled)
				return;

			var replyUri = new Uri(wsFederationIdentityProvider.Wtrealm);

			_logger.LogDebug($"Configure \'{wsFederationIdentityProvider.DisplayName}\'. AuthenticationScheme = {wsFederationIdentityProvider.AuthenticationScheme}, MetadataAddress = {wsFederationIdentityProvider.MetadataAddress}, Wtrealm = {wsFederationIdentityProvider.Wtrealm}");

			// configure the Microsoft WS-Federation OWIN library
			new AuthenticationBuilder(services).AddWsFederation(wsFederationIdentityProvider.AuthenticationScheme, wsFederationIdentityProvider.DisplayName, options =>
			{
				options.SignInScheme = "idsrv.external";
				options.MetadataAddress = wsFederationIdentityProvider.MetadataAddress;
				options.Wtrealm = wsFederationIdentityProvider.Wtrealm;
			});
		}
	}
}
