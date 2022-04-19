using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Core.Rendering
{
    public class Renderer
    {
        private Vector2 _topLeft, _size;
        private Vector2 _initialSize;
        private Vector2 _initialPos;
        private Background background;
        private List<IDrawable> drawables;
        
        

        public Renderer(Vector2 topLeft, Vector2 size)
        {
            _topLeft = topLeft;
            _size = size;
            _initialSize = size;
            _initialPos = topLeft;
            background = new Background(topLeft, size);
            drawables = new List<IDrawable>();
            drawables.Add(background);
        }

        public void Render()
        {
            foreach(var item in drawables)
            {
                item.Draw();
            }
        }

        public void BindBackgroundTexture(string path)
        {
            background.BindTexture(path);
        }

        private class Background : IDrawable
        {
            private Vector2 _topLeft, _size;
            private Vector2 _initialSize;
            private Vector2 _initialPos;
            private uint _textureID = 0;
            private Vector4 _color;


            private int VAO;
            private int VBO;
            private int EBO;
            private int TBO;

            public Background(Vector2 topLeft,Vector2 size)
            {
                _topLeft = topLeft;
                _size = size;
                _initialSize = size;
                _initialPos = topLeft;
                _color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

                VAO = GL.GenVertexArray();
                GL.BindVertexArray(VAO);

                VBO = GL.GenBuffer();
                var _vertexData = GetVertices();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, _vertexData.Length * sizeof(float), _vertexData, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.EnableVertexAttribArray(0);
                EBO = GL.GenBuffer();
                var _elementData = GetElements();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _elementData.Length * sizeof(uint),
                    _elementData, BufferUsageHint.StaticDraw);
                TBO = GL.GenBuffer();
                var _tboData = GetTextureCoord();
                GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
                GL.BufferData(BufferTarget.ArrayBuffer, _tboData.Length * sizeof(float), _tboData, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(1);


            }

            public void BindTexture(string path)
            {
                _textureID = TextureHandler.LoadTexture(path);
            }

            public  void Draw()
            {
                GLGlobals.GetCurrentShader().Use();
                GLGlobals.GetCurrentShader().EnableTexturing();
                GL.BindVertexArray(VAO);
                TextureHandler.UseTexture2D(TextureUnit.Texture0, _textureID);
                GLGlobals.GetCurrentShader().SetVector4("aColor",_color);
                GL.DrawElements(BeginMode.Triangles,
                    GetElements().Length, DrawElementsType.UnsignedInt, 0);
                GL.BindTexture(TextureTarget.Texture2D,0);
            }

            

            private float[] GetVertices()
            {
                return new float[]
                {
                    (_topLeft.X - 1.0f),(_topLeft.Y+1.0f),-1.0f, //Top Left
                    ((_topLeft.X-1.0f)+_size.X),(_topLeft.Y+1.0f),-1.0f,//Top Right
                    ((_topLeft.X-1.0f)+_size.X),((_topLeft.Y+1.0f)-_size.Y),-1.0f,//Bottom Right
                    (_topLeft.X - 1.0f),((_topLeft.Y+1.0f)-_size.Y),-1.0f
                }; 
            }

            private float[] GetTextureCoord()
            {
                return new float[]
                {
                    0.0f,0.0f,
                    1.0f,0.0f,
                    1.0f,1.0f,
                    0.0f,1.0f
                };
            }

            private uint[] GetElements()
            {
                return new uint[]
                {
                    0,1,3,//First Triangle
                    1,2,3
                };
            }
        }
    }
}
