using UnityEditor;
using UnityEngine;
using System.IO;

public class BasicMapStructureTool : EditorWindow
{
    private Vector2 scroll;

    //basic structure

    private const string Assets = "Assets";
    private const string Editor = "Editor";
    private const string Scripts = "Scripts";
    private const string Prefabs = "Prefabs";
    private const string Materials = "Materials";
    private const string Sprites = "Sprites";
    private const string Models3D = "3D Models";
    private const string Animations = "Animations";

    public static bool EditorMap;
    public static bool ScriptsMap;
    public static bool PrefabsMap;
    public static bool MaterialsMap;
    public static bool SpritesMap;
    public static bool Models3DMap;
    public static bool AnimationsMap;

    //3d model sub folders

    private const string ModelFBX = "Model FBX Files";
    private const string ModelTextureAndMaterial = "Model Textures and Material";

    public static bool ModelFBXMap;
    public static bool ModelTextureAndMaterialMap;

    //animations sub folders

    private const string AnimatorControllers = "AnimatorControllers";
    private const string Animation = "Animation";

    public static bool AnimatorControllersMap;
    public static bool AnimationMap;

    private void OnEnable()
    {
        scroll = new Vector2(0, 0);
    }

    [MenuItem("Assets/Create/Basic Folder layout test", priority = 1)]
    public static void PopUp()
    {
        BasicMapStructureTool window = GetWindow<BasicMapStructureTool>();
        window.minSize = new Vector2(400, 600);
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.HelpBox("Choose Which Folders You Want and press \"Create Basic Map Structure\" when you selected all folders u want ", MessageType.Info, true);

        EditorGUILayout.BeginScrollView(scroll);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Select All"))
        {
            EditorMap = true;
            ScriptsMap = true;
            PrefabsMap = true;
            MaterialsMap = true;
            SpritesMap = true;
            Models3DMap = true;
            AnimationsMap = true;

            ModelFBXMap = true;
            ModelTextureAndMaterialMap = true;

            AnimatorControllersMap = true;
            AnimationMap = true;
        }
        else if (GUILayout.Button("Deselect All"))
        {
            EditorMap = false;
            ScriptsMap = false;
            PrefabsMap = false;
            MaterialsMap = false;
            SpritesMap = false;
            Models3DMap = false;
            AnimationsMap = false;

            ModelFBXMap = false;
            ModelTextureAndMaterialMap = false;

            AnimatorControllersMap = false;
            AnimationMap = false;
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorMap = EditorGUILayout.ToggleLeft(Editor, EditorMap);
        EditorGUILayout.Space();

        ScriptsMap = EditorGUILayout.ToggleLeft(Scripts, ScriptsMap);
        EditorGUILayout.Space();

        PrefabsMap = EditorGUILayout.ToggleLeft(Prefabs, PrefabsMap);
        EditorGUILayout.Space();

        MaterialsMap = EditorGUILayout.ToggleLeft(Materials, MaterialsMap);
        EditorGUILayout.Space();

        SpritesMap = EditorGUILayout.ToggleLeft(Sprites, SpritesMap);
        EditorGUILayout.Space();

        Models3DMap = EditorGUILayout.ToggleLeft(Models3D, Models3DMap);
        if (Models3DMap)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select All Subfolders"))
            {
                ModelFBXMap = true;
                ModelTextureAndMaterialMap = true;
            }
            if (GUILayout.Button("Deselect All Subfolders"))
            {
                ModelFBXMap = false;
                ModelTextureAndMaterialMap = false;
            }
            EditorGUILayout.EndHorizontal();

            ModelFBXMap = EditorGUILayout.ToggleLeft(ModelFBX, ModelFBXMap);
            ModelTextureAndMaterialMap = EditorGUILayout.ToggleLeft(ModelTextureAndMaterial, ModelTextureAndMaterialMap);

            EditorGUI.indentLevel--;
        }
        else
        {
            ModelFBXMap = false;
            ModelTextureAndMaterialMap = false;
        }

        EditorGUILayout.Space();

        AnimationsMap = EditorGUILayout.ToggleLeft(Animations, AnimationsMap);
        if (AnimationsMap)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select All Subfolders"))
            {
                AnimationMap = true;
                AnimatorControllersMap = true;
            }
            if (GUILayout.Button("Deselect All Subfolders"))
            {
                AnimationMap = true;
                AnimatorControllersMap = true;
            }
            EditorGUILayout.EndHorizontal();

            AnimatorControllersMap = EditorGUILayout.ToggleLeft(AnimatorControllers, AnimatorControllersMap);
            AnimationMap = EditorGUILayout.ToggleLeft(Animation, AnimationMap);

            EditorGUI.indentLevel--;
        }
        else
        {
            AnimatorControllersMap = false;
            AnimationMap = false;
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Basic Map Structure"))
        {
            if (EditorMap || ScriptsMap || PrefabsMap || MaterialsMap || SpritesMap || Models3DMap || AnimationsMap || ModelFBXMap || ModelTextureAndMaterialMap || AnimatorControllersMap || AnimationMap)
            {
                CreateBasicUnityFolderLayout();
            }
            else
            {
                Debug.LogWarning("No folders were made because nothing was selected");
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private static void CreateBasicUnityFolderLayout()
    {
        //basic layout

        if (EditorMap) MakeFolder(Assets, Editor);
        if (ScriptsMap) MakeFolder(Assets, Scripts);
        if (PrefabsMap) MakeFolder(Assets, Prefabs);
        if (MaterialsMap) MakeFolder(Assets, Materials);
        if (SpritesMap) MakeFolder(Assets, Sprites);
        if (Models3DMap) MakeFolder(Assets, Models3D);
        if (AnimationsMap) MakeFolder(Assets, Animations);

        //SubFolders
        //sub 3D
        if (ModelFBXMap) MakeFolder($"{Assets}/{Models3D}", ModelFBX);
        if (ModelTextureAndMaterialMap) MakeFolder($"{Assets}/{Models3D}", ModelTextureAndMaterial);

        //sub animation
        if (AnimationMap) MakeFolder($"{Assets}/{Animations}", Animation);
        if (AnimatorControllersMap) MakeFolder($"{Assets}/{Animations}", AnimatorControllers);
    }

    private static void MakeFolder(string baseMap, string mapName)
    {
        if (!Directory.Exists($"{baseMap}/{mapName}"))
        {
            AssetDatabase.CreateFolder($"{baseMap}", $"{mapName}");

            File.Create($"{baseMap}/{mapName}/.gitkeep");
            Debug.Log($"made \"{mapName}\" folder");
        }
        else
        {
            Debug.Log($"{mapName} folder already exited so no new one was created");
        }
    }
}