using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interectable
{
    public Animator pivotAnim;

    public override void Interact()
    {
        
        if(GameManager.Instance.inventory.Contains(1))
        {
            GameManager.Instance.inventory.Remove(1);
            Debug.Log("You oppened the dor");
            pivotAnim.SetBool("Open", true);
        }
        else
        {
            Debug.Log("The door is Locked");
        }
    }
}
