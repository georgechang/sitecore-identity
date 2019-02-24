namespace GC.Plugin.IdentityProvider.WsFederation
{
    public class WsFederationIdentityProvider : Sitecore.Plugin.IdentityProviders.IdentityProvider
    {
        public string Wtrealm { get; set; }

        public string MetadataAddress { get; set; }
    }
}
