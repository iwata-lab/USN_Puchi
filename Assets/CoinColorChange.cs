using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinColorChange : MonoBehaviour
{
    private Renderer coinRenderer;   
   
    // Start is called before the first frame update
    void Start()
    {
        coinRenderer = GetComponent<Renderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color color)
    {
        coinRenderer.material.color = color;        

    }

}
