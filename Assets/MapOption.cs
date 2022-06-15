using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapOption : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] Text modeText;
    [SerializeField] Text mapText;
    [SerializeField] TMP_Text voteCount;
    [SerializeField] Image backdropImage;
    [SerializeField] TMP_Text names;

    public void setIcon(Sprite icon)
    {
        iconImage.sprite = icon;
    }
    public void setMode(string mode)
    {
        modeText.text = mode;
    }
    public void setMap(string map)
    {
        mapText.text = map;
    }
    public void setVote(int vote)
    {
        voteCount.text = "" + vote;
    }
    public void setBackdrop(Sprite backdrop)
    {
        backdropImage.sprite = backdrop;
    }
    public void setNames(string userNames)
    {
        names.text = userNames;
    }
}
