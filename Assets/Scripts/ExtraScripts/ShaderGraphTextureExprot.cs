/*using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class ShaderGraphTextureExprot : EditorWindow
{
    public Material shader;
    public RenderTexture renderTexture;
    List<string> Logs = new List<string>();
    public GameObject obj;

    string BasePath = "Asset/Textur_Out";

    [MenuItem("Custom Tools / ���̴� �׷��� �ؽ��� �����")]
    public static void ShowWindow()
    {
        GetWindow<ShaderGraphTextureExprot>("�ؽ��� �����");
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("�۾� ���"))
        {
            Initialized();
        }
        EditorGUILayout.EndHorizontal();
        shader = EditorGUILayout.ObjectField(obj,typeof(Material),true)as Material;


    }

    private void Initialized()
    {

    }





}
#endif*/