using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace MyEditor.BehaviourTree
{
    //��BehaviorTree�༭���Ĵ���
    public class BehaviourTreeEditor : EditorWindow
    {
        //�����ļ���path
        const string path = "Assets/0.Scripts/BehaviourTree/Resources/";
        public static BehaviourTreeEditor windowRoot;
        private static InspectorView inspectorView; //Inspector���
        private static BehaviourTreeView btView; //BehaviorTree���
        
        public static ObjectField fileSource;//��Ϊ�����ļ�Դ

        //�򿪴��ڵ�·��
        [MenuItem("MyEditor/BehaviourTreeEditor")]
        public static void OpenBtEditor()
        {
            //�õ�������ڲ��򿪣���ʱ�����CreateGUI������
            BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
            //���ô��ڵı���
            wnd.titleContent = new GUIContent("BehaviourWindowEditor");
        }

        //�����ļ���������BehaviourTreeConfigʱ����
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

            //���uxml�ļ���uss�ļ�������
            var uxmlFile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{path}BehaviourTreeEditor.uxml");
            uxmlFile.CloneTree(root);
            var ussFile = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{path}BehaviourTreeEditor.uss");
            root.styleSheets.Add(ussFile);

            //�����������
            inspectorView = root.Q<InspectorView>();
            btView = root.Q<BehaviourTreeView>();

            fileSource = root.Q<ObjectField>("FileSource");
            fileSource.objectType = typeof(BehaviourTreeConfig);
            fileSource.RegisterCallback<ChangeEvent<Object>>(LoadBtFile);

            //ע���¼�
            btView.onNodeViewSelected = OnNodeViewSelected;
            btView.onNodeViewDeselected = OnNodeViewDeselected;

            //���ļ�Դ��ֵʱֱ�Ӽ���
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
