using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MethodResource 
{
    public static bool arrayContains(string[] strs, string str)
    {
        foreach (string s in strs)
        {
            if (str.Equals(s)) return true;
        }
        return false;
    }
    

}
