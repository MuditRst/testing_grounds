using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererComponent : MonoBehaviour
{
    private LineRenderer lineRenderer;
    ReplaySystem replaySystem;
    
    void Start(){
        replaySystem = GetComponent<ReplaySystem>();
        lineRenderer = replaySystem.Player.AddComponent<LineRenderer>();
    }
    public Material material;
    // Start is called before the first frame update
    public void resetLineRenderer(){
        lineRenderer.positionCount = 0;
    }
    void setLineRenderer(){
        Color red = Color.red;
        
        lineRenderer.material = new Material(material);
        lineRenderer.startColor = red;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }
    public void LineRendererComponentFn(){
        setLineRenderer();
        
        lineRenderer.positionCount = replaySystem.load_positions.Count;

        for (int i = 0; i < replaySystem.load_positions.Count; i++ )
        {
            lineRenderer.SetPosition(i, replaySystem.load_positions[i]);
        }
    }


}
