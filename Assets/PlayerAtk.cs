using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerAtk : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory;
    //[SerializeField] Transform cameraTransform;
    [SerializeField] GameObject playerEquipment;

    public bool hasEquipment;

    float timer;

    

    int damage;
    float atkSpeed;
    float range;
    public RaycastHit hit;


    private void Start()
    {
        ChangeItem(GameManager.Instance.equipment);
    }

    void Update()
    {
        if (!hasEquipment)
        {
            return;
        }


        timer += Time.deltaTime;


        if (Input.GetMouseButtonDown(0) && !playerInventory.inventoryOpen && timer > atkSpeed)
        {
           if(Physics.Raycast(Camera.main.transform.position, transform.forward, out hit, range))
           {
             if (hit.collider.CompareTag("Enemy"))
             {
               hit.collider.GetComponent<EnemyBehaviour>().EnemyTakeDamage(damage);
               Debug.Log("Attack hit an enemy");
             }
             else
             {
               Debug.Log("Attack hit a non-enemy");
             }
           }
           else
           {
              Debug.Log("Attack missed");
           }

           timer = 0;
        }
 
        
    }

    public void EquipItem()
    {
        ChangeItem(playerInventory.selectedInventoryIndex);

        GameManager.Instance.equipment = playerInventory.selectedInventoryIndex;
        GameManager.Instance.inventory.RemoveAt(playerInventory.
            selectedInventoryPos);

        playerInventory.UpdateInventory();
    }

    public void ChangeItem(int index)
    {
        switch (playerInventory.selectedInventoryIndex)
        {
            //Sword
            case 101:
                hasEquipment = true;
                playerEquipment.SetActive(true);

                damage = 1;
                atkSpeed = 0.3f;
                range = 3;
                break;
            //StrongSword
            case 102:
                hasEquipment = true;
                playerEquipment.SetActive(true);

                damage = 4;
                atkSpeed = 4;
                range = 6;
                break;
        }

    }

    public void UnequipItem()
    {
        if (IsInventoryFull())
            return;

        hasEquipment = false;
        playerEquipment.SetActive(false);

        GameManager.Instance.inventory.Add(GameManager.Instance.equipment);
        GameManager.Instance.equipment = 0;
        playerInventory.UpdateInventory();
    }

    bool IsInventoryFull()
    {
        if (GameManager.Instance.inventory.Count >= 4)
        {
            Debug.Log("Inventory Full!");
            return true;
        }
        else
        {
            return false;
        }
    }
}
