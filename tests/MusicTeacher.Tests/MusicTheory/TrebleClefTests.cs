using MusicTeacher.Shared.MusicTheory;

namespace MusicTeacher.Tests.MusicTheory;

public sealed class TrebleClefTests
{
    [Theory]
    [InlineData(NoteLetter.E, 4, 0, "mi")]
    [InlineData(NoteLetter.F, 4, 1, "fa")]
    [InlineData(NoteLetter.G, 4, 2, "sol")]
    [InlineData(NoteLetter.A, 4, 3, "la")]
    [InlineData(NoteLetter.B, 4, 4, "ti")]
    [InlineData(NoteLetter.C, 5, 5, "do")]
    [InlineData(NoteLetter.D, 5, 6, "re")]
    [InlineData(NoteLetter.E, 5, 7, "mi")]
    [InlineData(NoteLetter.F, 5, 8, "fa")]
    public void BeginnerNotesMapToTrebleStaffSteps(NoteLetter letter, int octave, int expectedStep, string expectedFixedDo)
    {
        var pitch = new Pitch(letter, octave);

        Assert.Equal(expectedStep, TrebleClef.GetStaffStep(pitch));
        Assert.Equal(expectedFixedDo, pitch.FixedDoName);
    }

    [Fact]
    public void StaffStepMappingRoundTrips()
    {
        foreach (var pitch in TrebleClef.BeginnerStaffNotes)
        {
            var staffStep = TrebleClef.GetStaffStep(pitch);

            Assert.Equal(pitch, TrebleClef.GetPitchFromStaffStep(staffStep));
        }
    }
}
