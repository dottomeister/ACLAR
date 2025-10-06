using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudsMoving : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 10;

    float speed;

    bool direction = false;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(0, 2) == 1;
        speed = Random.Range(1.0f, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime * (direction ? 1 : -1), 0, 0);
    }
}
