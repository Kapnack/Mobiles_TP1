using UnityEngine;
using System.Collections;

public class VeredaRespawn : MonoBehaviour
{
    public string PlayerTag = "Player";
    
    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag)) 
            return;
        
        if (other.TryGetComponent<Respawn>(out var respawn))
            respawn.Respawnear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag(PlayerTag)) 
            return;
        
        if (collision.transform.TryGetComponent<Respawn>(out var respawn))
            respawn.Respawnear();
    }
}