using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TeslaVideoCenter.Views
{
    public class EventInfoView : UserControl
    {
        public EventInfoView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}