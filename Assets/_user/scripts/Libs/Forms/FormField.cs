using UnityEngine;
using UnityEngine.UI;

namespace Forms
{
    public enum MessageType {Success, Warning, Error};

    public class FormField
    {
        public string name;
        private GameObject field;
        private GameObject mesageLabel;

        public FormField(string name, GameObject field, GameObject mesageLabel)
        {
            this.name = name;
            this.field = field;
            this.mesageLabel = mesageLabel;
        }

        public void Reset() {
            if (field)
                field.GetComponent<InputField>().text = "";

            if (mesageLabel)
                mesageLabel.GetComponent<Text>().text = "";
        }

        public void SetValue(string value)
        {
            field.GetComponent<InputField>().text = value;
        }

        public string GetValue()
        {
            return field.GetComponent<InputField>().text;
        }
        
        public void SetMessage(string message, MessageType type=MessageType.Success)
        {
            Text label = mesageLabel.GetComponent<Text>();

            switch (type)
            {
                case MessageType.Success:
                    label.color = Color.green;
                break;
                case MessageType.Warning:
                    label.color = Color.yellow;
                break;
                
                case MessageType.Error:
                    label.color = Color.red;
                break;      
            }

            label.text = message;
        }

        public InputField GetField()
        {
            return field.GetComponent<InputField>();
        }
    }
}

