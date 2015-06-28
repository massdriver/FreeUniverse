using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Procedural
{
    public static class CubemapGenerator
    {
        public static Cubemap Make(Camera camera)
        {
            Cubemap cmap = new Cubemap(4096, TextureFormat.ARGB32, false);

            if (camera.RenderToCubemap(cmap))
            {
                
                return cmap;
            }

            return null;
            
        }
    }
}
