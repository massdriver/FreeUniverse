using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public sealed class UIForm : MonoBehaviour
    {
        public bool mayDrag { get; set; }

        private bool drag { get; set; }
        private Vector3 prevPosition { get; set; }

        void Update()
        {
            if (IsMousePointerInside() && Input.GetMouseButtonDown(0))
            {
                drag = true;
                prevPosition = Input.mousePosition;
            }

            if (drag && Input.GetMouseButtonUp(0))
            {
                drag = false;
            }

            if (drag)
            {
                Vector3 delta = Input.mousePosition - prevPosition;

                prevPosition = Input.mousePosition;

                RectTransform tr = GetComponent<RectTransform>();

                tr.position += delta;
            }
        }

        private bool IsMousePointerInside()
        {
            RectTransform tr = GetComponent<RectTransform>();

            Vector2 lp;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(tr, Input.mousePosition, null, out lp);

            return tr.rect.Contains(lp);
        }
    }
}
