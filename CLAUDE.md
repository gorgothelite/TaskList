# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build

This is a .NET Framework 4.8 WinForms project. Build with MSBuild or Visual Studio:

```
msbuild TaskList\TaskList.csproj /p:Configuration=Debug
```

Output lands in `TaskList\bin\Debug\Test.exe`. There are no tests and no lint step.

## Project identity quirk

The solution/assembly is named **Test** (`<AssemblyName>Test</AssemblyName>`, `<RootNamespace>Test</RootNamespace>`) even though the folder and repo are called **TaskList**. All source files use `namespace Test`.

## Architecture

All logic lives in a single WinForms project under `TaskList/`. The key layers:

**Models (`Models.cs`)**
- `TaskItem` — the core entity. All state (priority, due date, alerts, snooze, hold) lives here.
- `RevisionEntry` / `FieldChange` — immutable audit log records.
- Enums: `TaskPriority`, `RevisionAction`, `SnoozeChoice`.

**Main window (`MainForm.cs` + `MainForm.Designer.cs`)**
- Owns `_tasks` and `_history` in memory; persists them to JSON on every mutation via `SaveAll()`.
- `RefreshList()` is the single re-render entry point — always call it after mutating `_tasks`.
- `ShowDetails()` renders the right-hand detail panel for the selected task.
- Alert polling runs on a 60-second `Timer`; `CheckAlerts()` fires one dialog at a time (`_alertActive` guard).
- Notes are auto-saved with an 800 ms debounce timer (`_notesSaveTimer`).
- Minimise-to-tray is handled in `OnResize`; close button hides rather than exits (exit via tray menu).

**Persistence**
- `tasks.json` and `tasks_history.json` are written next to the `.exe` (`bin\Debug\` in development).
- Every save also writes a timestamped backup to `bin\Debug\backups\`; the 20 most recent are kept (`MaxBackups`).
- `HistoryDialog` can clear `_history` in memory and persist it.

**Jira integration (`JiraForm.cs`)**
- Separate modeless window opened from the toolbar.
- Two-phase workflow: search → master list → import. Dedup on `TaskItem.Name`.
- Connection settings (URL, email, API token) are saved to a JSON file.
- JQL presets are managed in `JiraPresetsDialog` and stored in `jira_presets.json`.
- Raises `TasksImported` event; `MainForm.OnJiraTasksImported` handles the actual insertion.

**Export (`ExportDialog.cs`, `XlsxWriter.cs`, `JiraExportDialog.cs`)**
- `ExportDialog` — CSV/Excel export of the local task list.
- `JiraExportDialog` — Excel export from within the Jira window (master list).
- `XlsxWriter` writes `.xlsx` directly using `System.IO.Compression` (no third-party Excel library).

**UI conventions**
- All forms use the same dark colour palette: background `#1C1C1E` / `#242428`, toolbar `#2C2C2E`, selection `#00549E`.
- The main ListView is fully owner-drawn (`OwnerDraw = true`); drawing happens in `DrawHeader` and `DrawCell`.
- UI controls added in Designer files must follow the existing `InitializeComponent` style (no VS designer XML — everything is hand-coded C#).
- New buttons in the detail panel must be positioned manually; the panel is 320 px wide with 18 px side margins (275 px usable).
