# SeleniumSharper
![Build & Test](https://img.shields.io/github/actions/workflow/status/maik-hasler/SeleniumSharper/dotnet.yml?branch=main&label=Build%20%26%20Tests)

SeleniumSharper is a powerful, lightweight tool that makes working with Selenium much easier. By offering a set of useful extensions and tools, it enhances the experience of working with Selenium.

# Table of contents
- [Introduction](#seleniumsharper)
- [Table of contents](#table-of-contents)
- [Installation](#installation)
- [Usage](#usage)

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
```
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var driver = new ChromeDriver();

var element = driver.Wait(30).Until(WaitConditions.ElementExists(By.CssSelector(".myclass")));
```
