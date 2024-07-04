using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    public float awarenessBoost = 20f;

    public void Use()
    {
        AwarenessManager awarenessManager = FindObjectOfType<AwarenessManager>();
        if (awarenessManager != null)
        {
            awarenessManager.IncreaseAwareness(awarenessBoost);
        }
        /*Destroy(gameObject); */
    }
}


