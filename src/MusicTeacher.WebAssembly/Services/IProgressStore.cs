using MusicTeacher.Shared.Progress;

namespace MusicTeacher.WebAssembly.Services;

public interface IProgressStore
{
    ValueTask<LearningProgress> LoadAsync(string lessonId);

    ValueTask SaveAsync(LearningProgress progress);

    ValueTask ResetAsync(string lessonId);
}
