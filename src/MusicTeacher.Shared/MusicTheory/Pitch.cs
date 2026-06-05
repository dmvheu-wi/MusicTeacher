namespace MusicTeacher.Shared.MusicTheory;

public readonly record struct Pitch(NoteLetter Letter, int Octave)
{
    public string ScientificName => $"{Letter}{Octave}";

    public string FixedDoName => Letter switch
    {
        NoteLetter.C => "do",
        NoteLetter.D => "re",
        NoteLetter.E => "mi",
        NoteLetter.F => "fa",
        NoteLetter.G => "sol",
        NoteLetter.A => "la",
        NoteLetter.B => "ti",
        _ => throw new ArgumentOutOfRangeException(nameof(Letter), Letter, null)
    };
}
