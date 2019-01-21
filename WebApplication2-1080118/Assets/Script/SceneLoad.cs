using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Assets.Script
{
    [ExecuteInEditMode]
    static class SceneLoad
    {
        public static void Start()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
            EditorApplication.isPlaying = true;            
        }
    }
}
