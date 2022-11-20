using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton Disk Factory
public class DiskFactory: MonoBehaviour
{
    private static DiskFactory instance;
    float disk_border = 6.0f;
    float color_border1 = 5.0f;
    float color_border2 = 8.0f;
    public List<Disk> used_disks =new List<Disk>();

    public static DiskFactory getInstance()
    {
        if (instance == null)
        {
            instance = (DiskFactory)FindObjectOfType(typeof(DiskFactory));
            if (instance == null)
            {
                Debug.LogError("An instance of " + typeof(DiskFactory)
                    + " is needed in the scene, but there is none.");
            }
        }
        return instance;
    }

    public Disk getDisk()
    {
        int disk_num = Random.Range(0,10);
        int color_num = Random.Range(0, 10);
        //float x_speed = Random.Range(-25, -15);
        //float y_speed = Random.Range(0, 5);
        float y_pos = Random.Range(8, 15);

        GameObject ufo;
        Material mat;
        // get disk prefab
        if (disk_num < disk_border)
        {
            ufo = Instantiate(Resources.Load<GameObject>("Prefabs/UFO_1"));
            disk_border -= 0.5f;
        }
        else
        {
            ufo = Instantiate(Resources.Load<GameObject>("Prefabs/UFO_2"));
            disk_border += 0.5f;
        }
        // get material prefab
        if (color_num < color_border1)
        {
            mat = Resources.Load<Material>("Prefabs/UFO_C1");
            color_border1 -= 0.5f;
            color_border2 -= 0.25f;
        }
        else if(color_num > color_border2)
        {
            mat = Resources.Load<Material>("Prefabs/UFO_C3");
            color_border1 += 0.25f;
            color_border2 += 0.5f;
        }
        else
        {
            mat = Resources.Load<Material>("Prefabs/UFO_C2");
            color_border1 += 0.25f;
            color_border2 -= 0.25f;
        }

        // twerking attributes 
        ufo.transform.localPosition = new Vector3(20, y_pos, 0);
        ufo.GetComponent<Renderer>().material = mat;
        ufo.gameObject.SetActive(false);
        //ufo.GetComponent<Rigidbody>().velocity = new Vector3(x_speed, y_speed, 0); 

        
        Disk disk = new Disk(ufo,mat);
        used_disks.Add(disk);
        return disk;
    }

    public void destroyDisk(Disk disk)
    {
        for(int i = 0; i < used_disks.Count; i++)
        {
            if (disk.ufo.GetInstanceID() == used_disks[i].ufo.GetInstanceID())
            {
                used_disks[i].ufo.gameObject.SetActive(false);
                Destroy(used_disks[i].ufo);
                used_disks.RemoveAt(i);
            }
        }
    }
    private void Start()
    {

    }
}
