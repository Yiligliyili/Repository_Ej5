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
        private const float DEFAULT_ANGULAR_SPEED =720.0f;
        private static Vector2f UpperVector = new Vector2f(0.0f, -1.0f);

        private Vector2f m_MousePosition;
        private Vector2f m_Forward;
        private Vector2f m_PositionWithoutModifiers;
        private Vector2f m_DesiredForward;

        private float m_AngularSpeed = DEFAULT_ANGULAR_SPEED;
        private float m_Speed;
        private float m_DesiredSpeed;

        public Vector2f Forward
        {
            get => m_Forward;
            set => m_Forward = value;
        }

        public FollowMouseComponent(Vector2f _forward)
        {
            m_Forward = _forward;
            m_DesiredSpeed = DEFAULT_SPEED;
            m_MousePosition = new Vector2f(0.0f, 0.0f);
            m_DesiredForward = new Vector2f();
        }

        public FollowMouseComponent(Vector2f _forward, float _speed)
        {
            m_Forward = _forward;
            m_DesiredSpeed = _speed;
            m_MousePosition = new Vector2f(0.0f, 0.0f);
            m_DesiredForward = new Vector2f();
        }

        public override void OnActorCreated()
        {
            base.OnActorCreated();

            TecnoCampusEngine.Get.Window.MouseMoved += MouseMovedHandler;
            
            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            Debug.Assert(transformComponent != null);

            m_PositionWithoutModifiers = transformComponent.Transform.Position;
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);

            TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
            Debug.Assert(transformComponent != null);

            Vector2f mouseOffset = m_MousePosition - m_PositionWithoutModifiers;
            m_Speed = Math.Min(mouseOffset.Size(), m_DesiredSpeed);
            m_DesiredForward = mouseOffset.Normal();

            float desiredForwardAngle = MathUtil.Angle(m_DesiredForward, m_Forward);
            float rotationAngle = m_AngularSpeed * _dt;
            if (rotationAngle > desiredForwardAngle)
            {
                rotationAngle = desiredForwardAngle;
            }

            float sign = MathUtil.Sign(m_DesiredForward, m_Forward);
            rotationAngle = rotationAngle * sign;

            m_Forward = m_Forward.Rotate(rotationAngle);

            Vector2f velocity = m_Forward * m_Speed;
            m_PositionWithoutModifiers += velocity * _dt;

            transformComponent.Transform.Position = m_PositionWithoutModifiers;
            transformComponent.Transform.Rotation = MathUtil.AngleWithSign(m_Forward, UpperVector);
        }

        private void MouseMovedHandler(object _sender, MouseMoveEventArgs _mouseEventArgs)
        {
            m_MousePosition = new Vector2f(_mouseEventArgs.X, _mouseEventArgs.Y);
        }

        public override void DebugDraw()
        {
            base.DebugDraw();
            TecnoCampusEngine.Get.DebugManager.Label(new Vector2f(50, 50), m_MousePosition.ToString(), Color.Green);
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public override object Clone()
        {
            FollowMouseComponent clonedComponent = new FollowMouseComponent(m_Forward, m_DesiredSpeed);
            return clonedComponent;
        }
    }
}
