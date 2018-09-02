using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Forms {
    
    public class Form : MonoBehaviour {
        
        private readonly List<FormField> _fields = new List<FormField>();

        private EventSystem _system;

        protected void Start ()
        {
            _system = EventSystem.current; //  EventSystemManager.currentSystem;
        }

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;

            Selectable next;
            bool shiftPressed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
                
            // next input
            next = shiftPressed?_system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp():_system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next == null)
            {
                next = shiftPressed?gameObject.GetComponentsInChildren<Selectable>().Last():gameObject.GetComponentInChildren<Selectable>();
            }

            if (next == null) return;
                
            InputField inputfield = next.GetComponent<InputField>();

            if (inputfield != null)
            {
                inputfield.OnPointerClick(new PointerEventData(_system)); //if it's an input field, also set the text caret
            }
    							
            _system.SetSelectedGameObject(next.gameObject, new BaseEventData(_system));
        }

        public FormField AddField(string name) 
        {
            Transform field = transform.Find(name + "InputField");
            Transform errorLabel = transform.Find(name + "ErrorLabel");
            
            if (field == null && errorLabel == null)
            {
                throw new Exception(string.Concat("Field not found: ", field));
            }

            FormField formField = new FormField(name, field==null?null:field.gameObject, errorLabel==null?null:errorLabel.gameObject);

            _fields.Add(formField);

            return formField;
        }

        public void ResetForm()
        {   
            foreach(FormField field in _fields) {
                field.Reset();
            }
            
            ResetErrors();
        }

        public void ResetErrors()
        {
            foreach(FormField field in _fields) {
                field.SetMessage("");
            }
        }

        public FormField FindFormField(string name)
        {
            return _fields.Find((FormField obj) => obj.name == name);
        }

        public bool SetFieldValue(string fieldName, string value)
        {
            FormField field = FindFormField(fieldName);

            if (field==null)
            {
                return false;
            }
            field.SetValue(value);

            return true;
        }

        public bool SetMessage(string fieldName, string message, MessageType type=MessageType.Success)
        {
            FormField field = FindFormField(fieldName);

            if (field==null)
            {
                return false;
            }

            field.SetMessage(message, type);

            return true;

        }

        public string GetFieldValue(string fieldName)
        {
            FormField field = FindFormField(fieldName);

            if (field == null)
            {
                return null;
            }

            return field.GetValue();
        }
    }
}
