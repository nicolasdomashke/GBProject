using System.Collections;
using System.Collections.Generic;
using System;

public class ResponseClass
{
    public string status = null;
    public List<string> keys = null;
    public List<string> values = null;
}

public class UserData
{
    public string login = null;
    public string password = null;
}

public class GameData
{
    public static string currentUser = "";
    public static float startTime = 0f;
    public static bool isFullRun = false;
}