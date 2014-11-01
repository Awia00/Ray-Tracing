using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ray_Tracing_Application.ViewModels;

namespace Ray_Tracing_Application.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RenderViewModel RenderViewModel = new RenderViewModel();
        public MainWindow()
        {
            InitializeComponent();
            UpdateRender();
            RenderGrid.DataContext = RenderViewModel;
        }

        public void UpdateRender()
        {
            RenderViewModel.UpdateImage();
            RenderGrid.DataContext = RenderViewModel;
        }
    }
}
