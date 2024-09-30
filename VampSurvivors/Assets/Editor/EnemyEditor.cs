using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//Thank you Yongqin Yu for allowing me to use this code

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        EnemyUnit obj = EditorUtility.InstanceIDToObject(instanceId) as EnemyUnit;

        if(obj != null )
        {
            return true;
        }

        return false;
    }
}

public class EnemyEditor : EditorWindow
{
    [MenuItem("Tools/Enemy/EnemyEditor")]
    public static void ShowExample()
    {
        EnemyEditor wnd = GetWindow<EnemyEditor>();
        wnd.titleContent = new GUIContent("EnemyEditor");
    }

    public void CreateGUI()
    {
        var enemy_data = AssetDatabase.FindAssets("t:EnemyUnit");
        var enemy_data_instances = new List<EnemyUnit>();
        foreach(var guid in enemy_data)
        {
            enemy_data_instances.Add(AssetDatabase.LoadAssetAtPath<EnemyUnit>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        var split = new TwoPaneSplitView(0, 100, TwoPaneSplitViewOrientation.Horizontal);
        root.Add(split);


        var left = new ListView();
        left.makeItem = () => new Label();
        left.bindItem = (item, index) =>
        {
            (item as Label).text = enemy_data_instances[index].name;
        };
        left.itemsSource = enemy_data_instances;
        left.selectionChanged += OnDataSelectionChange;
        split.Add(left);

        m_right_panel = new VisualElement();
        split.Add(m_right_panel);
    }


    private VisualElement m_right_panel;


    private void OnDataSelectionChange(IEnumerable<object> data)
    {
        m_right_panel.Clear();

        var enumerator = data.GetEnumerator();
        if (enumerator.MoveNext())
        {
            var next_data = enumerator.Current as EnemyUnit;
            SerializedObject obj = new SerializedObject(next_data);
            m_right_panel.Add(new PropertyField(obj.FindProperty("name")));
            m_right_panel.Add(new PropertyField(obj.FindProperty("health")));
            m_right_panel.Add(new PropertyField(obj.FindProperty("damage")));
            m_right_panel.Add(new PropertyField(obj.FindProperty("speed")));
            m_right_panel.Add(new PropertyField(obj.FindProperty("canShoot")));
            m_right_panel.Add(new PropertyField(obj.FindProperty("sprite")));
            m_right_panel.Bind(obj);
        }
    }
}
