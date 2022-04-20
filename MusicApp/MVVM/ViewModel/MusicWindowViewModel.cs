using MusicApp.Core;
using MusicApp.Core.Rendering;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MusicApp.MVVM.ViewModel
{
    public class MusicWindowViewModel : ObservableObject
    {
        public ICommand LoadedCommand;
        public ICommand RenderCommand;
        public ICommand InteractBottomPanelCommand;
        private Renderer renderer;

        private bool isEntered = false;

        public MusicWindowViewModel()
        {
            LoadedCommand = new RelayCommand(OnLoaded);
            RenderCommand = new RelayCommand(OnRendering);
            InteractBottomPanelCommand = new RelayCommand(OnInteractBottomPanel);
        }
        public void OnLoaded(object sender)
        {
            GLGlobals.Initialize(sender as GLWpfControl);
            renderer = GLGlobals.GetCurrentRenderer();
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);


        }
        public void OnRendering(object sender)
        {
            GL.ClearColor(0f, 0f, 0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            renderer.Render();

        }
        private void OnInteractBottomPanel(object sender)
        {
            var panel = sender as WrapPanel;
            //If the opacity is 1 so its shown and user wants to leave
            if(isEntered)
            {
                DoubleAnimation animation = new DoubleAnimation(0,TimeSpan.FromMilliseconds(500));
                panel.BeginAnimation(WrapPanel.OpacityProperty, animation);
                var anime = new DoubleAnimation()
                {
                    To = -10,
                    Duration = TimeSpan.FromMilliseconds(300),


                };
                panel.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anime);

                isEntered = false;
            }
            else
            {
                DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromMilliseconds(500));
                panel.BeginAnimation(WrapPanel.OpacityProperty, animation);
                var anime = new DoubleAnimation()
                 {
                     To = -50,
                     Duration = TimeSpan.FromMilliseconds(300),
                     
                    
                 };
                 panel.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anime);
                
                isEntered = true;
            }

        }

       
    }
}
