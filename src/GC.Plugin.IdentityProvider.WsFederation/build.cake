#addin "nuget:?package=Cake.Figlet&version=1.2.0"

#load "../../scripts/clean.cake"
#load "../../scripts/pack.cake"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information(Figlet("Sitecore Identity"));
	Information("WSFederation Plugin");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

Task("Default")
	.IsDependentOn("Pack");

RunTarget(target);