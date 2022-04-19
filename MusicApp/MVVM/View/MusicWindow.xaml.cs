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
        public MusicWindow()
        {
            InitializeComponent();
            var viewModel = this.DataContext  as MusicWindowViewModel;
            LoadedCommand = viewModel.LoadedCommand;
            RenderCommand = viewModel.RenderCommand;
            LoadedCommand?.Execute(glControl);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
   
            

        }

        private void OnRendering(TimeSpan delta)
        {
            RenderCommand?.Execute(delta);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeX(-0.1f);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeX(0.1f);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeY(-0.1f);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeY(0.1f);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeZ(-0.1f);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeZ(0.1f);

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeShi(-1.0f);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeShi(1.0f);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.atten0 += 1.0f;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.atten0 -= 1.0f;
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.atten1 += 0.2f;
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.atten1 -= 0.2f;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.atten2 += 1.0f;
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.atten2 -= 1.0f;
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeAmbientStrength(0.1f);
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeSpecularStrength(0.1f);

        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeAmbientStrength(-0.1f);

        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            GLGlobals.light0.ChangeSpecularStrength(-0.1f);
        }
    }
}
