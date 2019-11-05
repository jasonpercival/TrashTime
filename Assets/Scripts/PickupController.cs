using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    public AudioClip pickupSound;
    private Animator anim;
    private bool isPickupReady = false;
    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (isPickupReady && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1")))
        {
            Debug.Log("Picked Up");
            GameManager.Instance.UpdateScore();
            if (pickupSound)
            {
                SoundManager.Instance.PlaySound(pickupSound);
            }
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
