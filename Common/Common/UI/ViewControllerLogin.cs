using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FreeUniverse.Common.Common.UI
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
            panelView.SetActive(false);

            userInputField = Assist.FindUI<InputField>(panelView, "user_input");
            passwordInputField = Assist.FindUI<InputField>(panelView, "password_input");
            loginButton = Assist.FindUI<Button>(panelView, "login_button");

            loginButton.onClick = new Button.ButtonClickedEvent();
            loginButton.onClick.AddListener(this.OnLoginButtonPressed);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        private void OnLoginButtonPressed()
        {
            Debug.Log("press");
        }
    }
}
