using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allmove : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, All.target, speed * Time.deltaTime);
        //Debug.Log(Ball.target);
        Destroy(this.gameObject, 3.0f);
    }
}
