using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public class MovableCamera : MonoBehaviour
    {
        public void Update()
        {
            //float movementSpeed = 0.05f;

            //if (Input.GetKey(KeyCode.W))
            //    gameObject.transform.Translate(new Vector3(0.0f, 0.0f, movementSpeed) * Time.deltaTime, Space.Self);

            //if (Input.GetKey(KeyCode.S))
            //    gameObject.transform.Translate(new Vector3(0.0f, 0.0f, -movementSpeed) * Time.deltaTime, Space.Self);

            //gameObject.transform.LookAt(new Vector3(0.0f, 0.1f, 0.0f));
            gameObject.transform.Rotate(0.0f, 1.0f * Time.deltaTime, 0.0f, Space.Self);
            //gameObject.transform.Translate(speed * Time.deltaTime, Space.Self);
            
        }
    }
}
