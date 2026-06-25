# Student Results Processing System

A C# console application that records student details and scores for five
courses, then calculates each student's **total**, **average**, **grade** and
**academic status** (Passed / Failed).

## Features

- Menu-driven interface (Enter Results / View Report / Exit)
- Collects full name, student ID, programme and level for any number of students (minimum 3)
- Collects scores for the five courses:
  - Operating Systems
  - Programming with Java
  - Information Theory
  - Computer Architecture
  - Digital Systems
- Score validation (must be between 0 and 100, re-prompts on invalid input)
- Grading system: A (80–100), B (70–79), C (60–69), D (50–59), F (below 50)
- Status: Passed (average ≥ 50) / Failed (average < 50)
- Bonus summary: best student, lowest-average student, class average

## How to Run

```bash
dotnet run
```

## Project Structure

| File | Description |
|------|-------------|
| `Program.cs` | All application logic (menu, input, validation, report) |
| `StudentResultsProcessingSystem.csproj` | Project file (.NET 10) |
