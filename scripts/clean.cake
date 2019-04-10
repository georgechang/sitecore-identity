Task("Clean")
	.DoesForEach(GetFiles("*.csproj"), project =>
	{
		DotNetCoreClean(project.GetDirectory().FullPath);
	}
);