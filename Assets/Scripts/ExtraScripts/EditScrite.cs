using UnityEngine;
using UnityEditor;

public class AddVariableEditor : EditorWindow
{
    private string variableName = "";
    private string variableType = "int"; // 변수 타입의 기본값은 int로 설정됩니다.
    private MonoScript targetScript;

    [MenuItem("Custom Tools/스크립트 조정기(Beta)")]
    public static void ShowWindow()
    {
        GetWindow<AddVariableEditor>("Add Variable");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Add Variable to Script", EditorStyles.boldLabel);

        variableName = EditorGUILayout.TextField("Variable Name", variableName);
        variableType = EditorGUILayout.TextField("Variable Type", variableType);

        targetScript = EditorGUILayout.ObjectField("Target Script", targetScript, typeof(MonoScript), false) as MonoScript;

        if (GUILayout.Button("Add Variable"))
        {
            if (targetScript != null && !string.IsNullOrEmpty(variableName))
            {
                // 변수를 스크립트에 추가하는 작업을 수행합니다.
                AddVariableToScript(targetScript, variableName, variableType);
            }
            else
            {
                Debug.LogError("Target Script and Variable Name must be specified.");
            }
        }
    }

    private void AddVariableToScript(MonoScript script, string varName, string varType)
    {
        string scriptPath = AssetDatabase.GetAssetPath(script);
        string scriptCode = System.IO.File.ReadAllText(scriptPath);

        // 새로운 변수 선언을 스크립트에 추가합니다.
        string newVariable = $"public {varType} {varName};";
        scriptCode = scriptCode.Insert(scriptCode.Length - 2, "\n    " + newVariable + "\n");

        System.IO.File.WriteAllText(scriptPath, scriptCode);

        AssetDatabase.Refresh();
    }
}
