using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            Destroy(collision.rigidbody.gameObject);
        }
    }
}
