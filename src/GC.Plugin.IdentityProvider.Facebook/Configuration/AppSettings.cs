using System;
using System.Collections.Generic;
using System.Text;

namespace GC.Plugin.IdentityProvider.Facebook.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Facebook";

        public FacebookIdentityProvider FacebookIdentityProvider { get; set; } = new FacebookIdentityProvider();
    }
}
