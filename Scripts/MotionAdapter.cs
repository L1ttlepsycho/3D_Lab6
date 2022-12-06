using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionAdapter
{
    private DiskMotion diskmotion;
    public MotionAdapter(DiskMotion dm)
    {
        diskmotion = dm;
    }

    public void launch(Disk disk)
    {
        diskmotion.launch(disk);
    }
}
