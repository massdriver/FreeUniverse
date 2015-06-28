using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace FreeUniverse.Common.Common.UI
{
    public interface IViewControllerCreateAccountDelegate
    {
        void OnViewControllerCreateAccountAction(ViewControllerCreateAccount controller, string email, string password);
    }

    public class ViewControllerCreateAccount : ViewControllerBase<IViewControllerCreateAccountDelegate>
    {
        private static readonly string PANEL_PREFAB_PATH = "UI/prefab_ui_panel_create_account";

        private InputField email { get; set; }
        private InputField password { get; set; }
        private Button createButton { get; set; }
        
        public ViewControllerCreateAccount()
        {
            panelView = Assist.LoadUI(PANEL_PREFAB_PATH);
            visible = false;

            email = Assist.FindUI<InputField>(panelView, "email");
            password = Assist.FindUI<InputField>(panelView, "password");
            createButton = Assist.FindUI<Button>(panelView, "create_button");

            createButton.onClick = new Button.ButtonClickedEvent();
            createButton.onClick.AddListener(this.OnCreateButtonPressed);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        private bool ValidateInput()
        {
            return false;
        }

        private void OnCreateButtonPressed()
        {
            if (!ValidateInput())
                return;

            if (controllerDelegate != null)
                controllerDelegate.OnViewControllerCreateAccountAction(this, email.text, password.text);
        }
    }
}
