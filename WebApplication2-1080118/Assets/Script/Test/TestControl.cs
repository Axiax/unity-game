using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.Test
{
    public class TestControl:MonoBehaviour
    {
        public void Start()
        {
            StartCoroutine(PrintPos(3f));
        }

        public void Update()
        {
            //Debug.Log(Time.time);
            if (Time.time > 0.3)
            {
                //EditorApplication.Exit(0);
            }
        }
        private IEnumerator PrintPos(float time)
        {
            //GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            GameObject[] allObjects = UnityEngine.GameObject.FindGameObjectsWithTag("AI");
            foreach (GameObject go in allObjects)
            {
                print("Now Time is " + Time.time + " and " + go.name + "`s position is " + go.transform.position + " and faced " + go.transform.eulerAngles);
            }
            yield return new WaitForSeconds(time);
        }
    }
}