using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI equipmentText;
    [SerializeField] TextMeshProUGUI[] inventoryTexts;
    [SerializeField] GameObject inventoryDisplay;
    [SerializeField] GameObject optionsDisplay;
    [SerializeField] GameObject[] dropPrefab;
    [SerializeField] Button[] inventoryButtons;
    [SerializeField] Button[] optionsButtons;
    [SerializeField] Button equipmentButton;

    public int selectedInventoryPos;
    public int selectedInventoryIndex;

    public bool inventoryOpen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen == false)
            {
                inventoryOpen = true;
                inventoryDisplay.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                UpdateInventory();
            }
            else
            {
                inventoryOpen = false;
                inventoryDisplay.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void UpdateInventory()
    {
        if(inventoryOpen)
        {
            for(int i = 0; i < 4; i++)
            {
                if (i < GameManager.Instance.inventory.Count)
                {
                    inventoryButtons[i].interactable = true;
                    inventoryTexts[i].text = GameManager.Instance.inventory[i].ToString();
                }
                else
                {
                    inventoryButtons[i].interactable = false;
                    inventoryTexts[i].text = null;
                } 
            }
            
            if (GameManager.Instance.equipment != 0)
            {
                equipmentButton.interactable = true;
                equipmentText.text =
                    GameManager.Instance.equipment.ToString();
            }
            else
            {
                equipmentButton.interactable = false;
                equipmentText.text = null;
            }
        }

        DisableOptions();

    }

    public void DropItem()
    {
        Debug.Log("qualquer");

        if (selectedInventoryIndex <= 100)
        {
            GameObject drop = Instantiate(dropPrefab[0], transform.position, Quaternion.identity);

            drop.GetComponent<PrefabMisc>().CreateItem(selectedInventoryIndex);
        }

        if (selectedInventoryIndex >= 101 && selectedInventoryIndex < 201)
        {
            GameObject drop = Instantiate(dropPrefab[1], transform.position, Quaternion.identity);

            drop.GetComponent<PrefabEquip>().CreateItem(selectedInventoryIndex);
        }

        if (selectedInventoryIndex >= 200 && selectedInventoryIndex < 301)
        {
            GameObject drop = Instantiate(dropPrefab[2], transform.position, Quaternion.identity);

            drop.GetComponent<PrefabConsumable>().CreateItem(selectedInventoryIndex);
        }

        GameManager.Instance.inventory.RemoveAt(selectedInventoryPos);

        UpdateInventory();
    }

    public void ItemOptions(int inventoryPosition)
    {
        selectedInventoryPos = inventoryPosition;

        selectedInventoryIndex = GameManager.Instance.inventory[inventoryPosition];
        EnableOptions();
    }

    void EnableOptions()
    {
        if (selectedInventoryIndex <= 100)
        {
            optionsButtons[0].interactable = false;
            optionsButtons[1].interactable = false;
        }

        if (selectedInventoryIndex >= 101 && selectedInventoryIndex <= 200)
        {
            optionsButtons[0].interactable = false;
            optionsButtons[1].interactable = true;
        }

        if (selectedInventoryIndex >= 201 && selectedInventoryIndex <= 300)
        {
            optionsButtons[0].interactable = true;
            optionsButtons[1].interactable = false;
        }
        optionsDisplay.SetActive(true);
    }

    void DisableOptions()
    {
        optionsDisplay.SetActive(false);
    }

}
