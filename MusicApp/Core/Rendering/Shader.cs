using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Core.Rendering
{   
    public class Shader
    {
        private readonly int _program;

        private readonly Dictionary<string, int> _uniformLocations;

        public Shader(string vertPath,string fragPath)
        {
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var vertMainPath = $"MusicApp.Core.Rendering.Shaders.{vertPath}";

            var fragMainPath = $"MusicApp.Core.Rendering.Shaders.{fragPath}";

            if (!names.Contains(vertMainPath))
            {
                throw new Exception("Cant find shaders");
            }
            if (!names.Contains(fragMainPath))
            {
                throw new Exception("Cant find shaders");
            }

            
            string vertex;
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(vertMainPath))
            using (StreamReader sr = new StreamReader(s))
            {
                vertex = sr.ReadToEnd();
            }
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);

            GL.ShaderSource(vertexShader, vertex);

            GL.CompileShader(vertexShader);

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int isCompiled);
            if (isCompiled == 0)
            {
                throw new Exception($"There is an error while trying to compile vertex shader : {GL.GetShaderInfoLog(vertexShader)}");
            }

            string frag;
            using(Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(fragMainPath))
            using (StreamReader sr = new StreamReader(s))
            {
                frag = sr.ReadToEnd();
            }

            var fragShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(fragShader, frag);


            GL.CompileShader(fragShader);

            GL.GetShader(fragShader, ShaderParameter.CompileStatus, out isCompiled);

            if (isCompiled == 0)
            {
                throw new Exception
                    ($"There is an error while trying to compile fragment shader : {GL.GetShaderInfoLog(fragShader)}");
            }

            _program = GL.CreateProgram();

            GL.AttachShader(_program, vertexShader);
            GL.AttachShader(_program, fragShader);

            GL.LinkProgram(_program);

            GL.DetachShader(_program, vertexShader);
            GL.DetachShader(_program, fragShader);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragShader);

            GL.GetProgram(_program, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(_program, i,out _,out _);

                var location = GL.GetUniformLocation(_program, key);

                _uniformLocations.Add(key, location);
            }

        }

        public void Use()
        {
            GL.UseProgram(_program);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(_program, attribName);
        }

        public void SetInt(string name, int data)
        {
            GL.UseProgram(_program);
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetFloat(string name, float data)
        {
            GL.UseProgram(_program);
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(_program);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(_program);
            GL.Uniform3(_uniformLocations[name], data);
        }

        public void SetVector4(string name, Vector4 data)
        {
            GL.UseProgram(_program);
            GL.Uniform4(_uniformLocations[name], data);
        }

        public void EnableTexturing()
        {
            SetInt("isTextureLoaded", 1);
        }

        public void DisableTexturing()
        {
            SetInt("isTextureLoaded", 0);
        }

        public void EnableLighting()
        {
            SetInt("isLightLoaded", 1);
        }
        public void DisableLighting()
        {
            SetInt("isLightLoaded", 0);
        }

        public void UseLighting(Light light,Light.LightType type)
        {
            string name = Light.GetLightName(type);


            SetVector3(name+".lightColor", light.GetColor());
            SetFloat(name+".ambientStrength", light.GetAmbientStrength());
            SetFloat(name+".specularStrength", light.GetSpecularStrength());
            SetFloat(name+".shiness", light.shiness);
            SetFloat(name+".atten0", light.atten0);
            SetFloat(name+".atten1", light.atten1);
            SetFloat(name+".atten2", light.atten2);
            SetVector3(name+".lightPos", light.GetPosition());
            
        }

        public void UseMatrixes()
        {
            SetVector3("viewPos", new Vector3(GLGlobals.GetCamera().GetPosition()));
            SetMatrix4("model", Matrix4.Identity);
            SetMatrix4("view", GLGlobals.GetCamera().GetViewMatrix());
            SetMatrix4("projection", GLGlobals.GetCamera().GetProjectionMatrix());
        }

        ~Shader()
        {
            GL.DeleteProgram(_program);
        }
    }
}
