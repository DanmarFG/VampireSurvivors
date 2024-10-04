using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateEnemyEditor : EditorWindow
{
    public new string name = "Unit";
    public float health = 0, damage = 0, speed = 0;
    public bool canShoot = false;

    public Sprite sprite;

    public MonoScript customBehaviourToAdd;

    [MenuItem("Tools/Enemy/CreateEnemy")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateEnemyEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create new Enemy", EditorStyles.boldLabel);

        name = EditorGUILayout.TextField("Enemy name",name);
        health = EditorGUILayout.FloatField("Health", health);
        damage = EditorGUILayout.FloatField("Damage", damage);
        speed = EditorGUILayout.FloatField("Speed", speed);
        canShoot = EditorGUILayout.Toggle("Can shoot", canShoot);
        sprite = EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Sprite;

        EditorGUILayout.BeginVertical();
        GUILayout.Label("Add a custom behaviour", EditorStyles.boldLabel);
        customBehaviourToAdd = (MonoScript)EditorGUILayout.ObjectField("Leave empty if no",customBehaviourToAdd, typeof(MonoScript), true);
        EditorGUILayout.EndVertical();  
        if (GUILayout.Button("Create ScriptableObject"))
        {
            CreateEnemySo();
        }

        if (GUILayout.Button("Create Prefab"))
        {
            CreatePrefab();
        }
    }

    void CreateEnemySo()
    {
        EnemyUnit UnitStatsToCreate = CreateInstance<EnemyUnit>();
        UnitStatsToCreate.name = name;
        UnitStatsToCreate.health = health;
        UnitStatsToCreate.damage = damage;
        UnitStatsToCreate.speed = speed;
        UnitStatsToCreate.canShoot = canShoot;
        UnitStatsToCreate.sprite = sprite;
        AssetDatabase.CreateAsset(UnitStatsToCreate, $"Assets/Enemies/EnemySO/{name}.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = UnitStatsToCreate;
    }

    void CreatePrefab()
    {
        EnemyUnit UnitStatsToCreate = CreateInstance<EnemyUnit>();
        UnitStatsToCreate.name = name;
        UnitStatsToCreate.health = health;
        UnitStatsToCreate.damage = damage;
        UnitStatsToCreate.speed = speed;
        UnitStatsToCreate.canShoot = canShoot;
        UnitStatsToCreate.sprite = sprite;
        AssetDatabase.CreateAsset(UnitStatsToCreate, $"Assets/Enemies/EnemySO/{name}.asset");
        AssetDatabase.SaveAssets();

        GameObject obj = (GameObject)AssetDatabase.LoadMainAssetAtPath("Assets/Enemies/EnemiesPF/EnemyPF.prefab");
        GameObject instanceRoot = (GameObject)PrefabUtility.InstantiatePrefab(obj);
        GameObject pVariant = PrefabUtility.SaveAsPrefabAsset(instanceRoot, $"Assets/Enemies/EnemiesPF/{name}.prefab");
        GameObject.DestroyImmediate(instanceRoot);
        pVariant.GetComponent<Unit>().EnemyUnit = UnitStatsToCreate;

        if(customBehaviourToAdd)
            pVariant.AddComponent(customBehaviourToAdd.GetClass());

        Selection.activeObject = pVariant;
    }
}
