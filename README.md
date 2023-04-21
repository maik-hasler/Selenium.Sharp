# SeleniumSharper
![Build & Test](https://img.shields.io/github/actions/workflow/status/maik-hasler/SeleniumSharper/dotnet.yml?branch=main&label=Build%20%26%20Tests)
![NuGet Downloads](https://img.shields.io/nuget/dt/SeleniumSharper)

SeleniumSharper is a powerful, lightweight tool that makes working with Selenium much easier. By offering a set of useful extensions and tools, it enhances the experience of working with Selenium.

# Table of contents
- [Introduction](#seleniumsharper)
- [Table of contents](#table-of-contents)
- [Installation](#installation)
- [Usage](#usage)
- [Credits](#credits)
    - [WebDriverManager.Net](#webdrivermanagernet)
    - [DotNetSeleniumExtras](#dotnetseleniumextras)

# Installation
You should install [SeleniumSharper with NuGet](https://www.nuget.org/packages/SeleniumSharper):
```
Install-Package SeleniumSharper
```
Or via the .NET Core command line interface:
```
dotnet add package SeleniumSharper
```
Either commands, from Package Manager Console or .NET Core CLI, will download and install SeleniumSharper and all required dependencies.

# Usage
```csharp
var title = driver.Wait(30)
    .Until(ctx => ctx.Title)
    .Satisfies(title => title.Equals("My awesome title"));
```
# Credits
I would like to give credit to the following resources that have inspired this project.
## [WebDriverManager.Net](https://github.com/rosolko/WebDriverManager.Net)
The idea for automatically downloading and installing driver binaries was inspired by the repository [WebDriverManager.Net](https://github.com/rosolko/WebDriverManager.Net). However, this project uses a slightly different approach by creating a driver manager that returns the instance of the `IWebDriver`, making it even easier to work with it.
## [DotNetSeleniumExtras](https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras)
The idea for implementing an enhanced methodology to wait for conditions to be satisfied in Selenium was inspired by the repository [DotNetSeleniumExtras](https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras). Fascinated by the concept, I developed my own implementation. The waiting methods included in this library are designed to resemble a sentence and are capable of accepting `Func<>` delegates, thereby increasing their flexibility and variety.
