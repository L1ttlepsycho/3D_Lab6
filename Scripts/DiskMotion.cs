using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiskMotion : MonoBehaviour
{
    protected float x_speed, y_speed;
    protected Disk disk;
    public abstract void launch(Disk disk);
}
