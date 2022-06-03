using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    class SimpleShooterDemo : Game
    {
        public void Init()
        {
            CreateMainCharacter();
            CreateObjectSpawner();
            CreateHUD();
        }

        public void DeInit()
        {
        }

        public void Update(float _dt)
        {
        }

        private void CreateMainCharacter()
        {
            Actor actor = new Actor("Following Mouse Actor");

            // Create an arrow shape using a ConvexShape
            ConvexShape shape = new ConvexShape(4);
            shape.SetPoint(0, new Vector2f(20.0f, 0.0f));
            shape.SetPoint(1, new Vector2f(40.0f, 40.0f));
            shape.SetPoint(2, new Vector2f(20.0f, 20.0f));
            shape.SetPoint(3, new Vector2f(0.0f, 40.0f));
            shape.FillColor = Color.Transparent;
            shape.OutlineColor = Color.Green;
            shape.OutlineThickness = 2.0f;
            actor.AddComponent<ShapeComponent>(shape);

            // Add the transform component and set its position correctly
            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(600.0f, 200.0f);

            // TODO:
            // - Add your new component (the one that follows the mouse poisition) to the actor
            //   with a linear speed of 400 pixels/second and an angular speed of 360 degrees/second

            actor.AddComponent<FollowRotate>();

            //////////////////////////////////////////////////

            actor.AddComponent<WeaponComponent>();

            // Add the actor to the scene
            TecnoCampusEngine.Get.Scene.CreateActor(actor);
        }

        private void CreateObjectSpawner()
        {
            Actor Spawner = new Actor("Enemigo Spawner");
            ActorSpawnerComponent<ActorPrefab> YellowEnemy = Spawner.AddComponent<ActorSpawnerComponent<ActorPrefab>>();

            YellowEnemy.m_MinPosition = new Vector2f(80.0f, 80.0f);
            YellowEnemy.m_MaxPosition = new Vector2f(TecnoCampusEngine.WINDOW_WIDTH - 80f, TecnoCampusEngine.WINDOW_HEIGHT - 80f);
            YellowEnemy.m_MaxTime = 3.0f;
            YellowEnemy.m_MinTime = 0.2f;
            YellowEnemy.Reset();

            ActorPrefab DisparoPrefab = new ActorPrefab("Bullet");
            ShapeComponent DisparoShape = DisparoPrefab.AddComponent<ShapeComponent>(new CircleShape(20f));
            DisparoShape.Shape.FillColor = Color.Transparent;
            DisparoShape.Shape.OutlineColor = Color.Yellow;
            DisparoShape.Shape.OutlineThickness = 1.0f;
            TransformComponent DisparoTransform = DisparoPrefab.AddComponent<TransformComponent>();
            TargetComponent TargetTransform = DisparoPrefab.AddComponent<TargetComponent>();

            YellowEnemy.AddActorPrefab(DisparoPrefab);
            TecnoCampusEngine.Get.Scene.CreateActor(Spawner);
        }

        private void CreateHUD()
        {
            Actor actor = new Actor("HUD Actor");

            // Add the transform component and set its position correctly
            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(900.0f, 50.0f);
            actor.AddComponent<HUDComponent>("Puntos");

            // Something is missing here!!!
            TecnoCampusEngine.Get.Scene.CreateActor(actor);
            //////////////////////////////////////
        }

    }
}
