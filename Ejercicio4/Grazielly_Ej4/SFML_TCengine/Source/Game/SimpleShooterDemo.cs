using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
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
            Actor actor = new Actor("Cube actor");
            Vector2f shapeSize = new Vector2f(40.0f, 40.0f);
            Shape shape = new RectangleShape(shapeSize);
            shape.FillColor = Color.Transparent;
            shape.OutlineColor = Color.Green;
            shape.OutlineThickness = 5.0f;
            actor.AddComponent<ShapeComponent>(shape);

            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(100.0f, 100.0f);


            TecnoCampusEngine.Get.Scene.CreateActor(actor);
        }

        private void CreateObjectSpawner()
        {
           
            Actor tanksSpawner = new Actor("Tank Spawner");
            ActorSpawnerComponent<ActorPrefab> spawnerComponent = tanksSpawner.AddComponent<ActorSpawnerComponent<ActorPrefab>>();
            const float spawnLimitOffset = 100.0f;
            spawnerComponent.m_MinPosition = new Vector2f(spawnLimitOffset, -spawnLimitOffset);
            spawnerComponent.m_MaxPosition = new Vector2f(TecnoCampusEngine.Get.ViewportSize.X - spawnLimitOffset, -spawnLimitOffset);
            spawnerComponent.m_MinTime = 0.5f;
            spawnerComponent.m_MaxTime = 5f;
            spawnerComponent.Reset();

            Vector2f tankForward = new Vector2f(0.0f, 1.0f);
            List<CollisionLayerComponent.ECollisionLayers> tankEnemyLayers = new List<CollisionLayerComponent.ECollisionLayers>();
            tankEnemyLayers.Add(CollisionLayerComponent.ECollisionLayers.Person);

            TecnoCampusEngine.Get.Scene.CreateActor(tanksSpawner);
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
