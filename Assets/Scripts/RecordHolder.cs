using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecordHolder
{
    private static int[] killRecords = { 0, 0, 0, 0, 0, 0};
    private static int[] xpAmounts = { 0, 0, 0, 0, 0 };
    private static int[] xpLevels = { 1, 1, 1, 1, 1};
    private static int[] xpLeftover = { 0, 0, 0, 0, 0 };

    private static bool hasCalcedStartingLevels;

    private static readonly float lengthOfBar = 836f;

    private static readonly int[] levelUpReqs =
    {
        14, //2 38
        14, //3 36
        14, //4 34
        21, //5 31
        21, //6 28
        21, //7 25
        28, //8 21
        28, //9 17
        28, //10 13
        35, //11 8
        35, //12 3
        35, //13
        42, //14
        42, //15
        42, //16
        49, //17
        49, //18
        49, //19
        70,
        100
    };
    private static readonly int[] banner =
    {
        0, //2 38
        1, //3 36
        1, //4 34
        1, //5 31
        2, //6 28
        2, //7 25
        2, //8 21
        3, //9 17
        3, //10 13
        3, //11 8
        4, //12 3
        4, //13
        4, //14
        5, //15
        5, //16
        5, //17
        6, //18
        6, //19
        6,
        7
    };
    private static readonly int max = 637;
    public static void calcStartingLevels()
    {
        if (hasCalcedStartingLevels)
        {
            return;
        }
        Debug.Log("CALC STARTING LEVELS");
        for (int i = 0; i < StatsHolder.numberOfCharacters; i++)
        {
            int xp = xpAmounts[i];
            int level = 0;
            while(xp >= levelUpReqs[level])
            {
                xp -= levelUpReqs[level];
                level++;
                Debug.Log("CURRENT LEVEL (-1 actual): " + level + "     Amount Remaining: " + xp);
            }
            xpLeftover[i] = xp;
            xpLevels[i] = level + 1;
        }
        hasCalcedStartingLevels = true;
    }
    public static void calcSpecificLevel(int i)
    {
        int xp = xpAmounts[i];
        int level = 0;
        while (xp >= levelUpReqs[level])
        {
            xp -= levelUpReqs[level];
            level++;
        }
        xpLeftover[i] = xp;
        xpLevels[i] = level + 1;
    }
        public static int getLevel(int index)
    {
        //Debug.Log("GET LEVEL FOR INDEX: " + index + "   WHICH IS: " + xpLevels[index]);
        return xpLevels[index];
    }
    public static int getBanner(int index)
    {
        return banner[xpLevels[index] - 1];
    }
    public static int getRemaining(int index)
    {
        if (getLevel(index) != 20)
        {
            return levelUpReqs[xpLevels[index] - 1] - xpLeftover[index];
        }
        return 0;
    }

    public static void setKillRecord(int index, int killAmt)
    {
        if(killAmt > killRecords[index])
        {
            killRecords[index] = killAmt;
            saveNewRecord();
        }
        addXPtoCharacter(index, killAmt * 7);
    }
    public static void addXPtoCharacter(int index, int xpAmt)
    {
            if(xpAmounts[index] + xpAmt > max)
            {
            return;
             }
            xpAmounts[index] += xpAmt;
            calcSpecificLevel(index);
    }
    public static float getLengthOfBar(int index)
    {
        if (getLevel(index) != 20)
        {
            return lengthOfBar * ((float)xpLeftover[index] / (float)levelUpReqs[xpLevels[index] - 1]);
        }
        return lengthOfBar;
    }
    public static int getKillsRecord(int index)
    {
        return killRecords[index];
    }
    public static int getXPForChar(int index)
    {
        return xpAmounts[index];
    }
    public static void setXPForChar(int index, int[] amount)
    {
        xpAmounts = amount;
    }
    private static void saveNewRecord()
    {
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SAVE_PRS")
           .SetEventAttribute("NORM", killRecords[0])
           .SetEventAttribute("ROVER", killRecords[1])
           .SetEventAttribute("VEN", killRecords[2])
           .SetEventAttribute("JUP", killRecords[3])
           .SetEventAttribute("NEP", killRecords[4])
           .SetEventAttribute("PLU", killRecords[5])
           .Send((response) => {
               if (!response.HasErrors)
               {
                   Debug.Log("PR's Saved To GameSparks...");
               }
               else
               {
                   Debug.Log("Error Saving PR Data...");
               }
           });
    }
}
