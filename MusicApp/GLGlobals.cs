using MusicApp.Core.Rendering;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;
using System.IO;
using System.Reflection;

namespace MusicApp
{
    public static class GLGlobals
    {
        private static GLWpfControlSettings _mainSettings;
        private static Shader _currentShader;
        private static Renderer _currentRenderer;
        public static Light light0;
        private static Camera _camera;
        public static void Initialize(GLWpfControl control)
        {
            _mainSettings = new GLWpfControlSettings { MajorVersion = 2, MinorVersion = 1 };
            control.Start(_mainSettings);
         
            _camera = new Camera((int)control.Width,(int)control.Height);
            _currentShader = new Shader("vertex.vert","frag.frag");

            _currentRenderer = new Renderer(new Vector2(0, 0), new Vector2(2, 2));

            _currentRenderer.BindBackgroundTexture("./Images/background.png");

           
        }

        public static Camera GetCamera()
        {
            return _camera;
        }

        public static Shader GetCurrentShader()
        {
            return _currentShader;
        }

        public static Renderer GetCurrentRenderer()
        {
            return _currentRenderer;
        }


    }
}
