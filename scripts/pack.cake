var output = Argument("output", "");

Task("Pack")
	.IsDependentOn("Clean")
	.DoesForEach(GetFiles("*.csproj"), project =>
	{
		var settings = new DotNetCorePackSettings {
			Configuration = configuration
		};

		if (!string.IsNullOrEmpty(output))
		{
			settings.OutputDirectory = output;
		}

		DotNetCorePack(project.GetDirectory().FullPath, settings);
	});
