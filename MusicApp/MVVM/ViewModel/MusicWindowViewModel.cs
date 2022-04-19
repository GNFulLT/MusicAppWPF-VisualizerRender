using MusicApp.Core;
using MusicApp.Core.Rendering;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;
using System.Windows;
using System.Windows.Input;

namespace MusicApp.MVVM.ViewModel
{
    public class MusicWindowViewModel : ObservableObject
    {
        public ICommand LoadedCommand;
        public ICommand RenderCommand;
        private Shader shader;
        private Renderer renderer;
        public MusicWindowViewModel()
        {
            LoadedCommand = new RelayCommand(OnLoaded);
            RenderCommand = new RelayCommand(OnRendering);
        }
        public void OnLoaded(object sender)
        {
            GLGlobals.Initialize(sender as GLWpfControl);
            shader = GLGlobals.GetCurrentShader();
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
    }
}
