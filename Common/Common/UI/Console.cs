using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FreeUniverse.Common.UI
{
    public sealed class Console
    {
        public static Console instance { get; private set; }

        public static void Init()
        {
            instance = new Console();
        }

        private GameObject consolePanel { get; set; }
        private InputField inputField { get; set; }
        private Text textField { get; set; }
        private List<string> messages { get; set; }
        private bool invalidState { get; set; }

        public const string UI_PREFAB = "UI/ui_console_panel";
        public const string UI_INPUT_FIELD = "text_input_field";
        public const string UI_TEXT_OUTPUT = "text_output";

        public Console()
        {
            invalidState = true;
            messages = new List<string>();
            consolePanel = Assist.LoadUI(UI_PREFAB);
            inputField = Assist.FindComponent<InputField>(consolePanel, UI_INPUT_FIELD);
            textField = Assist.FindComponent<Text>(consolePanel, UI_TEXT_OUTPUT);
        }

        public void Update()
        {
            if (inputField == null)
                return;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                string input = inputField.text;

                if (input.Length == 0)
                    return;

                SubmitInput(input);
                invalidState = true;
            }

            UpdateText();
        }

        public void PostMessage(string text)
        {
            if (text == null)
                return;

            invalidState = true;

            if (text.Length == 0)
                messages.Add("");
            else
                messages.Add(text);
        }

        void UpdateText()
        {
            StringBuilder builder = new StringBuilder();

            foreach (string s in messages)
            {
                builder.AppendLine(s);
            }

            textField.text = builder.ToString(0, builder.Length);

            invalidState = false;
        }

        void SubmitInput(string text)
        {
            PostMessage(text);
        }
    }
}
