using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public List<GameObject> objects;

    private static bool created = false;

    private void Start()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            for(int i = 0; i < objects.Count; i++)
            {
                DontDestroyOnLoad(objects[i]);
            }
            created = true;
        }
        else
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Destroy(objects[i]);
            }
        }
    }
}
