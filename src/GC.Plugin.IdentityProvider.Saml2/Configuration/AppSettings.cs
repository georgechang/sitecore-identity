namespace GC.Plugin.IdentityProvider.Saml2.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Saml2";

        public Saml2IdentityProvider Saml2IdentityProvider { get; set; } = new Saml2IdentityProvider();
    }
}
