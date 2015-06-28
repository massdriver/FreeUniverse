using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Common.UI
{
    public class ViewControllerBase<T>
    {
        protected GameObject panelView { get; set; }

        public T controllerDelegate { get;set; }

        public bool visible
        {
            get
            {
                return panelView.activeSelf;
            }

            set
            {
                panelView.SetActive(value);
            }
        }

        public ViewControllerBase()
        {

        }

        public virtual void Update(float dt)
        {

        }
    }
}
