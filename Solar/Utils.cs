﻿using SFML.System;

namespace Solar
{
    internal class Utils
    {
        private Vector2f Lerp(Vector2f v0, Vector2f v1, float t)
        {
            return (1 - t) * v0 + t * v1;
        }
    }
}