using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetName : MonoBehaviour {
	public static string userName = "Player";
    public static bool userNameCanChange = true;
    // Use this for initialization
    private void Start()
    {
        userNameCanChange = true;
    }
    public void SetPlayername(string _name)
	{
		userName = _name.ToUpper();
	}
}
