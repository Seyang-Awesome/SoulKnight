using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyEditor.BehaviourTree
{
    //打开BehaviorTree编辑器的窗口
    public class BehaviourTreeEditor : EditorWindow
    {
        //加载文件的path
        const string path = "Assets/0.Scripts/BehaviourTree/Resources/";
        public static BehaviourTreeEditor windowRoot;
        private static InspectorView inspectorView; //Inspector面板
        private static BehaviourTreeView btView; //BehaviorTree面板
        
        public static ObjectField fileSource;//行为树的文件源

        //打开窗口的路径
        [MenuItem("MyEditor/BehaviourTreeEditor")]
        public static void OpenBtEditor()
        {
            //得到这个窗口并打开（打开时会调用CreateGUI函数）
            BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
            //设置窗口的标题
            wnd.titleContent = new GUIContent("BehaviourWindowEditor");
        }

        //当打开文件的类型是BehaviourTreeConfig时调用
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId,int line)
        {
            if(Selection.activeObject is BehaviourTreeConfig)
            {
                OpenBtEditor();
                fileSource.value = Selection.activeObject;
                return true;
            }
            return false;
        }

        public void CreateGUI()
        {
            windowRoot = this;
            VisualElement root = rootVisualElement;

            //添加uxml文件和uss文件的引用
            var uxmlFile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{path}BehaviourTreeEditor.uxml");
            uxmlFile.CloneTree(root);
            var ussFile = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{path}BehaviourTreeEditor.uss");
            root.styleSheets.Add(ussFile);

            //查找相关引用
            inspectorView = root.Q<InspectorView>();
            btView = root.Q<BehaviourTreeView>();

            fileSource = root.Q<ObjectField>("FileSource");
            fileSource.objectType = typeof(BehaviourTreeConfig);
            fileSource.RegisterCallback<ChangeEvent<Object>>(LoadBtFile);

            //注册事件
            btView.onNodeViewSelected = OnNodeViewSelected;

            //当文件源有值时直接加载
            if (fileSource.value != null)
                LoadBtFile(fileSource.value as BehaviourTreeConfig);
        }

        public void OnNodeViewSelected(NodeView nodeView)
        {
            inspectorView.UpdateView(nodeView);
        }

        public void LoadBtFile(ChangeEvent<Object> evt)
        {
            if (evt.newValue == null) return;
            btView.PopulateView(evt.newValue as BehaviourTreeConfig);          
        }

        public void LoadBtFile(BehaviourTreeConfig config)
        {
            if (config != null) return;
            btView.PopulateView(config);
        }

        //public void SaveBtFile()
        //{

        //}
    }
}
