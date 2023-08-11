using UnityEngine;
using UnityEditor;

public class Tools : MonoBehaviour
{
    [MenuItem("Tools/Clear Prefs")]
    public static 
        void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
