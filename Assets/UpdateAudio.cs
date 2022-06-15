using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAudio : MonoBehaviour {
	[SerializeField] Slider audioSlider;
    bool ignoreThisChange;
    void Awake()
    {
        audioSlider.value = StatsHolder.audioValue;
    }

    public void updateSliderGraphic()
    {
      
        ignoreThisChange = true;
        audioSlider.value = StatsHolder.audioValue;
    }
    public void SetAudioValue()
    {
        Debug.Log("Modified");
        StatsHolder.audioValue = audioSlider.value;
        AudioListener.volume = StatsHolder.audioValue / 10;
    }
    public void increaseAudio(){
		if (StatsHolder.audioValue < 10) {
			StatsHolder.audioValue++;
		//	audioText.text = "" + StatsHolder.audioValue;
			AudioListener.volume = StatsHolder.audioValue/10;
		}
	}
	public void decreaseAudio(){
		if (StatsHolder.audioValue > 0) {
			StatsHolder.audioValue--;
			//audioText.text = "" + StatsHolder.audioValue;
			AudioListener.volume = StatsHolder.audioValue/10;
		}
	}
}
