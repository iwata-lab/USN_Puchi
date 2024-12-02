using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalDistance : MonoBehaviour
{
    // Start is called before the first frame update
 
    public Text DistanceText;
    float distance;
    float totalDistance;
 
    void Start()
    {
        totalDistance = DistanceTracker.GetTotalDistance();
        DistanceText.text = totalDistance.ToString("F2") + " m";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
