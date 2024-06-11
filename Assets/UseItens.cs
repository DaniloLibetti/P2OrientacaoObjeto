using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UseItens : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory;

    public void UseItem()
    {
        switch (playerInventory.selectedInventoryIndex)
        {
            //potion
            case 201:
                if(GameManager.Instance.life <= 25)
                {
                    GameManager.Instance.playerCurrentLife += 5;
                    GameManager.Instance.LifeUpdate();
                }
                else if (GameManager.Instance.life < 30)
                {
                    GameManager.Instance.playerCurrentLife = 30;
                    GameManager.Instance.LifeUpdate();
                }
                break;
            //poison
            case 202:
                GameManager.Instance.life -= 5;
                break;
            default:
                Debug.Log("No index");
                break;
        }

        GameManager.Instance.inventory.RemoveAt(playerInventory.selectedInventoryPos);

        playerInventory.UpdateInventory();

    }
}
