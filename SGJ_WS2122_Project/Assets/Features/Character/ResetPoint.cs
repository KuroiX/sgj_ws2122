using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<DeathBehaviour>().SetPoint(transform.position);
    }
}
