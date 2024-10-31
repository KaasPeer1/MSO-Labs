using System.Windows.Controls;
using System.Windows.Input;

namespace EduCode.CustomControls;

// Custom TextBox with variable tab size
public class CustomTextBox : TextBox
{
    public int TabSize { get; set; } = 4;

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Tab)
        {
            e.Handled = true;

            var caretIndex = CaretIndex;
            Text = Text.Insert(caretIndex, new string(' ', TabSize));
            CaretIndex = caretIndex + TabSize;
        }

        base.OnPreviewKeyDown(e);
    }
}