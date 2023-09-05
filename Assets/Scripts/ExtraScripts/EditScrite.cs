using UnityEngine;
using UnityEditor;

public class AddVariableEditor : EditorWindow
{
    private string variableName = "";
    private string variableType = "int"; // ���� Ÿ���� �⺻���� int�� �����˴ϴ�.
    private MonoScript targetScript;

    [MenuItem("Custom Tools/��ũ��Ʈ ������(Beta)")]
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
                // ������ ��ũ��Ʈ�� �߰��ϴ� �۾��� �����մϴ�.
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

        // ���ο� ���� ������ ��ũ��Ʈ�� �߰��մϴ�.
        string newVariable = $"public {varType} {varName};";
        scriptCode = scriptCode.Insert(scriptCode.Length - 2, "\n    " + newVariable + "\n");

        System.IO.File.WriteAllText(scriptPath, scriptCode);

        AssetDatabase.Refresh();
    }
}
