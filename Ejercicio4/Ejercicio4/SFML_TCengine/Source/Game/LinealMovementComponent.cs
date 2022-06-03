using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using TCEngine;

namespace TCGame
{
    public class LinealMovementComponent : BaseComponent
    {
        private float DEFAULT_SPEED = 100f;

        private Vector2f m_Forward;
        private Vector2f m_MousePosition = new Vector2f(0f, 0f);
        private float m_Speed;

        public Vector2f Forward
        {
            get => m_Forward;
        }
        public LinealMovementComponent()
        {
            m_Speed = DEFAULT_SPEED;
            m_Forward = new Vector2f(1f, 0f);
        }

        public LinealMovementComponent(float _speed)
        {
            m_Speed = _speed;
            m_Forward = new Vector2f(1f, 0f);      
        }

        public LinealMovementComponent(Vector2f _forward, float _speed)
        {
            m_Forward = _forward;
            m_Speed = _speed;

            if(m_Forward.X > 1f || m_Forward.Y > 1f)
            {
                m_Forward = new Vector2f(1f, 1f);
            }
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

            transformComponent.Transform.Position += m_Forward * m_Speed * _dt;
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
