using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Common.ECS.Components
{
    public struct Bindings
    {
        public Dictionary<string, int> Pairs { get; private set; }

        public Bindings(string fileName)
        {
            Pairs = null;
            InitializeBindings(fileName);
        }

        void InitializeBindings(string fileName)
        {
            var elements = XElement.Load(@".\Content\Data\Bindings\" + fileName + ".xml").Elements();
            Pairs = elements.ToDictionary(e => e.Name.LocalName, e=> Convert.ToInt32(e.Value));
        }

        public static Bindings operator +(Bindings a, Bindings b)
        {
            foreach (var item in b.Pairs)
            {
                a.Pairs.Add(item.Key, item.Value);
            }

            return a;
        }
    }
}