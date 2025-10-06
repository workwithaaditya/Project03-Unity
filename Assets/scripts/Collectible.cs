using UnityEngine;
using Photon.Pun;

public class Collectible : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScore ps = other.GetComponent<PlayerScore>();
            if (ps != null)
            {
                ps.AddScore(1); // +1 score
            }

            PhotonNetwork.Destroy(gameObject); // Object destroy network-wide
        }
    }
}
