using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interectable
{
    public int index;

    public override void Interact()
    {
        if (IsInventoryFull())
            return;

        GameManager.Instance.inventory.Add(index);
        Destroy(gameObject);
    }
}
