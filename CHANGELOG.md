# Changelog
All notable changes to this project will be documented in this file.

## v1.0.0
**Published:** 19th April 2023
### Added
### Changed
- Changes `Waiter<T>` to `ContextualWait<TSearchContext>`, which allows
### Removed
- Collection of commonly used selenium wait conditions

## v1.0.0-preview.0
**Published:** 18th April 2023
<br />
**Disclaimer:** This is a pre-release. It was published in order to verify, that the nuget.yml workflow works fine.
### Added
- `Waiter<T>` to wait for a specified condition to be satisfied
- Collection of commonly used selenium wait conditions
- Extension method to create a `Waiter<T>` object from an `ISearchContext`
- Various `IJavaScriptExecutor` extension methods