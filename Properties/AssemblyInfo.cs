using System.Reflection;
using SceneWarehouse;

[assembly: AssemblyTitle(BuildInfo.Description)]
[assembly: AssemblyDescription(BuildInfo.Description)]
[assembly: AssemblyCompany(BuildInfo.Company)]
[assembly: AssemblyProduct(BuildInfo.Name)]
[assembly: AssemblyCopyright("Developed by " + BuildInfo.Author)]
[assembly: AssemblyTrademark(BuildInfo.Company)]
[assembly: AssemblyVersion(BuildInfo.Version)]
[assembly: AssemblyFileVersion(BuildInfo.Version)]
[assembly: MelonLoader.MelonInfo(typeof(Main), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonLoader.MelonColor(System.ConsoleColor.White)]

[assembly: MelonLoader.MelonGame("Stress Level Zero", "BONELAB")]

// Gonna load right after Fusion since we don't wanna take it's spot at the top of BoneMenu :P
[assembly: MelonLoader.MelonPriority(-9999)]