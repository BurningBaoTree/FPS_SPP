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

    [MenuItem("Custom Tools / 쉐이더 그래프 텍스쳐 추출기")]
    public static void ShowWindow()
    {
        GetWindow<ShaderGraphTextureExprot>("텍스쳐 추출기");
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("작업 취소"))
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