# Changelog

All notable changes to this project are documented here.

## 0.1.0-alpha.8 — 2026-08-06

### Highlights

- Separate stages for clarity when building a Visual Tree, `ControlTemplate` and `Style` using WPFX.

### Features
- WPFX: Added `Transform` types. Space savings using expression-bodied members.
- `Demo_07_Style` & `Demo_11_ControlTemplate` logic improvements and tidy-ups.

## 0.1.0-alpha.7 — 2026-08-05

### Highlights

- **Breaking change:** Simplified `Grid` extension methods for adding rows and columns, removing the `DefinitionX` suffix.
- Rationale: `Grid.AddRow()` and `Grid.AddColumn()` are _extension_ methods that do not clash with the existing WPF framework, therefore the `X` suffix is not required.

### Breaking Changes

- Renamed `AddRowDefinitionX()` -> `AddRow()`.
- Renamed `AddColumnDefinitionX()` -> `AddColumn()`.

### Features
- `Demo_08_FluentTheme` logic improvements, and suppress warning moved to `csproj`.

## 0.1.0-alpha.6 — 2026-08-02

### Highlights
- WPFX helper API cleaned and standardized (generic `FrameworkElementX` factory; many helpers added/normalized)
- Improved binding support for `BindingX`. Also includes `MultiBindingX`.

### Features
- `PathStringX`: expression-to-path-string conversion, e.g. "ViewModel.SelectedCat.Type".
- MVVM demo apps revised to showcase the new binding features.