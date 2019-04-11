using GC.Plugin.IdentityProvider.Saml2.Configuration;

namespace GC.Plugin.IdentityProvider.Saml2
{
    public class Saml2IdentityProvider : Sitecore.Plugin.IdentityProviders.IdentityProvider
    {
        public IdentityProviderConfiguration IdentityProviderConfiguration { get; set; }

        public ServiceProviderConfiguration ServiceProviderConfiguration { get; set; }
    }
}
