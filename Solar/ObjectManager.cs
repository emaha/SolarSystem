using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;

namespace Solar
{
    public class ObjectManager
    {
        private List<SpaceObject> objects = new List<SpaceObject>();
        private double G = 6.67;

        public void Add(SpaceObject obj)
        {
            objects.Add(obj);
        }

        public void Update()
        {
            foreach (var item in objects)
            {
                Vector2f sumVector = new Vector2f(0, 0);
                foreach (var vectorItem in objects.Where(x => x != item))
                {
                    sumVector += NormilizeVector(new Vector2f(vectorItem.Position.X - item.Position.X,
                        vectorItem.Position.Y - item.Position.Y)) * CalcForce(item, vectorItem);
                }

                item.Velocity += sumVector;

                item.Update();
            }
        }

        public void Draw(RenderTarget target)
        {
            foreach (var item in objects)
            {
                item.Draw(target);
            }
        }

        public double GetDistance(Vector2f a, Vector2f b)
        {
            double dx = Convert.ToDouble(b.X - a.X);
            double dy = Convert.ToDouble(b.Y - a.Y);

            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        public float CalcForce(SpaceObject a, SpaceObject b)
        {
            float r = (float)GetDistance(a.Position, b.Position);
            float force = (float)(G * (a.Mass * b.Mass) / Math.Pow(r, 2));

            return force;
        }

        public Vector2f NormilizeVector(Vector2f vect)
        {
            float length = (float)Math.Sqrt(vect.X * vect.X + vect.Y * vect.Y);
            float inv_length = (1 / length);
            return new Vector2f(vect.X * inv_length, vect.Y * inv_length);
        }
    }
}