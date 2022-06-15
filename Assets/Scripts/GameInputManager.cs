using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GameSparks.Core;

	public static class GameInputManager
	{
   
    public static float localAudioValue;
    private static bool hasChangedKeysSinceSave;
		static Dictionary<string, KeyCode> keyMapping;
		static string[] keyMaps = new string[11]
		{
			"Fire1", //0
			"Jump", //1
			"Special", //2
			"Drop", //3
			"Left", //4
			"Right",
		    "Dance",
            "Map",
            "Rocket",
            "Focus",
            "Fullscreen"
		};
		static KeyCode[] defaults = new KeyCode[11]
		{
			KeyCode.Mouse0, //0
	     	KeyCode.W, //1
			KeyCode.Space, //2
			KeyCode.S, //3
			KeyCode.A, //4
			KeyCode.D,
		    KeyCode.LeftShift,
            KeyCode.M,
            KeyCode.W,
            KeyCode.F,
            KeyCode.Tab
		};

		static GameInputManager()
		{
            localAudioValue = StatsHolder.audioValue;
            InitializeDictionary();
		}

    public static void hasModifiedSettings()
    {
        hasChangedKeysSinceSave = true;
    }
    private static void InitializeDictionary()
		{
			keyMapping = new Dictionary<string, KeyCode>();
			for(int i=0;i<keyMaps.Length;++i)
			{
				keyMapping.Add(keyMaps[i], defaults[i]);
			}
		}

		public static void SetKeyMap(string keyMap,KeyCode key)
		{
			if (!keyMapping.ContainsKey(keyMap))
				throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
			keyMapping[keyMap] = key;
		}

		public static bool GetKeyDown(string keyMap)
		{
			return Input.GetKeyDown(keyMapping[keyMap]);
		}
        public static bool GetKey(string keyMap)
        {
        return Input.GetKey(keyMapping[keyMap]);
        }
        public static bool GetKeyUp(string keyMap)
	    {
		return Input.GetKeyUp(keyMapping[keyMap]);
	    }
    public static void swapKey(KeyCode kcode, string key)
    {
        hasChangedKeysSinceSave = true;
        keyMapping[key] = kcode;
    }
    public static void swapKey(KeyCode kcode, string key, bool isUser)
    {
        hasChangedKeysSinceSave = isUser;
        keyMapping[key] = kcode;
    }
    public static void showKeys()
    {
        foreach (KeyValuePair<string, KeyCode> s in keyMapping) {
            Debug.Log("Move: " + s.Key + "Key: " + s.Value);
        }
    }
    public static KeyCode checkKey(string key)
    {
        return keyMapping[key];
    }
   
    public static void saveKeybinds()
    {
        if (!hasChangedKeysSinceSave && Math.Abs(localAudioValue - StatsHolder.audioValue) < 0.1 || !GS.Authenticated)
        {
            return;
        }
        localAudioValue = StatsHolder.audioValue;
        hasChangedKeysSinceSave = false;
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SAVE_KEYBINDS")
            .SetEventAttribute("Fire1", (int)keyMapping["Fire1"])
            .SetEventAttribute("Jump", (int)keyMapping["Jump"])
            .SetEventAttribute("Special", (int)keyMapping["Special"])
            .SetEventAttribute("Drop", (int)keyMapping["Drop"])
            .SetEventAttribute("Left", (int)keyMapping["Left"])
            .SetEventAttribute("Right", (int)keyMapping["Right"])
            .SetEventAttribute("Dance", (int)keyMapping["Dance"])
            .SetEventAttribute("Map", (int)keyMapping["Map"])
            .SetEventAttribute("Focus", (int)keyMapping["Focus"])
            .SetEventAttribute("Volume", (long)StatsHolder.audioValue)
            .Send((response) => {
                if (!response.HasErrors)
                {
                    Debug.Log("Keybinds Saved To GameSparks...");
                }
                else
                {
                    Debug.Log("Error Saving Keybind Data...");
                }
            });
        }
}
