Task("Clean")
	.DoesForEach(GetFiles("src/**/*.csproj"), project =>
	{
		DotNetCoreClean(project.GetDirectory().FullPath);
	}
);