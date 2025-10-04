namespace LD58Game.StatemachineModule
{
    public interface IStatemachine
    {
        bool ChangeState<T>() where T : GameState;
        public void Update();
    }
}
