using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject cloudPrefab;

    [SerializeField]
    float maxHeight = 200, minHeight = -200, maxWidth = 200, minWidth = -200;

    [SerializeField]
    int numberOfCloudsToSpawn = 200;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfCloudsToSpawn; i++)
        {
            float randomHeightValue = Random.Range(0, 1.0f);
            randomHeightValue *= randomHeightValue;

            float height = Mathf.Lerp(minHeight, maxHeight, randomHeightValue);

            float width = Mathf.Lerp(minWidth, maxWidth, Random.Range(0, 1.0f));

            GameObject cloud = Instantiate<GameObject>(cloudPrefab, transform, true);
            cloud.transform.localPosition = new Vector3(width, height, 0);
        }
    }
}
