using System.Windows;

namespace PowerAccent.UI;

public partial class Selector : Window
{
    public Selector(char[] selectedCharacters)
    {
        InitializeComponent();
        this.ShowActivated = false;
        this.Topmost = true;
        characters.ItemsSource = selectedCharacters.Select((character, index) => new AccentChoice(
            character,
            index < 9 ? (index + 1).ToString() : index == 9 ? "0" : string.Empty));
        characters.SelectedIndex = 0;
    }

    public void SetIndex(int index)
    {
        characters.SelectedIndex = index;
    }

    public void SetPosition(double left, double top)
    {
        this.Left = left;
        this.Top = top;
    }

    public void SetBorderWindowAlignment(bool? isLeft)
    {
        gridBorder.HorizontalAlignment = isLeft switch
        {
            true => HorizontalAlignment.Left,
            false => HorizontalAlignment.Right,
            _ => HorizontalAlignment.Center,
        };
    }

    private sealed record AccentChoice(char Character, string Shortcut);
}
