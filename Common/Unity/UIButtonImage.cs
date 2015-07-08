using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FreeUniverse.Common.Unity
{
    public interface UIButtonImageDelegate
    {
        void OnUIButtonImageClick(UIButtonImage button);
    }

    public class UIButtonImage : Image
    {
        public UIButtonImageDelegate buttonDelegate { get; set; }

        protected UIButtonImage()
        {
            fadeSpeed = 1.0f;
            maxRollOverFade = 0.5f;
            clickFade = 1.0f;
        }

        public bool IsMousePointerInside()
        {
            RectTransform tr = GetComponent<RectTransform>();

            if (tr.parent != null)
            {
                RectTransform parentRect = (tr.parent.parent as RectTransform);

                Vector2 pl;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, Input.mousePosition, null, out pl);

                if (!parentRect.rect.Contains(pl))
                    return false;
            }

            Vector2 lp;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(tr, Input.mousePosition, null, out lp);

            return tr.rect.Contains(lp);
        }

        private float fade { get; set; }

        public float clickFade { get; set; }
        public float fadeSpeed { get; set; }
        public float maxRollOverFade { get; set; }

        private void UpdateFadeColor()
        {
            float deltaFade = fadeSpeed * Time.deltaTime;

            if (IsMousePointerInside())
            {
                if (fade < maxRollOverFade)
                {
                    fade += deltaFade;
                }
 
                CheckClickInside();
            }
            else
            {
                fade -= deltaFade;
            }

            if (fade < 0.0f)
                fade = 0.0f;

            color = new Color(color.r, color.g, color.b, fade);
        }

        bool CheckClickInside()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if( buttonDelegate != null )
                    buttonDelegate.OnUIButtonImageClick(this);

                fade = clickFade;

                return true;
            }

            return false;
        }

        void Update()
        {
            if (enabled)
            {
                UpdateFadeColor();

                
            }
        }
    }
}
