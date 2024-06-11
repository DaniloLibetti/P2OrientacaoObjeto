using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMisc : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    public void CreateItem(int index)
    {
        switch (index)
        {
            case 1:
                Instantiate(prefabs[0], transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(prefabs[1], transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("Default");
                break;
        }
        Destroy(gameObject);
    }
}
