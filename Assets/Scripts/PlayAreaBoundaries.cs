using UnityEngine;

public class PlayAreaBoundaries : MonoBehaviour
{
    //destroy anything that leaves the play area
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject); 
    }
}
