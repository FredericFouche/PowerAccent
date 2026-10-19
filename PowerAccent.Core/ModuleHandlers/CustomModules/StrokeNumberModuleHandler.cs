using PowerAccent.Core.Services;
using System.Diagnostics;

namespace PowerAccent.Core.ModuleHandlers.CustomModules;

internal class StrokeNumberModuleHandler : ModuleHandler
{
    private const uint Numpad0 = 0x60;
    private const uint Numpad9 = 0x69;

    public StrokeNumberModuleHandler(PowerAccent powerAccent, SettingsService settingsService, KeyOptions options)
        : base(powerAccent, settingsService, options)
    {
    }

    public override bool InvokeKeyDown(uint key)
    {
        if (!ShouldHandle(key))
            return base.InvokeKeyDown(key);

        int index = GetSelectionIndex(key);
        if (index < Options.Characters.Length)
        {
            Options.SelectedIndex = index;
            PowerAccent.SelectCharacter(index);
            Debug.WriteLine($"InvokeKeyDown StrokeNumberModuleHandler - SelectedIndex: {index}");
        }

        // Do not type the shortcut digit in the active application.
        return false;
    }

    public override bool InvokeKeyUp(uint key)
    {
        if (ShouldHandle(key))
            return false;

        return base.InvokeKeyUp(key);
    }

    private bool ShouldHandle(uint key)
    {
        if (!Options.IsVisible ||
            (Options.LetterPressed.HasValue && key == (uint)Options.LetterPressed.Value))
            return false;

        return GetSelectionIndex(key) >= 0;
    }

    private static int GetSelectionIndex(uint key)
    {
        uint topRow0 = (uint)LetterKey._0;
        uint topRow9 = (uint)LetterKey._9;

        if (key >= (uint)LetterKey._1 && key <= topRow9)
            return (int)(key - (uint)LetterKey._1);
        if (key == topRow0)
            return 9;
        if (key >= Numpad0 && key <= Numpad9)
            return key == Numpad0 ? 9 : (int)(key - Numpad0 - 1);

        return -1;
    }
}
