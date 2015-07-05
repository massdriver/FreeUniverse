using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UI;

namespace FreeUniverse.Common.UI
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

            email = Assist.FindComponent<InputField>(panelView, "email");
            password = Assist.FindComponent<InputField>(panelView, "password");
            createButton = Assist.FindComponent<Button>(panelView, "create_button");

            createButton.onClick = new Button.ButtonClickedEvent();
            createButton.onClick.AddListener(this.OnCreateButtonPressed);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        private bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private bool ValidateInput()
        {
            string emailAddress = email.text;

            if (!IsValidEmail(emailAddress))
                return false;

            return password.text.Length > 6;
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
