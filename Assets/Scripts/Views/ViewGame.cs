using System;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.Behaviours;

namespace Views
{
    public class ViewGame: BaseView
    {
        private const char MinInputLetter = 'A';
        private const char MaxInputLetter = 'Z';
        private const char HiddenCharacter = '?';
        
        [SerializeField] private ComponentPool<LetterBox> wordLetterPool; 
        [SerializeField] private ComponentPool<LetterBox> inputLetterPool;
        [SerializeField] private ComponentPool<Transform> errorPool;
        [SerializeField] private TMP_Text textScore;
        private readonly List<LetterBox> _word = new List<LetterBox>();
        private Action<char> _actionChose;

        public void Show(int wordLength, int score, Action<char> actionChose)
        {
            gameObject.SetActive(true);
            _actionChose = actionChose;
            errorPool.ReleaseAll();
            wordLetterPool.ReleaseAll();
            _word.Clear();
            for (int i = 0; i < wordLength; i++)
            {
                var box = wordLetterPool.Get();
                box.Value = HiddenCharacter;
                _word.Add(box);
            }
            
            inputLetterPool.ReleaseAll();
            for (char c = MinInputLetter; c <= MaxInputLetter; c++)
            {
                var box = inputLetterPool.Get();
                box.Value = c;
                box.EventClicked += OnLetterClicked;
            }

            textScore.text = $"SCORE: {score}";
        }

        public void Release()
        {
            gameObject.SetActive(false);
            _actionChose = null;
        }

        public void OpenWordLetter(char letter, int index)
        {
            _word[index].Value = letter;
        }

        public void DropLetterFromInput(char letter)
        {
            foreach (LetterBox box in inputLetterPool)
            {
                if (box.Value == letter)
                {
                    box.Disable();
                }
            }
        }

        public void AddErrorPoint()
        {
            errorPool.Get();
        }
        
        private void OnLetterClicked(LetterBox box)
        {
            _actionChose?.Invoke(box.Value);
        }

        public void DropAllLettersFromInput()
        {
            foreach (LetterBox box in inputLetterPool)
            {
                box.Disable();
            }
        }
    }
}