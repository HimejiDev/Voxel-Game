using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    /* Variables */
    [Header("Object References")]
    [SerializeField] private PlayerController pController;

    /* Default Functions */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == pController.gameObject)
            return;

        pController.SetGrounded(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == pController.gameObject)
            return;

        pController.SetGrounded(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == pController.gameObject)
            return;

        pController.SetGrounded(true);
    }
}
