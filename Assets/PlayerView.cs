using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] Camera playerCamera;

    RaycastHit hit;

    bool lookingAtInteractive;

    [SerializeField] GameObject inventory;

    

    // Update is called once per frame
    void Update()
    {
        FindInteractive();

        if(lookingAtInteractive && Input.GetKeyDown(KeyCode.E))
        {
            hit.collider.GetComponent<Interectable>().Interact();
        }
    }

    private void FindInteractive()
    {
        
        if (Physics.Raycast(playerCamera.transform.position,
            playerCamera.transform.forward, out hit, 2f) &&
            hit.collider.CompareTag("Interactive"))
        {
            interactText.text = hit.collider.name;
            lookingAtInteractive = true;
        }
        else
        {
            lookingAtInteractive = false;
        }

    }

}
