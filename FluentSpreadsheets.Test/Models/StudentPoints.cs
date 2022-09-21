namespace FluentSpreadsheets.Test.Models;


public readonly record struct StudentPoints(Student Student, IReadOnlyCollection<LabPoints> LabPoints);