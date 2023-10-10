using UnityEditor;
using UnityEngine;

public static class DevTools
{
    [MenuItem("DevTools/Player/Speed (+10)")]
    public static void PlayerAdd10Speed()
    {
        foreach (GameObject plyr in GameObject.FindGameObjectsWithTag("Player"))
            plyr.GetComponent<PlayerController>().speed += 10;
    }

    [MenuItem("DevTools/Player/Speed (-10)")]
    public static void PlayerRemove10Speed()
    {
        foreach (GameObject plyr in GameObject.FindGameObjectsWithTag("Player"))
            plyr.GetComponent<PlayerController>().speed -= 10;
    }
}
