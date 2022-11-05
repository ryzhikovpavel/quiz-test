using System.Threading.Tasks;
using Core;
using Dictionary;
using UnityEngine;
using Views;

namespace States
{
    public class GameState: State
    {
        private readonly Config _config;
        private readonly ViewGame _viewGame;
        private readonly int _wordLength;
        private readonly string _word;
        private readonly int _score;
        private int _lettersFound;
        private int _lives;
        private int _errorCount;

        public GameState(Context context, int score, int wordLength): base(context)
        {
            _viewGame = Context.Resolve<ViewGame>();
            _config = Context.Resolve<Config>();
            _score = score;
            _wordLength = wordLength;
            _word = Context.Resolve<WordDictionary>().GetWord(_wordLength);
            _lettersFound = 0;
            _errorCount = 0;
            Debug.Log("Word: " + _word);
        }
        
        public override void Enter()
        {
            _viewGame.Show(_wordLength, _score, OnPlayerChoseLetter);
        }

        public override void Exit()
        {
            _viewGame.Release();
        }

        private void OnPlayerChoseLetter(char letter)
        {
            bool found = false;
            for (int i = 0; i < _wordLength; i++)
            {
                if (_word[i] == letter)
                {
                    _viewGame.OpenWordLetter(letter, i);
                    _lettersFound++;
                    found = true;
                }
            }

            _viewGame.DropLetterFromInput(letter);

            if (found) SuccessChose();
            else WrongChose();
        }

        private async void SuccessChose()
        {
            if (_lettersFound != _wordLength) return;
            _viewGame.DropAllLettersFromInput();
            await Task.Delay(1000);
            Context.ChangeTo(new VictoryState(Context, _score, _errorCount, _wordLength));
        }

        private void WrongChose()
        {
            _errorCount++;
            _viewGame.AddErrorPoint();
            if (_errorCount < _config.MaxErrorPerWord) return;
            Context.ChangeTo(new DefeatState(Context));
        }
    }
}