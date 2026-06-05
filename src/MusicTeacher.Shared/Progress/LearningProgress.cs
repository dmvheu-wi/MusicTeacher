namespace MusicTeacher.Shared.Progress;

public sealed record LearningProgress(
    string LessonId,
    int Attempts,
    int CorrectAnswers,
    int Streak)
{
    public static LearningProgress Empty(string lessonId) => new(lessonId, 0, 0, 0);
}
