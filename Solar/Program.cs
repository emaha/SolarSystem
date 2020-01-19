using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Solar
{
    internal class Program
    {
        public static Vector2f Offset = new Vector2f(-600, -400);
        public static float Scale = 8000f;
        private static readonly Time UPS = Time.FromSeconds(1 / 60f);
        private static bool isExit = false;
        private static Clock clock = new Clock();
        private static Time accum = Time.Zero;

        private static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(1200, 800), "Solar");
            window.Closed += OnClose;
            window.KeyPressed += OnKeyPressed;
            window.SetActive();

            ObjectManager manager = new ObjectManager();

            SpaceObject Sun = new SpaceObject()
            {
                Mass = 1_989_100_000f,
                Velocity = new Vector2f(0, 0),
                Position = new Vector2f(-150_000_000f, 0),
                Radius = 1_392_000f,

                Color = Color.Red
            };
            Sun.Init();

            SpaceObject Earth = new SpaceObject()
            {
                Mass = 5973.6f,
                Velocity = new Vector2f(0, 10800f),
                Position = new Vector2f(0, 0),
                Radius = 12742f,

                Color = Color.Blue
            };
            Earth.Init();

            SpaceObject Moon = new SpaceObject()
            {
                Mass = 73.5f,
                Velocity = Earth.Velocity + new Vector2f(0, 1.02f),
                Position = new Vector2f(300_000f, 0),
                Radius = 3472f,
                Color = Color.White
            };
            Moon.Init();

            //Random r = new Random();
            //for (int i = 0; i < 100; i++)
            //{
            //    SpaceObject sp = new SpaceObject()
            //    {
            //        Position = new Vector2f(r.Next(500) - 250f, r.Next(500) - 250f),

            //        Radius = r.Next(20) + 10,
            //        Color = Color.Cyan,
            //        Velocity = new Vector2f(r.Next(2) - 1f, r.Next(2) - 1f),
            //        Mass = r.Next(200) + 150f
            //    };
            //    sp.Init();
            //    manager.Add(sp);
            //}

            //SpaceObject Mars = new SpaceObject()
            //{
            //    Mass = 2973.6f,
            //    Velocity = new Vector2f(0, -2.022f),
            //    Position = new Vector2f(-384403, 0),
            //    Radius = 6,
            //    Color = Color.Red
            //};
            //Mars.Init();

            manager.Add(Sun);
            manager.Add(Earth);
            manager.Add(Moon);
            //manager.Add(Mars);
            //manager.Add(Mars1);

            while (window.IsOpen && !isExit)
            {
                window.Clear();
                window.DispatchEvents();

                if (accum >= UPS)
                {
                    accum -= UPS;
                    HandleKeyboard();

                    Offset = Earth.Position /Scale - new Vector2f(600, 400); 
                    manager.Update();

                    window.SetTitle($"{Earth.Position.X} / {Offset.X} : {Offset.Y}");
                }

                manager.Draw(window);
                accum += clock.Restart();
                window.Display();
            }

            window.Close();
        }

        public static void HandleKeyboard()
        {
            float scrollSpeed = 20f;

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                Offset -= new Vector2f(scrollSpeed, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                Offset += new Vector2f(scrollSpeed, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                Offset -= new Vector2f(0, scrollSpeed);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                Offset += new Vector2f(0, scrollSpeed);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                Scale *= 1.05f;
                Console.WriteLine(Scale);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                Scale *= 0.95f;
                Console.WriteLine(Scale);
            }
        }

        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                isExit = true;
            }
        }

        private static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
    }
}