using UnityEngine;
using System.Runtime.InteropServices;

public class PageTool : MonoBehaviour
{
    void Update()
    {
        if (GameInputManager.GetKeyDown("Fullscreen"))
        {
            #if !UNITY_EDITOR
            GoFullscreen();
            #endif
        }
    }
    [DllImport("__Internal")]
    private static extern void GoFullscreen();
}
