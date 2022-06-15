using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewsManager : MonoBehaviour
{
    [SerializeField] GameObject[] articles;
    [SerializeField] ArticleInfo[] articleInfos;
    [SerializeField] Text expandedTitle;
    [SerializeField] Text expandedCategory;
    [SerializeField] Text expandedDate;
    [SerializeField] Image expandedImage;
    [SerializeField] Animator blocker;
    private int currentArticle = 1;
    int queueItem;
    private void Start()
    {
        switchArticle(0);
    }

    public void switchArticle(int to)
    {
        blocker.SetBool("fade", true);
        queueItem = to;
    }
    public void makeTheSwitch()
    {
        if (currentArticle != queueItem)
        {
            articles[currentArticle].SetActive(false);
            articles[queueItem].SetActive(true);
            currentArticle = queueItem;
            expandedTitle.text = articleInfos[queueItem].getTitle();
            expandedCategory.text = articleInfos[queueItem].getCategory();
            expandedCategory.color = articleInfos[queueItem].getColor();
            expandedDate.text = articleInfos[queueItem].getDate();
            expandedImage.sprite = articleInfos[queueItem].getExpandedImage();
        }
    }

}
