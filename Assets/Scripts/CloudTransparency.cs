using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudTransparency : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1,1,1, Random.Range(0.3f, 0.9f));
    }
}
