using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public int agentId;
    private void OnTriggerEnter(Collider other)
    {
        gameManager.EndEpisode(agentId);
    }
}
