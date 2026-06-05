namespace MusicTeacher.Tests.EndToEnd;

public sealed class E2EFactAttribute : FactAttribute
{
    public E2EFactAttribute()
    {
        if (!string.Equals(Environment.GetEnvironmentVariable("MUSIC_TEACHER_RUN_E2E"), "true", StringComparison.OrdinalIgnoreCase))
        {
            Skip = "Set MUSIC_TEACHER_RUN_E2E=true to run browser E2E tests.";
        }
    }
}
