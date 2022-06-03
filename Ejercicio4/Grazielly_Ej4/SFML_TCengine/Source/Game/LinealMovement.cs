using SFML.Window;
using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    public class LinealMovement : BaseComponent
    {
        private TransformComponent TransComp;

        private float D_SPEED = 100.0f;
        private float Speed;

        private Vector2f m_Forward;
        private Vector2f m_MousePosition = new Vector2f(0f, 0f);

        public Vector2f Forward_
        {
            get => m_Forward;
        }

        public LinealMovement()
        {
            Speed = D_SPEED;
            m_Forward = new Vector2f(1f, 0f);
        }

        public LinealMovement(float _speed)
        {
            Speed = _speed;
            m_Forward = new Vector2f(1f, 0f);
        }

        public LinealMovement(float _speed, Vector2f _direction)
        {
            m_Forward = _direction;
            Speed = _speed;
            if (m_Forward.X > 1f|| m_Forward.Y > 1f)   
            { 
                m_Forward = new Vector2f(1f, 1f); 
            }
        }

        public override void OnActorCreated()
        {
            base.OnActorCreated();

            TransComp = Owner.GetComponent<TransformComponent>();
            TecnoCampusEngine.Get.Window.MouseMoved += HandleMouseMoved;
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);

            TransComp.Transform.Position += m_Forward * Speed * _dt;
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }
        private void HandleMouseMoved(object _sender, MouseMoveEventArgs _moveEventArgs)
        {
            m_MousePosition.X = _moveEventArgs.X;
            m_MousePosition.Y = _moveEventArgs.Y;
        }
        public override object Clone()
        {
            TargetComponent clonedComponent = new TargetComponent();
            return clonedComponent;
        }
    }
}
