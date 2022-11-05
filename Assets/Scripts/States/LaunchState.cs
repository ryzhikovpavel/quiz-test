using Core;

namespace States
{
    public class LaunchState: State
    {
        private readonly Config _config;

        public LaunchState(Context context) : base(context)
        {
            _config = context.Resolve<Config>();
        }

        public override void Enter()
        {
            Context.ChangeTo(new GameState(Context,0, _config.WordMinLetters));
        }

        public override void Exit()
        {
            
        }
    }
}