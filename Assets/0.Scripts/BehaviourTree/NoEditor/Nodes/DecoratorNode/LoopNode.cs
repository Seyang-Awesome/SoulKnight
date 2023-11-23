namespace MyEditor.BehaviourTree
{
    /// <summary>
    /// 循环节点，表明该节点的子节点会一直执行（一般作为根节点）
    /// </summary>
    public class LoopNode : DecoratorNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            child.Update();
            return NodeState.Running;
        }
    }
}