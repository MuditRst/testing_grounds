
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class ReplaySystem : MonoBehaviour
{

    List<Position> positions = new List<Position>();
    
    LineRendererComponent lr;
    private Vector3 DefaultPosition;
    [SerializeField]public GameObject Player;
    [SerializeField]private float CurrentIndex;
    private float changeRate = 0;
    [SerializeField]bool isReplaying;
    [SerializeField]bool isRewind;
    [SerializeField]bool isRecord;
    [SerializeField]int count;
    private bool pos,rot;

    public Canvas canvas;
    public List<Vector3> load_positions = new List<Vector3>();
    protected List<Quaternion> load_rotations = new List<Quaternion>();

    Vector3 MoveBy;
    

    
    void Start()
    {
        lr = GetComponent<LineRendererComponent>();
        canvas.worldCamera = Camera.main;
    }
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        DefaultPosition = Player.transform.position;
    }

    void Update()
    {
        if(isReplaying || isRewind){
            Player.GetComponent<Movement>().enabled = false;
            if(CurrentIndex == positions.Count - 1){
                isReplaying = false;
                isRewind = false;
                Player.GetComponent<Movement>().enabled = true;
                return;
            }
        }

        if(Input.GetButton("AButton")){ //replay
            
            isReplaying = true;
            isRewind = false;
            changeRate = 1;
            setTransform(0);
            Player.GetComponent<Rigidbody>().isKinematic = true;
            isReplaying = true;
            isRecord = false;
            
        }

        if(Input.GetButton("BButton")){ //rewind
            isRewind = true;
            isReplaying = false;
            changeRate = -1; /* Skip how many frames? */
            setTransform(CurrentIndex + changeRate);
        }

        if(Input.GetButton("YButton")){ //reset
            isRewind = false;
            isReplaying = false;
            Player.transform.position = DefaultPosition;
            Player.GetComponent<Rigidbody>().isKinematic = false;
            lr.resetLineRenderer();
        }

        if(Input.GetButton("RSHButton")){ //play
            changeRate = 1;
        }

        if(Input.GetButton("LSHButton")){ //pause
            changeRate = 0;
        }
    }

    private void FixedUpdate(){
        if(!isReplaying && isRecord){
            positions.Add(new Position{position = Player.transform.position,rotation = Player.transform.rotation});
        }

        if(isReplaying){
            float nextIndex = CurrentIndex + changeRate;
            if(nextIndex < load_positions.Count && nextIndex < load_rotations.Count && nextIndex >= 0){
                setTransform(nextIndex);
            }
            
            lr.LineRendererComponentFn();
        }

    }

    public void SaveButton(){
        saveData();
    }

    public void LoadButton(){
        loadData();
    }

    public void Record(){
        isRecord = true;
    }

    public void StopRecording(){
        isRecord = false;
    }

    private void setTransform(float Index){
        CurrentIndex = Index;
        Vector3 position = load_positions[(int)Index];
        Quaternion rotation = load_rotations[(int)Index];

        Player.transform.position = position;
        Player.transform.rotation = rotation;
    }

    private void saveData(){
        string path = Application.dataPath +"/ReplayData" + "/ReplayData{count}.txt".Replace("{count}", UnityEngine.Random.Range(0, 100).ToString());
        StreamWriter writer = new StreamWriter(path);
        foreach(Position pos in positions){
            writer.WriteLine(Mathf.Round(pos.position.x *100f)*0.01f + "," + Mathf.Round(pos.position.y *100f)*0.01f + "," + Mathf.Round(pos.position.z *100f)*0.01f);
            writer.WriteLine(Mathf.Round(pos.rotation.x *100f)*0.01f + "," + Mathf.Round(pos.rotation.y *100f)*0.01f + "," + Mathf.Round(pos.rotation.z *100f)*0.01f);
        }
        writer.Close();
        

    }

    private void loadData(){
        string[] files = Directory.GetFiles(Application.dataPath +"/ReplayData");

        foreach(string file in files){

            int i=1;
            StreamReader stream = new StreamReader(file);
            string lineReader = stream.ReadToEnd();
            string[] lines = lineReader.Split('\n');
            foreach(string line in lines){
                //Debug.Log(line);
                string Trimmedline = line.TrimEnd(new char[] {'\r'});
                string[] values = Trimmedline.Split(',');
                
                if(i <= lineReader.Length && values.Length > 0){
                    AddData(values,i);
                    i++; 
                }else{
                    break;
                }
            }
            
        }
    }

    void AddData(string[] lines,int i){
        if(i%2 != 0 && lines[0] != null){
            load_positions.Add(new Vector3(float.Parse(lines[0]),float.Parse(lines[1]),float.Parse(lines[2])));             
        }
        if(i%2 == 0 && lines[0] != null){
            load_rotations.Add(new Quaternion(float.Parse(lines[0]),float.Parse(lines[1]),float.Parse(lines[2]),0));
                    
        }
                
    }

    
}

