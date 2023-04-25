# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased](https://github.com/maik-hasler/SeleniumSharper/compare/v1.2.0...HEAD)

## [v1.2.0](https://github.com/maik-hasler/SeleniumSharper/releases/tag/v1.2.0)
**Published:** 25th April 2023
### Added
- Static class `WebDriverManager` to simplify the download / installation process
- Support for `FirefoxDriver`
- Option to provide a custom configuration
### Fixed
- `ContextualWait<TSearchContext>` can now return `WebElementsConditionBuilder<TSearchContext, TSearchResult>`
### Changed
- `IWebDriverManager` is no longer returning the instance of `IWebDriver`. Instead it retuns the path to the downloaded driver binary.
- Changed many class and method implementations related to `IWebDriverManager`

## [v1.1.0](https://github.com/maik-hasler/SeleniumSharper/releases/tag/v1.1.0)
**Published:** 21th April 2023
### Added
- `IWebDriverManager<TOptions>` and first implementation for Chrome to automatically install driver binaries
- Helper classes related to `IWebDriverManager<TOptions>`
- Enum and extension method to fire JavaScript event
### Changed
- Upgrade dependencies: Selenium.WebDriver, Selenium.Support etc.
### Removed
- Custom result classes

## [v1.0.0](https://github.com/maik-hasler/SeleniumSharper/releases/tag/v1.0.0)
**Published:** 19th April 2023
### Added
- `WebElementsConditionBuilder<TSearchContext, TSearchResult>` to build wait conditions for a `ReadOnlyCollection<IWebElement>`
- `WebElementConditionBuilder<TSearchContext, TSearchResult>` to build wait conditions for a `IWebElement`
- `ClassConditionBuilder<TSearchContext, TSearchResult>` to build wait conditions for a `IEquatable<string>`
- Custom result classes `WebElementsVisibilityResult` and `WebElementVisibilityResult`
### Changed
- Changed `Waiter<T>` to `ContextualWait<TSearchContext>`, which supports more generic method chaining
### Removed
- Collection of commonly used selenium wait conditions

## [v1.0.0-preview.0](https://github.com/maik-hasler/SeleniumSharper/releases/tag/v1.0.0-preview.0)
**Published:** 18th April 2023
<br />
**Disclaimer:** This is a pre-release. It was published in order to verify, that the nuget.yml workflow works fine.
### Added
- `Waiter<T>` to wait for a specified condition to be satisfied
- Collection of commonly used selenium wait conditions
- Extension method to create a `Waiter<T>` object from an `ISearchContext`
- Various `IJavaScriptExecutor` extension methods
