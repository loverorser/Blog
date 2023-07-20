
using UnityEditor;
using UnityEngine;

public class CustomEditorWindow : EditorWindow
{
    string exePath = "";
    string inputPath = "";
    string outputPath = "";
    [MenuItem("My Tools/Custom Window")]
    public static void ShowWindow()
    {
        // ����һ������ʵ��
        CustomEditorWindow window = EditorWindow.GetWindow<CustomEditorWindow>("Custom Window");
        window.Show();
    }

    // ������ʵ������Ҫ��GUIԪ��
    private void OnGUI()
    {
        GUILayout.Label("����һ���Զ��崰�ڹ��߷���ʾ��", EditorStyles.boldLabel);
        GUILayout.Label("��ǰ�������·����"+exePath);
        if (GUILayout.Button("�����"))
        {
            Debug.Log("�������Զ��尴ť��");
        }

        if (GUILayout.Button("ѡ�񵼱����"))
        {
            // ���ļ�ѡ��Ի��򣬲���ȡ�û�ѡ����ļ�·��
            string selectedFilePath = EditorUtility.OpenFilePanel("ѡ�񵼱����", "", "exe");

            // ȷ���û���ѡ���ļ�
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                exePath = selectedFilePath;
                EditorPrefs.SetString("ExePath", selectedFilePath);
            }
        }

        GUILayout.Label("���ļ���·��:"+inputPath);
        if (GUILayout.Button("ѡ����ļ���·��"))
        {
            var tmp = EditorUtility.OpenFolderPanel("ѡ���·��", "", "");
            if(!string.IsNullOrEmpty(tmp))
            {
                inputPath =tmp;
                EditorPrefs.SetString("InputPath", inputPath);
            }
            
        }

        GUILayout.Label("�����ļ���·��:"+outputPath);
        if (GUILayout.Button("ѡ������ļ���·��"))
        {
            var tmp = EditorUtility.OpenFolderPanel("ѡ���·��", "", "");
            if (!string.IsNullOrEmpty(tmp))
            {
                outputPath = tmp;
                EditorPrefs.SetString("OutputPath", outputPath);
            }
            
        }
        if (GUILayout.Button("Go", GUILayout.Height(100), GUILayout.Width(120))) {

            System.Diagnostics.Process.Start(exePath,inputPath+" "+outputPath);
        }
    }
    private void OnEnable()
    {
        // �ڱ༭����ʱ��EditorPrefs�л�ȡ�洢���ļ�·��
        exePath = EditorPrefs.GetString("ExePath", "");
        inputPath= EditorPrefs.GetString("InputPath", "");
        outputPath= EditorPrefs.GetString("OutputPath", "");
    }
}