using SFML.Graphics;
using SFML.System;
using TCEngine;

namespace TCGame
{
    class ComplexMovementGame : Game
    {
        public void Init()
        {
            Actor actor = new Actor("Follow Mouse Actor");
            Vector2f shapeSize = new Vector2f(50.0f, 50.0f);
            Shape shape = new RectangleShape(shapeSize);
            shape.FillColor = Color.Transparent;
            shape.OutlineColor = Color.Green;
            shape.OutlineThickness = 5.0f;
            actor.AddComponent<ShapeComponent>(shape);

            TransformComponent transformComponent = actor.AddComponent<TransformComponent>();
            transformComponent.Transform.Position = new Vector2f(100.0f, 100.0f);
            actor.AddComponent<FollowMouseComponent>(new Vector2f(1f, 0f));

            TecnoCampusEngine.Get.Scene.CreateActor(actor);
        }

        public void DeInit()
        {
        }

        public void Update(float _dt)
        {
        }
    }
}
