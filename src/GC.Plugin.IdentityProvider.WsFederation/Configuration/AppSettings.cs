namespace GC.Plugin.IdentityProvider.WsFederation.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:WsFederation";

        public WsFederationIdentityProvider WsFederationIdentityProvider { get; set; } = new WsFederationIdentityProvider();
    }
}
