using System.Text.Json;
using Microsoft.JSInterop;
using MusicTeacher.Shared.Progress;

namespace MusicTeacher.WebAssembly.Services;

public sealed class BrowserProgressStore(IJSRuntime jsRuntime) : IProgressStore
{
    private const string StorageKeyPrefix = "music-teacher-progress:";

    public async ValueTask<LearningProgress> LoadAsync(string lessonId)
    {
        var json = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKeyPrefix + lessonId);

        if (string.IsNullOrWhiteSpace(json))
        {
            return LearningProgress.Empty(lessonId);
        }

        return JsonSerializer.Deserialize<LearningProgress>(json) ?? LearningProgress.Empty(lessonId);
    }

    public async ValueTask SaveAsync(LearningProgress progress)
    {
        var json = JsonSerializer.Serialize(progress);
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKeyPrefix + progress.LessonId, json);
    }

    public async ValueTask ResetAsync(string lessonId)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", StorageKeyPrefix + lessonId);
    }
}
