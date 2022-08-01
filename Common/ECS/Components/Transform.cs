using System;
using Common.Settings;
using Microsoft.Xna.Framework;

namespace Common.ECS.Components
{
    public class Transform
    {
        public Vector3 Position {
                                    get => position;
                                    set => SetPosition(value);
                                }

        public Quaternion Rotation {
                                    get => rotation;
                                    set => SetRotation(value);
                                }
        public float OneRotation {
                                    get => oneRotation;
                                    set => SetRotation(value);
                                }
                                
        public Vector3 Scale    {
                                    get => scale;
                                    set => SetScale(value);
                                }
        public float OneScale   {
                                    get => scale.X;
                                    set => SetScale(new Vector3(value, value, value));
                                }

        public Vector3 Forward  {
                                    get => WorldMatrix.Forward;
                                }
        public Vector3 DeltaPosition { get; private set; }
        public Matrix WorldMatrix { get; private set; }
        public float RotationSpeed;
        private Vector3 position, scale;
        private Quaternion rotation;
        private float oneRotation;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#region Constructors

        public Transform(Vector3 position)
        {
            this.position = position;
            rotation = Quaternion.Identity;
            scale = Vector3.One;
            RotationSpeed = 1;
            UpdateWorldMatrix();
        }

        public Transform(Vector3 position, float rotationSpeed)
        {
            this.position = position;
            rotation = Quaternion.Identity;
            scale = Vector3.One;
            RotationSpeed = rotationSpeed;
            UpdateWorldMatrix();
        }

        public Transform(Vector3 position, Vector3 forward)
        {
            WorldMatrix = Matrix.Identity;
            this.position = position;
            rotation = Quaternion.Identity;
            scale = Vector3.One;
            RotationSpeed = 1;
            LookAt(forward + position);
        }

        public Transform(Vector3 position, Vector3 forward, Vector3 scale)
        {
            WorldMatrix = Matrix.Identity;
            this.position = position;
            rotation = Quaternion.Identity;
            this.scale = scale;
            RotationSpeed = 1;
            LookAt(forward + position);
        }

        public Transform(Vector3 position, Vector3 forward, float scale)
        {
            WorldMatrix = Matrix.Identity;
            this.position = position;
            rotation = Quaternion.Identity;
            this.scale = new Vector3(scale, scale, scale);
            RotationSpeed = 1;
            LookAt(forward + position);
        }

        public Transform(Vector3 position, float rotationSpeed, float scale)
        {
            this.position = position;
            rotation = Quaternion.Identity;
            this.scale = Vector3.One;
            RotationSpeed = rotationSpeed;
            UpdateWorldMatrix();
        }

#endregion
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void UpdateWorldMatrix()
        { //ISRT
            var identityMatrix = Matrix.Identity;
            //So basically, a few things were made to invert Y axis so it could grow going from down to up:
            //1) switching RasterizerState in spriteBatch begin call to cull clockwise
            //2) inversing scale of sprite in spriteBatch draw call
            //3) changing 'Up' vector by inversing M22 element of transform world matrix
            identityMatrix.M22 = -1;
            WorldMatrix = identityMatrix;
            WorldMatrix *= Matrix.CreateScale(Scale);
            WorldMatrix *= Matrix.CreateFromQuaternion(Rotation);
            WorldMatrix *= Matrix.CreateTranslation(Position);
        }

        public void Translate(Vector3 translation, bool updateMatrix = true)
        {
            var oldPosition = position;
            position += translation;
            DeltaPosition = position - oldPosition;
            
            if(updateMatrix)
                UpdateWorldMatrix();
        }

        public void Translate(float x = 0, float y = 0, float z = 0)
        {
            Translate(new Vector3(x, y, z));
        }

        public void SetPosition(Vector3 newPosition, bool updateMatrix = true)
        {
            var oldPosition = position;
            position = newPosition;
            DeltaPosition = position - oldPosition;
            
            if(updateMatrix)
                UpdateWorldMatrix();
        }

        public void Rotate(Quaternion rotation, bool updateMatrix = true)
        {
            this.rotation *= rotation;
            
            if(updateMatrix)
                UpdateWorldMatrix();
        }

        public void Rotate(float rotation, bool updateMatrix = true)
        {
            oneRotation = rotation;
            oneRotation %= 360;
            
            this.rotation *= Quaternion.CreateFromAxisAngle(Vector3.Backward, oneRotation);
            
            if(updateMatrix)
                UpdateWorldMatrix();
        }

        public void SetRotation(Quaternion rotation, bool updateMatrix = true)
        {
            this.rotation = rotation;

            if(updateMatrix)
            {
                UpdateWorldMatrix();
            }
        }

        public void SetRotation(float rotation, bool updateMatrix = true)
        {
            oneRotation = rotation;
            oneRotation %= 360;
            
            this.rotation = Quaternion.CreateFromAxisAngle(Vector3.Backward, oneRotation);

            if(updateMatrix)
            {
                UpdateWorldMatrix();
            }
        }

        public void Rotate(Vector3 rotation)
        {
            oneRotation += rotation.Z;
            oneRotation %= 360;

            Rotate(Quaternion.CreateFromYawPitchRoll(rotation.X, rotation.Y, rotation.Z));
        }

        public void RotateSmooth(Vector3 rotation, float smoothness)
        {
            oneRotation += rotation.Z;
            oneRotation %= 360;

            var newRotation = Quaternion.CreateFromYawPitchRoll(rotation.X, rotation.Y, rotation.Z);
            var newSmoothedRotation = Quaternion.Lerp(this.rotation, newRotation, smoothness);
            Rotate(newSmoothedRotation);
        }

        public void Rotate(Vector3 axis, float angle, bool updateMatrix = true)
        {
            oneRotation += angle;
            oneRotation %= 360;
            
            Rotate(Quaternion.CreateFromAxisAngle(axis, angle), updateMatrix);
        }

        public void RotateAroundPoint(Vector3 point, Vector3 axis, float angle)
        {
            var direction = point - Position;
            var distance = direction.Length();
            
            SetPosition(Vector3.Zero);
            Rotate(axis, angle);
            SetPosition(point);
            Translate(WorldMatrix.Backward * distance);
        }

        public void RotateAroundPoint(Vector3 point, Quaternion rotation)
        {
            var direction = point - Position;
            var directionNormalized = Vector3.Normalize(direction);
            var distance = direction.Length();
            
            Translate(directionNormalized * distance);
            Rotate(rotation);
            Translate(WorldMatrix.Backward * distance);
        }

        public void SetScale(Vector3 newScale, bool updateMatrix = true)
        {
            scale = newScale;
            
            if(updateMatrix)
                UpdateWorldMatrix();
        }

        public void SetScale(float x, float y, float z)
        {
            SetScale(new Vector3(x, y, z));
        }

        public void LookAt(Vector3 targetPosition, bool updateMatrix = true)
        {
            var direction = Vector3.Normalize(targetPosition - Position);
            var up = Vector3.Up;

            var dot = Vector3.Dot(direction, up);
            if(dot > -1 && dot < 1){
                var rotMatrix = Matrix.CreateWorld(Position, direction, up);
                Quaternion rot;
                rotMatrix.Decompose(out _, out rot, out _);
                rotation = rot;
                
                if(updateMatrix)
                    UpdateWorldMatrix();
            }
        }

        public void LookAtSmooth(Vector3 targetPosition, float rotationSpeed, bool updateMatrix = true)
        {
            var direction = Vector3.Normalize(targetPosition - Position);
            var up = Vector3.Up;

            var dot = Vector3.Dot(direction, up);
            if(dot > -1 && dot < 1){
                var rotMatrix = Matrix.CreateWorld(Position, direction, up);
                Quaternion rot;
                rotMatrix.Decompose(out _, out rot, out _);
                rotation = Quaternion.Lerp(rotation, rot, rotationSpeed);
                
                if(updateMatrix)
                    UpdateWorldMatrix();
            }
        }
    }
}