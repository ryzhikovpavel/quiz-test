using Core;
using Views;

namespace States
{
    public class DefeatState: State
    {
        private const string Message = "You loss";
        private readonly ViewMessageBox _view;

        public DefeatState(Context context) : base(context)
        {
            _view = Context.Resolve<ViewMessageBox>();
        }
        
        public override void Enter()
        {
            _view.Show(Message, ()=>Context.ChangeTo(new LaunchState(Context)));
        }

        public override void Exit()
        {
            _view.Release();
        }
    }
}