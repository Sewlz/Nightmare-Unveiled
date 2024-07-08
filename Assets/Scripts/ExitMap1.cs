using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMap1 : MonoBehaviour
{
    public string sceneMap2;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay called with: " + other.name);

        if (other.gameObject)
        {
            SceneManager.LoadScene(sceneMap2);
        }
    }
}
