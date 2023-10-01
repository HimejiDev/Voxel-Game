using UnityEditor;

public static class DevTools
{
    [MenuItem("DevTools/Player/Speed (+10)")]
    public static void PlayerAdd10Speed()
    {
        PlayerMovement.speed += 10;
    }

    [MenuItem("DevTools/Player/Speed (-10)")]
    public static void PlayerRemove10Speed()
    {
        PlayerMovement.speed -= 10;
    }
}
