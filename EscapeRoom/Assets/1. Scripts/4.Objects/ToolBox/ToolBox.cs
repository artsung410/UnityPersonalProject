using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : InterectiveObject
{
    [Header("ToolBox")]
    [SerializeField] private float activeTime_AfterCompletion;
    private bool isPickUpKey;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        NumLock.Unlock += SetAvailability;
        ItemPickup.PickUpSignal += ActiveBoxCollider;
    }

    public override void Operate()
    {
        if (LockController.isNumLockOpen == true)
        {
            isActive = !isActive;
            animator.SetBool("isActive", isActive);
            SoundManager.Instance.PlayObjectSound(audioSource, "ToolBoxOpen", "ToolBoxClose", isActive);
            
            if (isPickUpKey == false)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        else
        {
            SoundManager.Instance.PlayObjectSound(audioSource, "Locked");
        }
    }

    public void SetAvailability()
    {
        isOpened = true;
    }

    public void ActiveBoxCollider(Item item)
    {
        if(item.name == "Silver_Key")
        {
            isPickUpKey = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
