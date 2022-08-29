using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{

    public Ghost ghost;

    private float timerValue;

    private int index1;
    private int index2;
    // Start is called before the first frame update
    void Awake()
    {
        timerValue=0;
    }

    // Update is called once per frame
    void Update()
    {
        timerValue += Time.unscaledDeltaTime;
        if(ghost.isReplay){
            getIndex();
            setTransform();
        }
        
    }

    public void Replay(){
        ghost.isReplay = true;
        ghost.isRecord = false;
    }

    public void clearData(){
        ghost.ClearData();
        GetComponent<GhostRecorder>().timer = 0;
        GetComponent<GhostRecorder>().timeValue=0;
    }
    

    void getIndex(){
        for(int i=0;i<ghost.timeStamp.Count-2;i++){
            if(ghost.timeStamp[i] == timerValue){
                index1=i;
                index2=i;
                return;
            }else if(ghost.timeStamp[i] < timerValue && ghost.timeStamp[i+1] > timerValue){
                index1=i;
                index2=i+1;
                return;
            }

            index1=ghost.timeStamp.Count-1;
            index2=ghost.timeStamp.Count-1;
        }
    }

    void setTransform(){
        if(index1 == index2 && index1 < ghost.timeStamp.Count){
            transform.position = ghost.position[index1];
            transform.rotation = ghost.rotation[index1];
        }else{
            float interpolateFactor = (timerValue - ghost.timeStamp[index1]) / (ghost.timeStamp[index2] - ghost.timeStamp[index1]);

            this.transform.position = Vector3.Lerp(ghost.position[index1], ghost.position[index2], interpolateFactor);
            this.transform.rotation = Quaternion.Lerp(ghost.rotation[index1], ghost.rotation[index2], interpolateFactor);
        }
    }
}
