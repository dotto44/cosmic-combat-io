using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class counterScript : MonoBehaviour {
	public Text textComponent;
	public void setTime5(){
		textComponent.text = "5";
	}
	public void setTime4(){
		textComponent.text = "4";
	}
	public void setTime3(){
		textComponent.text = "3";
	}
	public void setTime2(){
		textComponent.text = "2";
	}
	public void setTime1(){
		textComponent.text = "1";
	}
    public void setTime(int time)
    {
		textComponent.text = "" + time; 
	}
}
