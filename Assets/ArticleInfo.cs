using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArticleInfo : MonoBehaviour
{
    [SerializeField] private Sprite expImage;
    [SerializeField] private string category;
    [SerializeField] private string title;
    [SerializeField] private string date;
    [SerializeField] private Sprite previewSprite;

    [SerializeField] private Text previewCat;
    [SerializeField] private Text previewTitle;
    [SerializeField] private Text previewDate;
    [SerializeField] private Image previewImage;

    Color myColor;
    private readonly Dictionary<string, Color> catColors = new Dictionary<string, Color>(){
        {"DEVS",   new Color(150/255f, 107/255f, 205/255f)},
        {"GENERAL",   new Color(253/255f, 175/255f, 52/255f)}
     };  


    void Start()
    {
        previewCat.text = category;
        previewTitle.text = title;
        previewDate.text = date;
        previewImage.sprite = previewSprite;
        myColor = whatColorAmI(category);
        previewCat.color = myColor;
    }
    public string getCategory()
    {
        return category;
    }
    public string getTitle()
    {
        return title;
    }
    public string getDate()
    {
        return date;
    }
    public Color getColor()
    {
        return myColor;
    }
    public Sprite getExpandedImage()
    {
        return expImage;
    }
    Color whatColorAmI(string str)
    {
        return catColors[str];
    }
}
