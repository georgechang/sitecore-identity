#addin "nuget:?package=Cake.Figlet&version=1.3.1"

#load "scripts/clean.cake"
#load "scripts/pack.cake"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information(Figlet("Sitecore Identity"));
	Information("Authentication Plugins");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

Task("Default")
	.IsDependentOn("Pack");

RunTarget(target);