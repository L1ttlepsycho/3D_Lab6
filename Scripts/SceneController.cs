using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using System.Linq;

public class SceneController : MonoBehaviour, IPlayerAction, ISceneController
{
    private static SceneController instance;

    public GameJudge gamejudge;
    public DiskFactory factory;


    private Queue<Disk> disk_queue=new Queue<Disk>();
    private bool isStart = false;
    private bool isPlaying = false;
    private bool isOver = false;

    public static SceneController getInstance()
    {
        if(instance == null)
        {
            instance = (SceneController)FindObjectOfType(typeof(SceneController));
            if (instance == null)
            {
                Debug.LogError("An instance of " + typeof(SceneController)
                    + " is needed in the scene, but there is none.");
            }
        }
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        gamejudge = GameJudge.getInstance();
        factory = DiskFactory.getInstance();
        loadResources();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if (isOver)
            {
                CancelInvoke("loadResources");
            }
            if (!isPlaying)
            {
                InvokeRepeating("loadResources", 2f, 2f);
                isPlaying = true;
            }
            LaunchDisk();
        }
        if (gamejudge.over())
        {
            isOver = true;
        }
    }
    public void loadResources()
    {
        disk_queue.Enqueue(factory.getDisk());
    }

    void wait()
    {
        Debug.Log("waiting...");
    }

    public void LaunchDisk()
    {
        if(disk_queue.Count > 0 && gamejudge.trials++<gamejudge.max_trials)
        {
            Disk disk = disk_queue.Dequeue();
            Debug.Log("Launching...");
            float x_speed = Random.Range(-25, -15);
            float y_speed = Random.Range(0, 5);
            disk.ufo.gameObject.SetActive(true);
            disk.ufo.GetComponent<Rigidbody>().velocity = new Vector3(x_speed, y_speed, 0);
        }       
    }
    
    public void gameStart()
    {
        isStart = true;
    }

    public void shoot(GameObject cam)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mp = Input.mousePosition; //get Screen Position

            //create ray, origin is camera, and direction to mousepoint
            Camera ca;
            if (cam != null) ca = cam.GetComponent<Camera>();
            else ca = Camera.main;

            Ray ray = ca.ScreenPointToRay(Input.mousePosition);

            //Return the ray's hit
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                for(int i = 0; i < factory.used_disks.Count; i++)
                {
                    if (factory.used_disks[i].ufo == hit.transform.gameObject)
                    {
                        Vector3 pos = hit.transform.gameObject.transform.localPosition;
                        gamejudge.scoring(factory.used_disks[i]);
                        factory.destroyDisk(factory.used_disks[i]);
                        Object exp=Instantiate(Resources.Load("Prefabs/Exploson6"),pos,Quaternion.identity);
                        Destroy(exp, 1f);
                    }
                } 
            }
        }
    }

    public int getScore()
    {
        return gamejudge.score;
    }
}
