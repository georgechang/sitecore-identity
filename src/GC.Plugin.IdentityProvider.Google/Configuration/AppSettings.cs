using System;
using System.Collections.Generic;
using System.Text;

namespace GC.Plugin.IdentityProvider.Google.Configuration
{
	public class AppSettings
	{
		public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Google";

		public GoogleIdentityProvider GoogleIdentityProvider { get; set; } = new GoogleIdentityProvider();
	}
}
