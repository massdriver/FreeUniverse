using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public class RotationScript : MonoBehaviour
    {
        public Vector3 speed;

        void Update()
        {
            this.gameObject.transform.Rotate(speed * Time.deltaTime, Space.World);
        }
    }
}
