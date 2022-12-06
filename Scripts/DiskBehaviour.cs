using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour : DiskMotion
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void  FixedUpdate()
    {
        if (disk != null)
        {
            if (disk.ufo == null) return;
            disk.ufo.transform.localPosition += new Vector3(x_speed*Time.deltaTime,y_speed*Time.deltaTime,0);
            y_speed -= 6*Time.deltaTime;
        }
    }
    public override void launch(Disk disk)
    {
        this.disk = disk;
        x_speed = Random.Range(-25, -15);
        y_speed = Random.Range(0, 5);
        disk.ufo.gameObject.SetActive(true);
        disk.ufo.GetComponent<Rigidbody>().useGravity = false;
    }
}
