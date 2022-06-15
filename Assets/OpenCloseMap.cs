using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseMap : MonoBehaviour {
	
	[SerializeField] ShopBar map;
    [SerializeField] ShopBar mapKey;
    [SerializeField] GameObject bigMapText;
	private bool mapUp = false;

	void Update () 
	{
		if (GameInputManager.GetKeyDown ("Map") ) 
		{
            map.setVisibleAndHidden();
            mapKey.setVisibleAndHidden();

            if (!mapUp) 
			{
               //bigMap.
               //bigMap.SetActive (true);
				//bigMapText.SetActive (true);
				//mapUp = true;
			} 
			else 
			{
				//bigMap.SetActive (false);
				//bigMapText.SetActive (false);
				//mapUp = false;
			}
		}
	}

}
