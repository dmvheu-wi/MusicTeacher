using Microsoft.JSInterop;
using MusicTeacher.Shared.MusicTheory;

namespace MusicTeacher.WebAssembly.Services;

public sealed class MusicAudioService(IJSRuntime jsRuntime)
{
    public ValueTask PlayNoteAsync(Pitch pitch)
        => jsRuntime.InvokeVoidAsync("musicTeacherAudio.playNote", pitch.FrequencyHz);

    public ValueTask PlayBuzzerAsync()
        => jsRuntime.InvokeVoidAsync("musicTeacherAudio.playBuzzer");
}
