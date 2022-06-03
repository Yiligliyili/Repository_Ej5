using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Diagnostics;
using TCEngine;

namespace TCGame
{
    public class FollowMouseComponent : BaseComponent
    {
        private const float DEFAULT_SPEED = 400.0f;
        private const float DEFAULT_ANGULAR_SPEED = 360.0f;

        private Vector2f m_MousePosition;
        private Vector2f m_Forward;
        private Vector2f m_DesiredForward;

        private float m_AngularSpeed;
        private float m_Speed;  

        public Vector2f Forward
        {
            get => m_Forward;           
        }

        public FollowMouseComponent()
        {
            m_Speed = DEFAULT_SPEED;
            m_AngularSpeed = DEFAULT_ANGULAR_SPEED;
        }

        public FollowMouseComponent(float _angleSpeed, float _speed)
        {
            m_Speed = _speed;
            m_AngularSpeed = _angleSpeed;
            
        }

        public override void OnActorCreated()
        {
            base.OnActorCreated();
            
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            TecnoCampusEngine.Get.Window.MouseMoved += MouseMovedHandler;

        }

        public override void Update(float _dt)
        {
            base.Update(_dt);

            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();

            Vector2f mouseOffset = m_MousePosition - transformComponent.Transform.Position;
            m_DesiredForward = mouseOffset.Normal();

            float desiredFwrdAngle = MathUtil.Angle(m_DesiredForward, m_Forward);
            float rotationAngle = m_AngularSpeed * _dt;
            if (rotationAngle > desiredFwrdAngle)
            {
                rotationAngle = desiredFwrdAngle;
            }

            float sign = MathUtil.Sign(m_DesiredForward, m_Forward);
            rotationAngle = rotationAngle * sign;

            m_Forward = m_Forward.Rotate(rotationAngle);

            transformComponent.Transform.Rotation = MathUtil.AngleWithSign(m_Forward, new Vector2f(0f, -1f));

            Vector2f nextPos = transformComponent.Transform.Position + m_Forward * m_Speed * _dt;
            transformComponent.Transform.Position = nextPos;

        }

        private void MouseMovedHandler(object _sender, MouseMoveEventArgs _mouseEventArgs)
        {
            m_MousePosition.X = _mouseEventArgs.X;
            m_MousePosition.Y = _mouseEventArgs.Y;
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public override object Clone()
        {
            TargetComponent targetCloned = new TargetComponent();
            return targetCloned;
        }
    }
}
