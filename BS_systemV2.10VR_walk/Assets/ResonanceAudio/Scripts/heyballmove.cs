using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heyballmove : MonoBehaviour
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
        transform.position = Vector3.MoveTowards(transform.position, Hey_Ball.target, speed * Time.deltaTime);
        //Debug.Log(Ball.target);
        Destroy(this.gameObject, 3.0f);
    }
}
