# Changelog

All notable changes to this project are documented here.

## 0.1.0-alpha.12 — 2026-09-12

### Highlights
- `Demo_14_DataTemplate`: Improve `DataTemplate` using a `Grid`
- Logic tidy-ups for `Demo_03_GridSplitter` and other uses of `GridSplitterX`

### Features
- WPFX: Revise `FrameworkElementFactoryX` type constraint as `DependencyObject`, to support `RowDefinition` and `ColumnDefinition` for `Grid`
- WPFX: Add `RelativeSourceX` helper
- WPFX: Logic simplifications using `Array.Empty` pattern

## 0.1.0-alpha.11 — 2026-09-05

### Highlights
- `DataTemplate` demo apps: `Demo_14_DataTemplate` & `Demo_15_DataTemplate2`
- Better organise and easier to read fragments of UI sections with their own `BuildXyz()` function, called from within the main UI tree
- Simplify the Triggers / EventAnimations demo apps 
- Improved local window resources with strong type returning functions rather than use a local `ResourceDictionary`

### Features
- WPFX: Add `DataTemplateX`


## 0.1.0-alpha.10 — 2026-08-26

### Highlights
- Simplify function name for additional WPFX helpers: `WpfxAdditions(...)`

### Breaking change
- Switch to **.NET 10**


## 0.1.0-alpha.9 — 2026-08-16

### Highlights
- Add project: `Demo_12_Triggers`

### Features
- WPFX: Add `StyleX` triggers, `EventTriggerX`, `BeginStoryBoardX`, `BeginAnimationX` etc 


## 0.1.0-alpha.8 — 2026-08-06

### Highlights
- Separate stages for clarity when building a Visual Tree, `ControlTemplate` and `Style` using WPFX

### Features
- WPFX: Add `Transform` types, space savings using expression-bodied members
- `Demo_07_Style` & `Demo_11_ControlTemplate` logic improvements and tidy-ups


## 0.1.0-alpha.7 — 2026-08-05

### Highlights
- **Breaking change:** Simplify `Grid` extension methods for adding rows and columns, removing the `DefinitionX` suffix
- Rationale: `Grid.AddRow()` and `Grid.AddColumn()` are _extension_ methods that do not clash with the existing WPF framework, therefore the `X` suffix is not required

### Breaking Changes
- Rename `AddRowDefinitionX()` -> `AddRow()`
- Rename `AddColumnDefinitionX()` -> `AddColumn()`

### Features
- `Demo_08_FluentTheme` logic improvements, and suppress warning moved to `csproj`


## 0.1.0-alpha.6 — 2026-08-02

### Highlights
- WPFX helper API cleaned and standardized (generic `FrameworkElementX` factory; many helpers added/normalized)
- Improve binding support for `BindingX`. Also includes `MultiBindingX`

### Features
- `PathStringX`: expression-to-path-string conversion, e.g. "ViewModel.SelectedCat.Type"
- MVVM demo apps revise to showcase the new binding features