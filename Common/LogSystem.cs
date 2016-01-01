using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common
{
    public static class LogSystem
    {
        public static void Write(object obj)
        {
            Debug.Log(obj);
        }
    }
}
