using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.Behaviours
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LetterBox : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text letter;
        private CanvasGroup _canvasGroup;
        private bool _disabled;

        public event Action<LetterBox> EventClicked; 

        public char Value
        {
            get => letter.text[0];
            set => letter.text = value.ToString();
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnDisable()
        {
            EventClicked = null;
            Value = ' ';
            _canvasGroup.alpha = 1;
            _disabled = false;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_disabled == false)
                EventClicked?.Invoke(this);
        }

        public void Disable()
        {
            _canvasGroup.alpha = 0.35f;
            _disabled = true;
        }
    }
}