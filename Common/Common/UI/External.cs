using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FreeUniverse.Common.UI
{
    public static class External
    {
        public static void Init()
        {
            GameObject canvasObj = GameObject.Find("Canvas");

            if (canvasObj == null)
            {
                Debug.Log("Canvas not found");
                return;
            }

            canvas = canvasObj.GetComponent<Canvas>();

            if (canvas == null)
                Debug.Log("Canvas not found");
        }

        public static Canvas canvas { get; private set; }
    }
}
