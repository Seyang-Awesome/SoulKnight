namespace MyEditor.BehaviourTree
{
    public class RootNode : NodeBase
    {
        public NodeBase child;
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            return child.Update();
        }

        public override NodeBase CloneNode()
        {
            RootNode node = Instantiate(this);
            node.child = child.CloneNode();
            return node;
        }
    }
}
