using MusicTeacher.Shared.MusicTheory;

namespace MusicTeacher.Tests.MusicTheory;

public sealed class PitchTests
{
    [Theory]
    [InlineData(NoteLetter.A, 4, 440.0)]
    [InlineData(NoteLetter.C, 4, 261.63)]
    [InlineData(NoteLetter.C, 5, 523.25)]
    public void FrequencyUsesEqualTemperament(NoteLetter letter, int octave, double expectedFrequency)
    {
        var pitch = new Pitch(letter, octave);

        Assert.Equal(expectedFrequency, pitch.FrequencyHz, precision: 2);
    }
}
