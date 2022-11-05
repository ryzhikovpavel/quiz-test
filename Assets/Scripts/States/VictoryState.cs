using System;
using Core;
using Views;

namespace States
{
    public class VictoryState: State
    {
        private const string Message = "You win!";
        private readonly int _nextWordLength;
        private readonly int _score;
        private readonly ViewMessageBox _view;

        public VictoryState(Context context, int beginScore, int errorCount, int wordLength): base(context)
        {
            Config config = Context.Resolve<Config>();
            _view = Context.Resolve<ViewMessageBox>();

            if (wordLength >= config.WordMaxLetters) 
                _nextWordLength = config.WordMaxLetters;
            else
                _nextWordLength = wordLength + 1;
            _score = beginScore + config.MaxErrorPerWord - errorCount;
        }
        
        public override void Enter()
        {
            _view.Show(Message, ()=>Context.ChangeTo(new GameState(Context, _score, _nextWordLength)));
        }

        public override void Exit()
        {
            _view.Release();
        }
    }
}