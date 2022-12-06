# 改良版Hit UFO小游戏
## Our Goal
使用adapter设计模式使得游戏支持物理学运动与Kinematic两种模式

## 需要对前版游戏进行的改进
1. 新增物理学运动相关代码（原版本使用自带物理引擎）。
2. 实现一个```MotionAdapter```类用于管理游戏的物理效果。
3. 将原有的物理引擎使用有关代码单独分离出一个类方便Adapter管理。

## 改进部分代码实现
### ```DiskMotion```虚基类
```
public abstract class DiskMotion : MonoBehaviour
{
    protected float x_speed, y_speed;
    protected Disk disk;
    public abstract void launch(Disk disk);
}
```
```x_speed```,```y_speed```代表飞碟的初始x、y方向速度。

### ```DiskBehaviour```纯手工手工物理引擎
```
public class DiskBehaviour : DiskMotion
{
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
```

### ```DiskPhysics```偷懒型物理引擎
```
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
```

### ```MotionAdapter```管理物理引擎使用
```
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
```

### ```SceneController```改动部分
```
    void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        gamejudge = GameJudge.getInstance();
        factory = DiskFactory.getInstance();
        if (kinematic)
        {
            motionAdapter = new MotionAdapter(gameObject.AddComponent<DiskPhysics>());
        }
        else
        {
            motionAdapter = new MotionAdapter(gameObject.AddComponent<DiskBehaviour>());
        }
        loadResources();
    }

    public void LaunchDisk()
    {
        if(disk_queue.Count > 0 && gamejudge.trials++<gamejudge.max_trials)
        {
            Disk disk = disk_queue.Dequeue();
            // Debug.Log("Launching...");
            motionAdapter.launch(disk);
        }       
    }
```
通过一个```public bool kinematic```变量控制使用哪种物理引擎。

## 演示Example
见项目文件。
