using UnityEngine;

public static class MyUtils
{
    public static Transform FindChildByName(this Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            Transform result = child.FindChildByName(childName);
            if (result != null)
                return result;
        }

        return null;
    }
}