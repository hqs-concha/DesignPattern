using System;

namespace DesignPattern.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        public string Name { get; set; }

        public CacheAttribute(string name)
        {
            Name = name;
        }
    }
}
