using System;
using System.Collections.Generic;

public class StageManager : MonoSingleton<StageManager>
{
    private Dictionary<Type, IStage> stages = new();
    public IStage currentStage;

    private void Start()
    {
        
    }

    private void BeginStage(Type stageType)
    {
        currentStage = stages[stageType];
        stages[stageType].OnEnter();
    }

    public void SwitchStage(Type stageType)
    {
        currentStage.OnExit();
        currentStage = stages[stageType];
        currentStage.OnEnter();
    }

    private void ExitStage()
    {
        currentStage.OnExit();
        currentStage = null;
    }

    public IStage GetCurrentStage()
    {
        return currentStage;
    }

    public T GetCurrentStage<T>() where T : IStage
    {
        try
        {
            T currentStage = (T)this.currentStage;
            return currentStage;
        }
        catch
        {
            return default;
        }
    }
    
}