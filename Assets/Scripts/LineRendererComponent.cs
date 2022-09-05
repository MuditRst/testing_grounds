using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererComponent : MonoBehaviour
{
    private LineRenderer lineRenderer;
    ReplaySystem replaySystem;

    private Color[] colorList = {Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, Color.magenta, Color.white, Color.black};
    
    void Start(){
        replaySystem = GetComponent<ReplaySystem>();
        lineRenderer = replaySystem.Player.AddComponent<LineRenderer>();
    }
    public Material material;
    // Start is called before the first frame update
    public void resetLineRenderer(){
        lineRenderer.positionCount = 0;
    }
    void setLineRenderer(int num){
        Color c =  colorList[num];
        Debug.Log(c);
        lineRenderer.material = new Material(material);
        material.color = c;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }
    public void LineRendererComponentFn(int num){
        setLineRenderer(num);
        
        lineRenderer.positionCount = replaySystem.load_positions.Count;

        for (int i = 0; i < replaySystem.load_positions.Count; i++ )
        {
            lineRenderer.SetPosition(i, replaySystem.load_positions[i]);
        }
    }


    

}
