public abstract class StageBase : IStage
{
    protected abstract InvokableAction onStageEnter { get; set; }
    protected abstract InvokableAction onStageExit { get; set; }

    public void OnEnter()
    {
        onStageEnter.Invoke();
    }

    public void OnExit()
    {
        onStageExit.Invoke();
    }
}