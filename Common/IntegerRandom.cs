using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common
{
    public static class FastRandom
    {
        public static void SetSeed(uint seed)
        {
            m_z = (uint)seed;
            m_w = (uint)seed + 3;
        }

        private static uint m_z;
        private static uint m_w;

        public static uint Next()
        {
            m_z = 36969 * (m_z & 65535) + (m_z >> 16);
            m_w = 18000 * (m_w & 65535) + (m_w >> 16);
            return (m_z << 16) + m_w;
        }

        public static uint Next(uint max)
        {
            m_z = 36969 * (m_z & 65535) + (m_z >> 16);
            m_w = 18000 * (m_w & 65535) + (m_w >> 16);
            return ((m_z << 16) + m_w) % max;
        }
    }
}
