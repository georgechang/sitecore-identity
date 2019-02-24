namespace GC.Plugin.IdentityProvider.Facebook
{
    public class FacebookIdentityProvider : Sitecore.Plugin.IdentityProviders.IdentityProvider
    {
        public string ApplicationId { get; set; }

        public string ApplicationSecret { get; set; }
    }
}
