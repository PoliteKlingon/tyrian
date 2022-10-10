using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaBoundaries : MonoBehaviour
{
    //destroy anything that leaves the play area
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject); //other! aby se neznicila play area, ale ten projektil
    }
}
