using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class LinkOpener : MonoBehaviour
{
	public void OpenLinkJSPlugin(string link)
	{
        #if !UNITY_EDITOR
		openWindow(link);
        #endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);
}