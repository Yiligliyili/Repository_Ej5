using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Diagnostics;
using TCEngine;

namespace TCGame
{
    public class WeaponComponent : BaseComponent
    {
        private const float DEFAULT_FIRE_RATE = 0.3f;

        private float m_FireRate;
        private float m_TimeToShoot;
        private FollowRotate FRotate;
        public WeaponComponent()
        {
            m_FireRate = DEFAULT_FIRE_RATE;
            m_TimeToShoot = 0.0f;
        }

        public WeaponComponent(float _fireRate)
        {
            m_FireRate = _fireRate;
            m_TimeToShoot = 0.0f;
        }

        public override void OnActorCreated()
        {
            base.OnActorCreated();
            FRotate =Owner.GetComponent<FollowRotate>();
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);

            if(m_TimeToShoot > 0.0f)
            {
                m_TimeToShoot -= _dt;
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Shoot();
            }

        }

        public void Shoot()
        {
            if (m_TimeToShoot <= 0.0f)
            {
                Actor bulletActor = new Actor("Bullet Actor");

                // Create a Rectangle Shape 
                Shape shape = new RectangleShape(new Vector2f(10.0f, 30.0f));
                shape.FillColor = Color.Transparent;
                shape.OutlineColor = Color.Red;
                shape.OutlineThickness = 2.0f;
                bulletActor.AddComponent<ShapeComponent>(shape);

                // Get the transform of the actor that is owner of this instance of the WeaponComponent
                TransformComponent actorTransform = Owner.GetComponent<TransformComponent>();

                // Add the a TransformComponent to the new bulletActor
                TransformComponent transformComponent = bulletActor.AddComponent<TransformComponent>();

                // Assign the Position and Rotation of the actor that will shoot the bullet (this way, the bullets will appear in the same position as the actor)
                transformComponent.Transform.Position = actorTransform.Transform.Position;
                transformComponent.Transform.Rotation = actorTransform.Transform.Rotation;

                // TODO:
                // 1. Get the component where you store the m_Forward information
                // 2. Add a component to the bulletActor that:
                //      - Moves in the same direction as this actor (you will get the information from the component you got in the previous line)
                //      - The speed must be 700 pixels/second
                // 3. Add a TimerComponent with a duration of 2 seconds
                //      - When the timer finishes, the bulletActor must be destroyed
                // 4. Add the bulletActor to the Scene

                Vector2f _forward = FRotate.Forward;

                bulletActor.AddComponent<BulletComponent>();
                bulletActor.AddComponent<LinealMovement>(700f, _forward);
                bulletActor.AddComponent<TimerComponent>(2f);

                TecnoCampusEngine.Get.Scene.CreateActor(bulletActor);

                ////////////////////////////////////////////////////////

                m_TimeToShoot = m_FireRate;
            }
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.PostUpdate;
        }

        public override object Clone()
        {
            WeaponComponent clonedComponent = new WeaponComponent(m_FireRate);
            return clonedComponent;
        }
    }
}
