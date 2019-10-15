using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    private Animator anim;
    private bool isPickupReady = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (isPickupReady && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Picked Up");
            Destroy(gameObject);
        }
    }

    // check if player enters pickup collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPickupReady = true;
            anim.SetBool("IsReady", isPickupReady);
        }
    }

    // check if player leaves pickup collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPickupReady = false;
            anim.SetBool("IsReady", isPickupReady);
        }
    }
}
