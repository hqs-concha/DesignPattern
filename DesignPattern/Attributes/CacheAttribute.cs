using System;

namespace DesignPattern.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        public string Key { get; set; }
        public int ExpireMinutes { get; set; } = 30;
    }
}
