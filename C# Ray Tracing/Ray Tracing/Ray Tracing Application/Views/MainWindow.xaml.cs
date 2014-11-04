using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RayTracingModel;
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
            UpdateProgressBar();
            RenderGrid.DataContext = RenderViewModel;
        }

        async private void UpdateProgressBar()
        {
            RenderProgressBar.Value = 0;
            int OneTenthSecondsPast = 0;
            do
            {
                Func<Task<int>> temp = () => Controller.GetInstance().GetRenderProgress();
                RenderProgressBar.Value = await Task.Run(temp);
                OneTenthSecondsPast++;
                if (OneTenthSecondsPast%10 == 0)
                {
                    TimeTextBlock.Text = "Time elapsed: " + OneTenthSecondsPast/10 + " seconds.";
                }
            } while (RenderProgressBar.Value < 99);
            RenderProgressBar.Value = 0;
            TimeTextBlock.Text = "Total time elapsed: " + OneTenthSecondsPast / 10 + " seconds."; 
        }
    }
}
