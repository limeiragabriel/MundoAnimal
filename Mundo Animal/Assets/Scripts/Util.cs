using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Util
{
    public static void ClearConsole()
    {
        var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    public static List<E> ShuffleList<E>(List<E> inputList, int seed)
    {
        List<E> randomList = new List<E>();
        List<E> tempList = inputList.GetRange(0,inputList.Count);


        System.Random r = new System.Random(seed);
        int randomIndex = 0;
        while (tempList.Count > 0)
        {
            randomIndex = r.Next(0, tempList.Count); //Choose a random object in the list
            randomList.Add(tempList[randomIndex]); //add it to the new, random list
            tempList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }
}
