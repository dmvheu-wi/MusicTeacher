using MusicTeacher.Shared.Lessons;
using MusicTeacher.Shared.MusicTheory;
using MusicTeacher.Shared.Progress;
using System.Net.Http.Json;

namespace MusicTeacher.WebAssembly.Pages;

public partial class Home
{
    private readonly IReadOnlyList<int> selectableSteps = TrebleClef.BeginnerPlacementNotes
        .Select(TrebleClef.GetStaffStep)
        .ToArray();

    private LessonDefinition? lesson;
    private LearningProgress progress = LearningProgress.Empty("treble-clef-start");
    private DrillMode mode = DrillMode.NameNote;
    private Pitch currentPitch = TrebleClef.BeginnerReadingNotes[0];
    private int? selectedStep;
    private string feedbackKey = "Ready";
    private string feedbackClass = "feedback";
    private Pitch? previousPitch;

    private string FeedbackText => Localizer[feedbackKey];

    private bool IsPlacementMode => mode == DrillMode.PlaceNote;

    private Pitch? DisplayedPitch => mode == DrillMode.NameNote
        ? currentPitch
        : selectedStep is null ? null : TrebleClef.GetPitchFromStaffStep(selectedStep.Value);

    private string CurrentDrillTitle => mode == DrillMode.NameNote
        ? Localizer["NameTheNoteTitle"]
        : Localizer["PlaceTheNoteTitle"];

    private string PromptText => mode == DrillMode.NameNote
        ? Localizer["NamePrompt"]
        : Localizer.Format("PlacePrompt", GetPromptName(currentPitch));

    protected override async Task OnInitializedAsync()
    {
        lesson = await Http.GetFromJsonAsync<LessonDefinition>("content/lessons/treble-clef-start.json");
        progress = await ProgressStore.LoadAsync("treble-clef-start");
        NextRound();
    }

    private void SetMode(DrillMode nextMode)
    {
        mode = nextMode;
        previousPitch = null;
        NextRound();
    }

    private async Task ChoosePitch(Pitch pitch)
    {
        var isCorrect = pitch == currentPitch;
        await PlayClickCue(isCorrect, currentPitch);
        await RecordAnswer(isCorrect);
    }

    private async Task PreviewPitch(Pitch pitch)
    {
        await Audio.PlayNoteAsync(pitch);
    }

    private async Task PreviewStaffStep(int step)
    {
        await Audio.PlayNoteAsync(TrebleClef.GetPitchFromStaffStep(step));
    }

    private async Task SelectStaffStep(int step)
    {
        selectedStep = step;
        var isCorrect = step == TrebleClef.GetStaffStep(currentPitch);
        await PlayClickCue(isCorrect, currentPitch);
        await RecordAnswer(isCorrect);
    }

    private async Task RecordAnswer(bool isCorrect)
    {
        progress = progress with
        {
            Attempts = progress.Attempts + 1,
            CorrectAnswers = progress.CorrectAnswers + (isCorrect ? 1 : 0),
            Streak = isCorrect ? progress.Streak + 1 : 0
        };

        feedbackKey = isCorrect ? "CorrectFeedback" : "MissedFeedback";
        feedbackClass = isCorrect ? "feedback is-correct" : "feedback is-missed";

        await ProgressStore.SaveAsync(progress);

        if (isCorrect)
        {
            NextRound();
        }
    }

    private void NextRound()
    {
        var notes = CurrentNotes;
        currentPitch = PickRandomPitch(notes);
        previousPitch = currentPitch;
        selectedStep = null;
        feedbackKey = mode == DrillMode.NameNote ? "PickNameFeedback" : "PickStaffFeedback";
        feedbackClass = "feedback";
    }

    private Pitch PickRandomPitch(IReadOnlyList<Pitch> notes)
    {
        if (notes.Count == 0)
        {
            throw new InvalidOperationException("A drill needs at least one note.");
        }

        if (notes.Count == 1 || previousPitch is null)
        {
            return notes[Random.Shared.Next(notes.Count)];
        }

        var candidates = notes.Where(note => note != previousPitch.Value).ToArray();
        return candidates[Random.Shared.Next(candidates.Length)];
    }

    private IReadOnlyList<Pitch> CurrentNotes => mode == DrillMode.NameNote
        ? TrebleClef.BeginnerReadingNotes
        : TrebleClef.BeginnerPlacementNotes;

    private string GetModeClass(DrillMode drillMode)
        => mode == drillMode ? "mode-button is-active" : "mode-button";

    private string GetCultureClass(string cultureName)
        => string.Equals(Localizer.CurrentCulture.Name, cultureName, StringComparison.OrdinalIgnoreCase)
            ? "language-button is-active"
            : "language-button";

    private async Task ChangeCulture(string cultureName)
    {
        await Localizer.SetCultureAsync(cultureName);
    }

    private async Task PlayClickCue(bool isCorrect, Pitch pitch)
    {
        if (isCorrect)
        {
            await Audio.PlayNoteAsync(pitch);
            return;
        }

        await Audio.PlayBuzzerAsync();
    }

    private string GetPromptName(Pitch pitch)
        => $"{(pitch.Octave == 4 ? Localizer["LowOctave"] : Localizer["HighOctave"])} {pitch.FixedDoName}";

    private enum DrillMode
    {
        NameNote,
        PlaceNote
    }
}
