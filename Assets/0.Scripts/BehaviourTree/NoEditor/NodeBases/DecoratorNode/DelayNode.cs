using UnityEngine;

namespace MyEditor.BehaviourTree
{
    /// <summary>
    /// 延迟节点，延迟一定时间后执行子节点
    /// </summary>
    public class DelayNode : DecoratorNode
    {
        public float duration;
        float currentTime;

        public DelayNode()
        {
        }

        public DelayNode(float duration)
        {
            this.duration = duration;
        }

        public override void OnStart()
        {
            currentTime = 0;
        }

        public override void OnStop()
        {
        }

        public override NodeState OnUpdate()
        {
            currentTime += Time.deltaTime;
            if (currentTime <= duration)
                return NodeState.Running;
            else
                return child.Update();
        }
    }
}