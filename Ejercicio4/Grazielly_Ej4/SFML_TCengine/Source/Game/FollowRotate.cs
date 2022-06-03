using SFML.Window;
using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    public class FollowRotate : BaseComponent
    {
        private TransformComponent TComp;
        private Vector2f MousePosition;

        private const float SPEED = 400.0f;

        private float Speed;

        public FollowRotate()//bajarlo
        {
            Speed = SPEED;
            AngularSpeed = A_SPEED;
        }
        public FollowRotate(float _speed, float _angularSpeed)
        {
            Speed = _speed;
            AngularSpeed = _angularSpeed;
        }

        private const float A_SPEED = 360.0f;
        private float AngularSpeed;


        private Vector2f m_WantedForward = new Vector2f(1.0f, 0.0f);
        private Vector2f m_PreviousForward = new Vector2f(1.0f, 0.0f);

        public Vector2f Forward

        {
            get => m_PreviousForward;
        }

        public override void OnActorCreated()
        {
            base.OnActorCreated();

            TComp = Owner.GetComponent<TransformComponent>();
            TecnoCampusEngine.Get.Window.MouseMoved += HandleMouseMoved;
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);

            Vector2f v_PosToMousePos = MousePosition - TComp.Transform.Position;

            m_WantedForward = v_PosToMousePos.Normal();



            float angleDesired = MathUtil.Angle(m_WantedForward, m_PreviousForward);

            float angleToRotate = AngularSpeed * _dt;

            if (angleToRotate > angleDesired)
            {
                angleToRotate = angleDesired; 
            }

            float sign = MathUtil.Sign(m_WantedForward, m_PreviousForward);
            angleToRotate = angleToRotate * sign;

            m_PreviousForward = m_PreviousForward.Rotate(angleToRotate);
            TComp.Transform.Rotation = MathUtil.AngleWithSign(m_PreviousForward, new Vector2f(0f, -1f));
            Vector2f newPosition = TComp.Transform.Position + m_PreviousForward * Speed * _dt;
            TComp.Transform.Position = newPosition;
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public override object Clone()
        {
            TargetComponent ClonedComp  = new TargetComponent();
            return ClonedComp;
        }

        private void HandleMouseMoved(object _sender, MouseMoveEventArgs _moveEventArgs)
        {
            MousePosition.X = _moveEventArgs.X;
            MousePosition.Y = _moveEventArgs.Y;
        }

    }

}