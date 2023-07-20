
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
        // 创建一个窗口实例
        CustomEditorWindow window = EditorWindow.GetWindow<CustomEditorWindow>("Custom Window");
        window.Show();
    }

    // 在这里实现你想要的GUI元素
    private void OnGUI()
    {
        GUILayout.Label("这是一个自定义窗口工具方法示例", EditorStyles.boldLabel);
        GUILayout.Label("当前导表程序路径："+exePath);
        if (GUILayout.Button("点击我"))
        {
            Debug.Log("你点击了自定义按钮！");
        }

        if (GUILayout.Button("选择导表程序"))
        {
            // 打开文件选择对话框，并获取用户选择的文件路径
            string selectedFilePath = EditorUtility.OpenFilePanel("选择导表程序", "", "exe");

            // 确保用户有选择文件
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                exePath = selectedFilePath;
                EditorPrefs.SetString("ExePath", selectedFilePath);
            }
        }

        GUILayout.Label("表文件夹路径:"+inputPath);
        if (GUILayout.Button("选择表文件夹路径"))
        {
            var tmp = EditorUtility.OpenFolderPanel("选择表路径", "", "");
            if(!string.IsNullOrEmpty(tmp))
            {
                inputPath =tmp;
                EditorPrefs.SetString("InputPath", inputPath);
            }
            
        }

        GUILayout.Label("代码文件夹路径:"+outputPath);
        if (GUILayout.Button("选择代码文件夹路径"))
        {
            var tmp = EditorUtility.OpenFolderPanel("选择表路径", "", "");
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
        // 在编辑器打开时从EditorPrefs中获取存储的文件路径
        exePath = EditorPrefs.GetString("ExePath", "");
        inputPath= EditorPrefs.GetString("InputPath", "");
        outputPath= EditorPrefs.GetString("OutputPath", "");
    }
}