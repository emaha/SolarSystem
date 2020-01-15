using SFML.Graphics;
using SFML.System;

namespace Solar
{
    public class SpaceObject
    {
        public double Mass { get; set; }
        public Vector2f Position { get; set; }
        public Vector2f Velocity { get; set; }
        public float Radius { get; set; }
        public Color Color { get; set; }

        public Shape shape;

        public void Init()
        {
            shape = new CircleShape(Radius) { FillColor = Color, Origin = new Vector2f(Radius / 2, Radius / 2) };
        }

        public void Update()
        {
            Position += Velocity;
            shape.Position = Position / Program.Scale - Program.Offset;

            if (1f / Program.Scale < 0.01f)
            {
                shape.Scale = new Vector2f(0.1f, 0.1f);
            }
            else
            {
                shape.Scale = new Vector2f(1f / Program.Scale, 1f / Program.Scale);
            }
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(shape);
        }
    }
}