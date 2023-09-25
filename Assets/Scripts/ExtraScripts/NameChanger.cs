using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR
public class NameChanger : EditorWindow
{
    public string SearchAndChange;
    public string ChangedName;
    Transform[] ChiledObjects;
    private List<Transform> TransformsList = new List<Transform>();
    private List<Transform> data = new List<Transform>();
    private List<string> ErrorLog = new List<string>();
    bool activateDraw = false;

    int changed = 0;

//transform 말고 gameobject로 수정할것


    [MenuItem("Custom Tools/이름 변환기(메인터넌스중)")]
    public static void ShowWindow()
    {
        GetWindow<NameChanger>("이름 변환기");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("로그가 너무 많으면 누르세요.");
        if (GUILayout.Button("로그 청소하기"))
        {
            ErrorLog.Clear();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("작업이 터지면 누르세요");
        if (GUILayout.Button("모든 작업 취소"))
        {
            Initialize();
            ErrorLog.Add("모든 작업을 취소했습니다.");
        }
        EditorGUILayout.EndHorizontal();


        GUILayout.Label("새로운 이름을 입력하세요.");
        SearchAndChange = EditorGUILayout.TextField("바꿀 이름", SearchAndChange);
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("-------------위에 문장을 아래 문장으로-----------------");
        EditorGUILayout.Space(20);
        ChangedName = EditorGUILayout.TextField("바뀔 이름", ChangedName);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("선택된 리스트 : ", EditorStyles.boldLabel);
        GUILayout.Label($"{changed}개", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();

        DrawTable(activateDraw);
        DrawLog(true);
        if (GUILayout.Button("선택 개체의 아이개체 불러오기"))
        {
            Initialize();
            if (Selection.activeGameObject == null)
            {
                ErrorLog.Add("개체가 선택되어있지 아니합니다. 선택 후 다시 실행 해 주세요.");
            }
            else
            {
                GameObject chosenObject = Selection.gameObjects[0];
                if (chosenObject == null)
                {
                    ErrorLog.Add("선택 안하셨습니다.");
                }
                else
                {
                    FindAllChiled(chosenObject.transform);
                }
                ErrorLog.Add("작업 완료");
                changed = TransformsList.Count;
            }
        }
/*        if(GUILayout.Button("선택 개체 확인"))
        {
            activateDraw = true;
        }*/


        if (GUILayout.Button("이름 바꾸기"))
        {
            changed = 0;
            foreach (var listedObject in TransformsList)
            {
                if (listedObject == null)
                {
                    continue;
                }
                else
                {
                    if(listedObject.name.Contains(SearchAndChange))
                    {
                        listedObject.name = listedObject.name.Replace(SearchAndChange, ChangedName);
                        changed++;
                    }
                }
            }
            GUILayout.Label($"{changed}개 개체의 {SearchAndChange} 이름을");
            GUILayout.Label($"{ChangedName} 로 변환하였습니다.");
        }




    }

    private void DrawTable(bool active)
    {
        int indexCount = 0;
        if (TransformsList != null && active)
        {
            ErrorLog.Add($"테이터 갯수 {TransformsList.Count}");

            // TransformsList.Count 대신 ChiledObjects.Length를 사용합니다.
            for (int i = 0; i < ChiledObjects.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < 4; j++)
                {
                    // indexCount가 TransformsList.Count보다 작은지 확인합니다.
                    if (indexCount < TransformsList.Count && TransformsList[indexCount] != null)
                    {
                        ChiledObjects[indexCount] = EditorGUILayout.ObjectField(TransformsList[indexCount], typeof(Transform), true) as Transform;
                        indexCount++;
                    }
                    else
                    {
                        activateDraw = false;
                        ErrorLog.Add("목록을 불러오는데 실패했습니다.");
                        Initialize();
                        indexCount++;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            ErrorLog.Add($"작업된 자식개체 {indexCount}");
            activateDraw = false;
        }
    }

    private void DrawLog(bool logGo)
    {
        if (ErrorLog != null && logGo)
        {
            for (int i = 0; i < ErrorLog.Count; i++)
            {
                EditorGUILayout.LabelField($"{ErrorLog[i]}");
            }
        }
    }

    private void Initialize()
    {
        EditorUtility.ClearProgressBar();
        SearchAndChange = "";
        ChangedName = "";
        changed = 0;
        ChiledObjects = new Transform[0];
        TransformsList.Clear();
        data.Clear(); // 데이터 리스트 초기화
        activateDraw = false;
    }

    void FindAllChiled(Transform parent)
    {
        foreach (Transform chiled in parent)
        {
            TransformsList.Add(chiled);
            FindAllChiled(chiled);
        }
    }
    private bool IscomplitCheck(Transform obj)
    {
        if (obj.childCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
#endif