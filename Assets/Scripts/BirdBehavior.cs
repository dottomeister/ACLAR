using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 30;

    [SerializeField]
    float forceToAdd = 110;

    float speed;

    bool direction = false;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(0, 2) == 1;
        speed = Random.Range(10f, maxSpeed);
    }

    private void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime * (direction ? 1 : -1), 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y).normalized * forceToAdd, ForceMode2D.Impulse);
        print(new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y).normalized * forceToAdd);
    }
}
