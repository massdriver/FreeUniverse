using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FreeUniverse.Common.UI
{
    public interface IViewControllerLoginDelegate
    {
        void OnViewControllerLoginAction(ViewControllerLogin controller, string user, string password);
    }

    public sealed class ViewControllerLogin : ViewControllerBase<IViewControllerLoginDelegate>
    {
        private static readonly string PANEL_PREFAB_PATH = "UI/prefab_ui_panel_login";

        private InputField userInputField { get; set; }
        private InputField passwordInputField { get; set; }
        private Button loginButton { get; set; }

        public ViewControllerLogin()
        {
            panelView = Assist.LoadUI(PANEL_PREFAB_PATH);
            visible = false;

            userInputField = Assist.FindComponent<InputField>(panelView, "user_input");
            passwordInputField = Assist.FindComponent<InputField>(panelView, "password_input");
            loginButton = Assist.FindComponent<Button>(panelView, "login_button");

            loginButton.onClick = new Button.ButtonClickedEvent();
            loginButton.onClick.AddListener(this.OnLoginButtonPressed);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        private bool ValidateInput()
        {
            return false;
        }

        private void OnLoginButtonPressed()
        {
            if (!ValidateInput())
                return;

            if (controllerDelegate != null)
            {
                controllerDelegate.OnViewControllerLoginAction(this, userInputField.text, passwordInputField.text);
            }
                
        }
    }
}
