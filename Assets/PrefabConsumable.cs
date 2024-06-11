using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabConsumable : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    public void CreateItem(int index)
    {
        switch (index)
        {
            case 201:
                Instantiate(prefabs[0], transform.position, Quaternion.identity);
                break;
            case 202:
                Instantiate(prefabs[1], transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("Default");
                break;
        }
        Destroy(gameObject);
    }
}
