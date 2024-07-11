using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMap1 : MonoBehaviour
{
    public string nextSceneMap;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject)
        {
            SceneManager.LoadScene(nextSceneMap);
        }
    }
}
