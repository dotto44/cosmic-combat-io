using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Networking;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
public class UnityWebRequest : MonoBehaviour
{
    public static string URL = "http://localhost:3000";
    static HttpClient client = new HttpClient();

    private async void Start()
    {
        //Debug.Log(await HttpRequest(""));
    }
    public static async Task<string> HttpRequest(string path)
    {
        string text = null;
        HttpResponseMessage response = await client.GetAsync(URL + path);
        if (response.IsSuccessStatusCode)
        {
            text = await response.Content.ReadAsStringAsync();
        }
        Debug.Log(text);
        return text;
    }
}
