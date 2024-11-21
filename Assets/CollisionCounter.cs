using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    // Start is called before the first frame update

    public Text CollisionText;
    int collisionCount;
    int totalCollision;
    void Start()
    {
        totalCollision = PlayerCollision.GetCollision();
        CollisionText.text = totalCollision.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
