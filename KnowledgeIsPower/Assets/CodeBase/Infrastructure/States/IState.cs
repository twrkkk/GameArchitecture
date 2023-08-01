namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitable
    {
        public void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitable
    {
        public void Enter(TPayload payload);
    }


    public interface IExitable
    {
        public void Exit();
    }

}