using Avalonia.Controls;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;

namespace Practice.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var timer = new System.Timers.Timer(16);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.UIThread.Post(() =>
            {
                //var skiaCanvas = this.FindControl<Control>("simpleCanvas");
                //skiaCanvas?.InvalidateVisual();

                var glCanvas = this.FindControl<OpenGlControlBase>("shadowCanvas");
                glCanvas?.RequestNextFrameRendering();
            }, DispatcherPriority.Background);
        }
    }
}