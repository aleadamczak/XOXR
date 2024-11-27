using UnityEngine;

public class Utility
{
    public static GameObject FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }

            GameObject result = FindChildByName(child, name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}
