# MusicTeacher

MusicTeacher is a Blazor WebAssembly PWA for children learning to read music from scratch.

The first version starts with treble-clef drills:

- name a note shown on the staff with fixed-do syllables
- place a named note on the correct staff line or space
- keep local progress in browser storage
- load lesson metadata from local JSON content

## Project Structure

- `src/MusicTeacher.WebAssembly` - standalone .NET 10 Blazor WebAssembly PWA
- `src/MusicTeacher.Shared` - pure C# music-theory and lesson/progress models
- `tests/MusicTeacher.Tests` - xUnit unit tests plus opt-in Playwright E2E tests

## Run

```powershell
dotnet run --project src\MusicTeacher.WebAssembly\MusicTeacher.WebAssembly.csproj
```

## Test

```powershell
dotnet test MusicTeacher.slnx --artifacts-path .artifacts
```

Playwright E2E tests are skipped by default. To run them, install Playwright browsers and enable the E2E flag:

```powershell
dotnet build MusicTeacher.slnx --artifacts-path .artifacts
pwsh .artifacts\bin\MusicTeacher.Tests\debug\playwright.ps1 install chromium
$env:MUSIC_TEACHER_RUN_E2E = "true"
dotnet test MusicTeacher.slnx --artifacts-path .artifacts
```
