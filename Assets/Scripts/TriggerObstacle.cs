using UnityEngine;
using System.Collections.Generic;

public class TriggerObstacle : MonoBehaviour
{
    public GameObject[] obstacles; 
    private List<int> randomIndexes = new List<int>();
    private bool isTriggered = false; 

    void Start()
    {

        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle != null)
            {
                obstacle.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            foreach (GameObject obstacle in obstacles)
            {
                if (obstacle != null)
                {
                    obstacle.SetActive(false);
                }
            }

            randomIndexes.Clear();
            while (randomIndexes.Count < 5)
            {
                int randomIndex = Random.Range(0, obstacles.Length);
                if (!randomIndexes.Contains(randomIndex))
                {
                    randomIndexes.Add(randomIndex);
                }
            }

            foreach (int index in randomIndexes)
            {
                if (obstacles[index] != null)
                {
                    obstacles[index].SetActive(true);
                }
            }
        }
    }
}
