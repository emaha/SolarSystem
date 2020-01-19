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
        public Shape rect;

        public void Init()
        {
            shape = new CircleShape(Radius) 
            { 
                FillColor = Color, 
                Origin = new Vector2f(Radius, Radius) 
            };

            rect = new RectangleShape(new Vector2f(20,20))
            {
                FillColor = Color.Black, 
                Origin = new Vector2f(10, 10),
                OutlineThickness = 1,
                OutlineColor = Color.Green,
            };

        }

        public void Update()
        {
            Position += Velocity;
            shape.Position = Position / Program.Scale - Program.Offset;
            rect.Position = Position / Program.Scale - Program.Offset;

            shape.Scale = new Vector2f(1f / Program.Scale, 1f / Program.Scale);
            
            //if (1f / Program.Scale < 0.001f)
            //{
            //    shape.Scale = new Vector2f(0.1f, 0.1f);
            //}
            //else
            //{
            //    shape.Scale = new Vector2f(1f / Program.Scale, 1f / Program.Scale);
            //}
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(rect);
            target.Draw(shape);
        }
    }
}