using MusicApp.MVVM.ViewModel;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for MusicWindow.xaml
    /// </summary>
    public partial class MusicWindow : Window
    {
        public ICommand LoadedCommand;
        public ICommand RenderCommand;
        public ICommand InteractBottomPanelCommand;
        public MusicWindow()
        {
            InitializeComponent();
            var viewModel = this.DataContext  as MusicWindowViewModel;
            LoadedCommand = viewModel.LoadedCommand;
            RenderCommand = viewModel.RenderCommand;
            InteractBottomPanelCommand = viewModel.InteractBottomPanelCommand;
            LoadedCommand?.Execute(glControl);
            
        }
        private void OnRendering(TimeSpan delta)
        {
            RenderCommand?.Execute(delta);
        }

        private void OnInteractBottomPanel(object sender,MouseEventArgs e)
        {
            InteractBottomPanelCommand?.Execute(sender);
        }

      
    }
}
