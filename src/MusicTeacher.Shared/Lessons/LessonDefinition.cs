namespace MusicTeacher.Shared.Lessons;

public sealed record LessonDefinition(
    string Id,
    string Title,
    string Clef,
    IReadOnlyList<string> Skills,
    IReadOnlyList<DrillDefinition> Drills);

public sealed record DrillDefinition(
    string Id,
    string Title,
    string Kind,
    IReadOnlyList<string> Notes);
