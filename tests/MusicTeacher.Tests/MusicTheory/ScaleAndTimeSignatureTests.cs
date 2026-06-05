using MusicTeacher.Shared.MusicTheory;

namespace MusicTeacher.Tests.MusicTheory;

public sealed class ScaleAndTimeSignatureTests
{
    [Fact]
    public void MajorScaleUsesToneToneSemitonePattern()
    {
        var scale = new Scale(NoteLetter.C, ScaleMode.Major);

        Assert.Equal([2, 2, 1, 2, 2, 2, 1], scale.SemitonePattern);
    }

    [Fact]
    public void SimpleTimeSignaturesAcceptBeginnerMeters()
    {
        Assert.True(TimeSignature.FourFour.IsSimple);
        Assert.True(TimeSignature.ThreeFour.IsSimple);
        Assert.False(new TimeSignature(7, 16).IsSimple);
    }
}
