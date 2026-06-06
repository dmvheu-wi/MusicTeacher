namespace MusicTeacher.Shared.MusicTheory;

public readonly record struct Pitch(NoteLetter Letter, int Octave)
{
    public string ScientificName => $"{Letter}{Octave}";

    public double FrequencyHz
    {
        get
        {
            var midiNote = (Octave + 1) * 12 + SemitoneFromC;
            return 440d * Math.Pow(2d, (midiNote - 69) / 12d);
        }
    }

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

    private int SemitoneFromC => Letter switch
    {
        NoteLetter.C => 0,
        NoteLetter.D => 2,
        NoteLetter.E => 4,
        NoteLetter.F => 5,
        NoteLetter.G => 7,
        NoteLetter.A => 9,
        NoteLetter.B => 11,
        _ => throw new ArgumentOutOfRangeException(nameof(Letter), Letter, null)
    };
}
