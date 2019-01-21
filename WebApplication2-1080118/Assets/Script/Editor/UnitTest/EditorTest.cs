using System.Runtime.CompilerServices;
using System.Timers;
using Assets.Script.Editor.Tools;
using Assets.Script.Test;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Script.Editor.UnitTest
{
    [ExecuteInEditMode]
    class EnterPoint
    {
        public static void Main()
        {            
            EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
            EditorApplication.isPlaying = true;
        }
    }
    
}
