using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject Player;    
    public Transform spawnPoint;  
    public float spawnValue;

    void Update()
    {
        if (Player.transform.position.y < spawnValue)
        {
            RespawnPoint();
        }
    }

    void RespawnPoint()
    {
        Player.transform.position = spawnPoint.position;
    }
}
