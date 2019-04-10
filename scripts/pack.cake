var output = Argument("output", "");

Task("Pack")
	.IsDependentOn("Clean")
	.DoesForEach(GetFiles("src/**/*.csproj"), project =>
	{
		var version = GitVersion();

		var settings = new DotNetCorePackSettings {
			Configuration = configuration,
			MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(version.AssemblySemVer)
		};

		if (!string.IsNullOrEmpty(output))
		{
			settings.OutputDirectory = output;
		}

		DotNetCorePack(project.GetDirectory().FullPath, settings);
	});
