using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class DebugLogs : MonoBehaviour
{

    private static DebugLogs instance = null;
    public static DebugLogs Instance => instance;

    public TMP_Text logs;
    public List<string> last3Objects = new List<string>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            last3Objects.Append("");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewHit(string name)
    {
        Debug.Log(name);
        last3Objects[0] = last3Objects[1];
        last3Objects[1] = last3Objects[2];
        last3Objects[2] = Time.time.ToString() +name + " - Hit";
        string full = last3Objects[0] + "\n" + last3Objects[1] + "\n" + last3Objects[2] + "\n";
        logs.text = full;

    }
    public void NewDestroy(string name)
    {
        Debug.Log(name);

        last3Objects[0] = last3Objects[1];
        last3Objects[1] = last3Objects[2];
        last3Objects[2] = Time.time.ToString() + name + " - Destroyed";
        string full = last3Objects[0] + "\n" + last3Objects[1] + "\n" + last3Objects[2] + "\n";
        logs.text = full;

    }
    public void NewOpened(string name)
    {
        Debug.Log(name);

        last3Objects[0] = last3Objects[1];
        last3Objects[1] = last3Objects[2];
        last3Objects[2] = Time.time.ToString() + name + " - Opened";
        string full = last3Objects[0] + "\n" + last3Objects[1] + "\n" + last3Objects[2] + "\n";
        logs.text = full;
    }
}
