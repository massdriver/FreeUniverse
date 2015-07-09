using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.UI
{
    public class ViewControllerBase<DelegateInterfaceType>
    {
        protected GameObject panelView { get; set; }

        public DelegateInterfaceType controllerDelegate { get;set; }

        public bool visible
        {
            get
            {
                if (panelView == null)
                    return false;

                return panelView.activeSelf;
            }

            set
            {
                if (panelView == null)
                    return;

                panelView.SetActive(value);
            }
        }

        public ViewControllerBase()
        {
            visible = false;
        }

        public virtual void Update(float dt)
        {

        }
    }
}
