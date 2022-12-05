using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Common.Settings;
using MonoGame.Extended.Input;

namespace Common.ECS.Components
{
    public struct Controller
    {
        public Dictionary<string, bool> Holdings { get; private set; }
        public Dictionary<string, bool> Pressings { get; private set; }
        public Dictionary<string, bool> Unpressings { get; private set; }

        public void Init(Bindings _bindings)
        {
            Holdings = new Dictionary<string, bool>();
            Pressings = new Dictionary<string, bool>();
            Unpressings = new Dictionary<string, bool>();
            InitializeFlags(Holdings, bindings);
            InitializeFlags(Pressings, bindings);
            InitializeFlags(Unpressings, bindings);
        }

        void InitializeFlags(Dictionary<string, bool> dictionary, Bindings bindings)
        {
            var pairs = bindings.Pairs;

            foreach (var pair in pairs)
            {
                dictionary.Add(pair.Key, false);
            }
        }

        public void SetHolding(string key, bool value)
        {
            Holdings[key] = value;
        }

        public void SetPressing(string key, bool value)
        {
            Pressings[key] = value;
        }

        public void SetUnpressing(string key, bool value)
        {
            Unpressings[key] = value;
        }

        public bool IsHolding(string key)
        {
            bool containKey = Holdings.ContainsKey(key);
            bool value = containKey && Holdings[key];

            if(value)
            {
                Console.WriteLine($"{key} is holding");
            }

            return value;
        }

        public bool WasPressed(string key)
        {
            bool containKey = Pressings.ContainsKey(key);
            bool value = containKey && Pressings[key];

            if(value)
            {
                Console.WriteLine($"{key} was pressed");
            }

            return value;
        }

        public bool WasUnpressed(string key)
        {
            bool containKey = Unpressings.ContainsKey(key);
            bool value = containKey && Unpressings[key];

            if(value)
            {
                Console.WriteLine($"{key} was unpressed");
            }

            return value;
        }

        public Vector2 GetInputVector(string left = "", string right = "", string up = "", string down = "")
        {
            var result = Vector2.Zero;

            if(IsHolding(left) && left != "")
            {
                result.X -= 1;
            }
            if(IsHolding(right) && right != "")
            {
                result.X += 1;
            }
            if(IsHolding(down) && down != "")
            {
                result.Y -= 1;
            }
            if(IsHolding(up) && up != "")
            {
                result.Y += 1;
            }

            return result;
        }

        public Vector2 GetInputVector(string _left, string _right, string _up, string _down)
        {
            var result = Vector2.Zero;

            if(IsHolding(_left))
            {
                result.X -= 1;
            }
            if(IsHolding(_right))
            {
                result.X += 1;
            }
            if(IsHolding(_down))
            {
                result.Y -= 1;
            }
            if(IsHolding(_up))
            {
                result.Y += 1;
            }

            return result;
        }
    }
}