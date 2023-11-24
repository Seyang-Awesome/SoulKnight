using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace MyEditor.BehaviourTree
{
    //BehaviourTreeEditor视图中的行为树编辑视图部分
    public class BehaviourTreeView : GraphView
    {
        const string path = "Assets/0.Scripts/BehaviourTree/Resources/";
        public BehaviourTreeConfig bt;

        public Action<NodeView> onNodeViewSelected;
        public Action onNodeViewDeselected;

        //必须覆写这个类，才能在UIBuilder里添加这个自定义VisualElement
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits> { }
        


        public BehaviourTreeView() 
        {
            //插入网格背景，背景的参数在uss文件中
            Insert(0,new GridBackground());

            //允许使用者在GraphView里面使用一些操作,如缩放，拖拽，矩形选择等
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new SelectionDropper());
            this.AddManipulator(new RectangleSelector());

            var ussFile = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{path}BehaviourTreeEditor.uss");
            styleSheets.Add(ussFile);

            //创建一个SearchTreeMenu的实例
            var menuWindowProvider = ScriptableObject.CreateInstance<SearchTreeMenu>();

            //给实例的委托注册方法,当点击选项的时候调用
            menuWindowProvider.onSelectEntryHander = (SearchTreeEntry searchTreeEntry, SearchWindowContext context) =>
            {
                var windowRoot = BehaviourTreeEditor.windowRoot.rootVisualElement;
                var windowMousePos = windowRoot.ChangeCoordinatesTo(windowRoot.parent,
                    context.screenMousePosition - BehaviourTreeEditor.windowRoot.position.position);
                var graphMousePos = contentContainer.WorldToLocal(windowMousePos);

                CreateNode((Type)searchTreeEntry.userData, graphMousePos);
                return true;
            };

            //nodeCreationRequest就是指选择Create Node选项时要执行的委托
            nodeCreationRequest += context =>
            {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), menuWindowProvider);
            };

        }

        /// <summary>
        /// 当你在Graph视图中右击鼠标将会弹出来的选项窗口
        /// </summary>
        /// <param name="evt"></param>
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort => 
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node
                ).ToList();
        }

        /// <summary>
        /// 遍历这个行为树，并将相关的节点生成到GraphView视图上
        /// </summary>
        public void PopulateView(BehaviourTreeConfig bt)
        {
            this.bt = bt;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            if (bt == null) return;

            if (bt.rootNode == null)
            {
                bt.rootNode = bt.CreateNode(typeof(RootNode));;
                EditorUtility.SetDirty(bt);
                AssetDatabase.SaveAssets();
            }

            bt.nodes.ForEach(n => CreateNodeView(n,n.posInView));

            bt.nodes.ForEach(n => {
                List<NodeBase> children = bt.GetChildren(n);
                children.ForEach(c => {
                    NodeView parentNodeView = FindNodeView(n);
                    NodeView childNodeView = FindNodeView(c);

                    Edge edge = parentNodeView.output.ConnectTo(childNodeView.input);
                    AddElement(edge);
                });
                
            });
        }

        NodeView FindNodeView(NodeBase node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }


        private GraphViewChange OnGraphViewChanged(GraphViewChange info)
        {
            if (info.elementsToRemove != null)
            {
                info.elementsToRemove.ForEach((element) => { 
                    if(element is NodeView)
                    {
                        if (element != null)
                        {
                            NodeBase node = (element as NodeView).node;
                            bt.DeleteNode(node);
                            
                            AssetDatabase.RemoveObjectFromAsset(node);
                            AssetDatabase.SaveAssets();
                        }
                    }
                    
                    if(element is Edge)
                    {
                        if (element != null)
                        {
                            Edge edge = element as Edge;
                            NodeView parent = edge.output.node as NodeView;
                            NodeView child = edge.input.node as NodeView;
                            bt.RemoveChild(parent.node, child.node); 
                        }
                    }
                });
            }

            if(info.edgesToCreate != null)
            {
                info.edgesToCreate.ForEach(element => {
                    if (element != null)
                    {
                        NodeView parent = element.output.node as NodeView;
                        NodeView child = element.input.node as NodeView;
                        bt.AddChild(parent.node,child.node);
                    }
                });
            }
            return info;
        }

        public NodeBase CreateNode(Type type,Vector2 pos)
        {
            if (bt == null)
            {
                //TODO：消息提示没检测到文件来源，询问是否要创建新文件
                return null;
            }
            NodeBase node = bt.CreateNode(type);
            CreateNodeView(node,pos);
            
            AssetDatabase.AddObjectToAsset(node, bt);
            EditorUtility.SetDirty(node);
            AssetDatabase.SaveAssets();

            return node;
        }
        
        /// <summary>
        /// 创建一个节点视图到GraphView视图上
        /// </summary>
        public void CreateNodeView(NodeBase node, Vector2 pos = new Vector2())
        {
            if (node == null) return;
            NodeView nodeView = new NodeView(node);
            nodeView.onNodeViewSelected = onNodeViewSelected;
            nodeView.onNodeViewDeselected = onNodeViewDeselected;
            nodeView.SetPosition(new Rect(pos,Vector2.one));
            AddElement(nodeView);
        }

        public void SetRootNode(NodeBase node)
        {
            bt.rootNode = node;
        }
    }

    //下面介绍这个类的细节（自己理解的）
    //这个类管控在网格视图中创建节点时的上下文视图
    //SearchTreeMenu继承于ScriptableObject和ISearchWindowProvider：SearchWindow.Open时需要一个继承于ScriptableObject和实现ISearchWindowProvider的类作为参数  
    //ISearchWindowProvider：继承该接口的类可以在网格图中生成相关的搜索视图
    public class SearchTreeMenu : ScriptableObject, ISearchWindowProvider//（RightClickMenu）
    {
        public delegate bool SelectEntryDelegate(SearchTreeEntry searchTreeEntry, SearchWindowContext context);
        public SelectEntryDelegate onSelectEntryHander;

        /// <summary>
        /// 填充搜索树的数据，SearchWindow.Open时将调用这个函数来创建相关的选项
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var entries = new List<SearchTreeEntry>();
            entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
            entries = AddNodeType<CompositeNode>(entries, "CompositeNode");
            entries = AddNodeType<DecoratorNode>(entries, "DecoratorNode");
            entries = AddNodeType<ActionNode>(entries, "ActionNode");

            return entries;
        }

        /// <summary>
        /// OnSelectEntry：当点击了搜索树当中的一个选项时要发生的事情
        /// </summary>
        /// <param name="SearchTreeEntry">点击的选项</param>
        /// <param name="context">点击了该选项后产生的上下文菜单</param>
        /// <returns></returns>
        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            return onSelectEntryHander(SearchTreeEntry, context);
        }

        /// <summary>
        /// 添加T类型节点选项（设置层级为1），再在该节点选项后添加所有继承于该节点的所有类型（设置层级为2）
        /// </summary>
        /// <typeparam name="T">添加的节点类型</typeparam>
        /// <param name="entries">当前已添加的节点类型列表</param>
        /// <param name="nodeTypeName">节点类型名称</param>
        /// <returns></returns>
        public List<SearchTreeEntry> AddNodeType<T>(List<SearchTreeEntry> entries, string nodeTypeName) where T : NodeBase
        {
            //添加搜索树的选项组，名称为nodeTypeName（层级为1）
            entries.Add(new SearchTreeGroupEntry(new GUIContent(nodeTypeName)) { level = 1 });
            List<Type> rootNodeTypes = typeof(T).GetDerivedClasses();

            foreach (var rootType in rootNodeTypes)
            {
                //将二级选项的名称命名为该类型的类名，在搜索树中添加该类别选项（层级为2）
                string entryName = rootType.Name;
                entries.Add(new SearchTreeEntry(new GUIContent(entryName)) { level = 2, userData = rootType });
            }

            return entries;
        }
    }
}