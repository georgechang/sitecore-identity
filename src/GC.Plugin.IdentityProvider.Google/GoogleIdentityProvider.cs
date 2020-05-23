namespace GC.Plugin.IdentityProvider.Google
{
	public class GoogleIdentityProvider : Sitecore.Plugin.IdentityProviders.IdentityProvider
	{
		public string ClientId { get; set; }

		public string ClientSecret { get; set; }
	}
}
