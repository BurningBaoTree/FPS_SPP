using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NameChanger : EditorWindow
{
    public string SearchAndChange;
    public string ChangedName;
    Transform[] ChiledObjects;
    Transform[] ListObject;
    private List<Transform> data = new List<Transform>();
    bool activateDraw = false;


    [MenuItem("Custom Tools/이름 변환기")]
    public static void ShowWindow()
    {
        GetWindow<NameChanger>("이름 변환기");
    }

    private void OnGUI()
    {
        GUILayout.Label("새로운 이름을 입력하세요.");
        SearchAndChange = EditorGUILayout.TextField("바꿀 이름", SearchAndChange);
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("-------------위에 문장을 아래 문장으로-----------------");
        EditorGUILayout.Space(20);
        ChangedName = EditorGUILayout.TextField("바뀔 이름", SearchAndChange);

        GUILayout.Label("3xN Table", EditorStyles.boldLabel);
        DrawTable(activateDraw);



        if (GUILayout.Button("선택 개체의 아이개체 불러오기"))
        {
            if (Selection.activeGameObject == null)
            {
                Debug.LogWarning("개체가 선택되어있지 아니합니다. 선택 후 다시 실행 해 주세요.");
            }
            else
            {
                GameObject chosenObject = Selection.gameObjects[0];
                ChiledObjects = new Transform[chosenObject.transform.childCount];
                foreach (var chos in ChiledObjects)
                {
                    data.Add(chos);
                }
            }
            activateDraw = true;
        }


        if (GUILayout.Button("이름 바꾸기"))
        {

        }


        if (GUILayout.Button("모든 작업 취소"))
        {
            Initialize();
        }
    }

    private void DrawTable(bool active)
    {
        if (data != null && active)
        {
            ListObject = new Transform[data.Count];
            int indexCount = 0;
            for (int i = 0; i < data.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < 3; j++)
                {
                    if (data[indexCount] != null)
                    {
                        ListObject[indexCount] = EditorGUILayout.ObjectField(data[indexCount], typeof(Transform), true) as Transform;
                        indexCount++;
                    }
                    else
                    {
                        EditorGUILayout.TextArea(null); // 데이터가 없을 경우 빈 텍스트 필드 표시
                    }

                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
    private void Initialize()
    {
        SearchAndChange = "";
        ChangedName = "";
        ChiledObjects = new Transform[0];
        data.Clear(); // 데이터 리스트 초기화
        activateDraw = false;
    }

}
