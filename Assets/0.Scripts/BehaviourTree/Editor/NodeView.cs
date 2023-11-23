using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyEditor.BehaviourTree
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public NodeBase node;
        public Port input;
        public Port output;

        public Action<NodeView> onNodeViewSelected;
        public Action onNodeViewDeselected;
        public NodeView(NodeBase node)
        {
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.guid;

            CreateInputPort();
            CreateOutputPort();
        }

        void CreateInputPort()
        {
            if(node is CompositeNode)
            {
                input = InstantiatePort(Orientation.Horizontal,Direction.Input,Port.Capacity.Single,typeof(NodeView));
            }
            else if(node is DecoratorNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(NodeView));
            }
            else if(node is ActionNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(NodeView));
            }
            else if (node is RootNode)
            {
                //input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(NodeView));
                input = null;
            }

            if (input != null)
            {
                input.portName = "Input";
                inputContainer.Add(input);              
            }
        }

        void CreateOutputPort()
        {
            if (node is CompositeNode)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(NodeView));
            }
            else if (node is DecoratorNode)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(NodeView));
            }
            else if (node is ActionNode)
            {
                //output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(NodeView));
                output = null;
            }
            else if (node is RootNode)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(NodeView));
            }

            if (output != null)
            {
                output.portName = "Output";
                outputContainer.Add(output);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);

            node.posInView = newPos.position;
            
            // EditorUtility.SetDirty(node);
            // AssetDatabase.SaveAssets();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        { 
            base.BuildContextualMenu(evt);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            if (onNodeViewSelected != null)
                onNodeViewSelected.Invoke(this);
        }

        public override void OnUnselected()
        {
            base.OnUnselected();
            if (onNodeViewDeselected != null)
                onNodeViewDeselected.Invoke();
        }
    }
}

