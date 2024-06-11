using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interectable : MonoBehaviour
{
    public virtual void Interact()
    {
        
    }

    public bool IsInventoryFull()
    {
        if(GameManager.Instance.inventory.Count >= 4)
        {
            Debug.Log("Inventory full");
            return true;
        }
        else
        {
            return false;
        }
    }
}
