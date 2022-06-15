using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebRequestTest : MonoBehaviour
{
    [SerializeField] ShopItem[] shopItem;
    [SerializeField] ShopTimer shopTimer;
    async void Start()
    {
        string shopItemString = await UnityWebRequest.HttpRequest("/shop");

        string[] shopItems = shopItemString.Split('{');
        for(int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i] = '{' + shopItems[i].Substring(0, shopItems[i].Length - 1);
        }
        try
        {
            for(int i = 0; i < shopItem.Length; i++)
            {
                
                JsonUtility.FromJsonOverwrite(shopItems[i + 1], shopItem[i]);
                shopItem[i].updateInfo();
            }
            Debug.Log(shopItems[4]);
            JsonUtility.FromJsonOverwrite(shopItems[4], shopTimer);
            shopTimer.startTimer();
            Debug.Log("Got shop items");

        } catch (Exception e)
        {
            Debug.Log(e);
        } 
        


    }

  
}
