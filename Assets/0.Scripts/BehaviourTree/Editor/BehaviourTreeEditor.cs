using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Seyang.BehaviourTree
{
    public class BehaviourTreeEditor : EditorWindow
    {
        const string path = "Assets/0.Scripts/BehaviourTree/Resources/";
        public static BehaviourTreeEditor windowRoot;
        private static InspectorView inspectorView; //Inspector���
        private static BehaviourTreeView btView; //BehaviorTree���
        
        public static ObjectField fileSource;//��Ϊ�����ļ�Դ
        
        [MenuItem("MyEditor/BehaviourTreeEditor")]
        public static void OpenBtEditor()
        {
            BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviourWindowEditor");
        }
        
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
            
            var uxmlFile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{path}BehaviourTreeEditor.uxml");
            uxmlFile.CloneTree(root);
            var ussFile = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{path}BehaviourTreeEditor.uss");
            root.styleSheets.Add(ussFile);
            
            inspectorView = root.Q<InspectorView>();
            btView = root.Q<BehaviourTreeView>();

            fileSource = root.Q<ObjectField>("FileSource");
            fileSource.objectType = typeof(BehaviourTreeConfig);
            fileSource.RegisterCallback<ChangeEvent<Object>>(LoadBtFile);

            btView.onNodeViewSelected = OnNodeViewSelected;
            btView.onNodeViewDeselected = OnNodeViewDeselected;
            
            if (fileSource.value != null)
                LoadBtFile(fileSource.value as BehaviourTreeConfig);
        }

        private void OnDestroy()
        {
            BehaviourTreeConfig btConfig = fileSource.value as BehaviourTreeConfig;;
            if (btConfig == null) return;
            
            btConfig.nodes.ForEach(EditorUtility.SetDirty);
            AssetDatabase.SaveAssets();
        }

        public void OnNodeViewSelected(NodeView nodeView)
        {
            inspectorView.UpdateView(nodeView);
        }

        public void OnNodeViewDeselected()
        {
            inspectorView.UpdateView(null);
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

        
    }
}
