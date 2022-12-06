using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskPhysics : DiskMotion
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        
    }
    public override void launch(Disk disk)
    {
        x_speed = Random.Range(-25, -15);
        y_speed = Random.Range(0, 5);
        disk.ufo.gameObject.SetActive(true);
        disk.ufo.GetComponent<Rigidbody>().velocity = new Vector3(x_speed, y_speed, 0);
    }
}
