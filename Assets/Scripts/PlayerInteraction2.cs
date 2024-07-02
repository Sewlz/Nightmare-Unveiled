using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction2 : MonoBehaviour
{
    private EnergyDrink currentEnergyDrink;

    void Update()
    {
        if (currentEnergyDrink != null && Input.GetKeyDown(KeyCode.E))
        {
            currentEnergyDrink.Use();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyDrink"))
        {
            currentEnergyDrink = other.GetComponent<EnergyDrink>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnergyDrink"))
        {
            if (currentEnergyDrink != null && other.GetComponent<EnergyDrink>() == currentEnergyDrink)
            {
                currentEnergyDrink = null;
            }
        }
    }
}

