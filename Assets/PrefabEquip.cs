using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabEquip : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    public void CreateItem(int index)
    {
        switch (index)
        {
            //Sword
            case 101:
                Instantiate(prefabs[0], transform.position, Quaternion.identity);
                break;
            
            case 102:
                Instantiate(prefabs[1], transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("Default");
                break;
        }
        Destroy(gameObject);
    }
}
