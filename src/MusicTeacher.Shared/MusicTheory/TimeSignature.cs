namespace MusicTeacher.Shared.MusicTheory;

public sealed record TimeSignature(int BeatsPerMeasure, int BeatUnit)
{
    public static readonly TimeSignature FourFour = new(4, 4);
    public static readonly TimeSignature ThreeFour = new(3, 4);
    public static readonly TimeSignature TwoFour = new(2, 4);

    public string DisplayName => $"{BeatsPerMeasure}/{BeatUnit}";

    public bool IsSimple
        => BeatsPerMeasure > 0
           && BeatUnit is 2 or 4 or 8
           && BeatsPerMeasure is >= 2 and <= 4;
}
