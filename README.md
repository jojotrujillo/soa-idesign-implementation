# To install the Solution Template, from a command prompt:
dotnet new -i <FILE_SYSTEM_DIRECTORY>

Example:
	dotnet new -i C:\Dev\SolutionTemplate

# To create a solution with the Solution Template:
dotnet new net6-template -n {YourCompanyName}.{YourAppName}

Example:
	dotnet new net6-template -n MyBiz.MyCoreApp

# To uninstall the template:
dotnet new -u net6-template

# References:
- [Custom templates for dotnet new](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates)
- [Create a custom template for dotnet new](https://docs.microsoft.com/en-us/dotnet/core/tutorials/create-custom-template)
- [How to create your own templates for dotnet new](https://blogs.msdn.microsoft.com/dotnet/2017/04/02/how-to-create-your-own-templates-for-dotnet-new/)
