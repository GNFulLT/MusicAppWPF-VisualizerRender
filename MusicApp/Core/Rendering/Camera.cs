using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Core.Rendering
{
    public class Camera
    {
        private Vector3 camPos;
        private Vector3 camUp;
        private Vector3 camFront;
        private float _fov = MathHelper.PiOver2;
        public float _aspectRatio;

        public Camera(int sizeX,int sizeY)
        {
            camPos = new Vector3(0.0f, 0.0f, 0.0f);
            camUp = new Vector3(0.0f, 1.0f, 0.0f);
            camFront = new Vector3(0.0f, 0.0f, -1.0f);
            _aspectRatio = sizeX / sizeY;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(camPos, camPos + camFront, camUp);
        }

        public Vector3 GetPosition()
        {
            return camPos;
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, _aspectRatio, 0.01f, 100f);
        }

    }
}
