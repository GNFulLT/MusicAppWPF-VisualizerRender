using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Core.Rendering
{
    public class Light
    {
        public enum LightType
        {
            light0,light1,light2,light3,light4
        }

        private Vector3 _pos;
        private Vector3 _color;
        private float _ambientStrength;
        private float _specularStrength;
        public float shiness = 2;
        public float atten0 = 1;
        public float atten1 = 1;
        public float atten2 = 1;

        public Light(Vector3 pos, Vector3 color, float ambientStrength = 1.0f, float specularStrength = 0.5f)
        {
            _pos = pos;
            _color = color;
            _ambientStrength = ambientStrength;
            _specularStrength = specularStrength;
        }

        public void ChangeShi(float x)
        {
            shiness += x;
        }

        public void ChangeAmbientStrength(float x)
        {
            _ambientStrength += x;
        }

        public void ChangeSpecularStrength(float x)
        {
            _specularStrength += x;
        }

        public void ChangeY(float x)
        {
            _pos.Y += x;
        }

        public void ChangeZ(float x)
        {
            _pos.Z += x;
        }


        public void ChangeX(float x)
        {
            _pos.X += x;
        }
        
        public float GetAmbientStrength()
        {
            return _ambientStrength;
        }

        public float GetSpecularStrength()
        {
            return _specularStrength;
        }
        public Vector3 GetColor()
        {
            return _color;
        }

        public Vector3 GetPosition()
        {
            return _pos;
        }

        public static string GetLightName(LightType type)
        {
            switch (type)
            {
                case LightType.light0:
                {
                    return nameof(LightType.light0);
                }
                case LightType.light1:
                    {
                        return nameof(LightType.light1);
                    }
                case LightType.light2:
                    {
                        return nameof(LightType.light2);
                    }
                case LightType.light3:
                    {
                        return nameof(LightType.light3);
                    }
                case LightType.light4:
                    {
                        return nameof(LightType.light4);
                    }
                default:
                    throw new Exception("There is no light type like this");
            }
        }
        
    }
}
