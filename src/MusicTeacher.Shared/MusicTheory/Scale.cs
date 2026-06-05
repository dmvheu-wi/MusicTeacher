namespace MusicTeacher.Shared.MusicTheory;

public sealed record Scale(NoteLetter Root, ScaleMode Mode)
{
    public IReadOnlyList<int> SemitonePattern => Mode switch
    {
        ScaleMode.Major => [2, 2, 1, 2, 2, 2, 1],
        ScaleMode.NaturalMinor => [2, 1, 2, 2, 1, 2, 2],
        _ => throw new ArgumentOutOfRangeException(nameof(Mode), Mode, null)
    };
}
