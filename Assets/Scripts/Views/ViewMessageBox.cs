using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class ViewMessageBox: BaseView
    {
        [SerializeField] private TMP_Text textMessage;
        [SerializeField] private Button buttonOk;
        private Action _actionOk;

        public void Show(string message, Action actionOk)
        {
            gameObject.SetActive(true);
            
            textMessage.text = message;
            _actionOk = actionOk;
        }
        
        private void Start()
        {
            buttonOk.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _actionOk?.Invoke();
        }

        public void Release()
        {
            gameObject.SetActive(false);
        }
    }
}