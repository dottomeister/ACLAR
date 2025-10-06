using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] pickupPrefabArray;

    [SerializeField]
    float maxHeight = 200, minHeight = -200, maxWidth = 200, minWidth = -200;

    [SerializeField]
    int numberOfPickupsToSpawn = 200;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfPickupsToSpawn; i++)
        {
            float randomHeightValue = Random.Range(0, 1.0f);

            float height = Mathf.Lerp(minHeight, maxHeight, randomHeightValue);

            float width = Mathf.Lerp(minWidth, maxWidth, Random.Range(0, 1.0f));

            GameObject pickup = Instantiate<GameObject>(pickupPrefabArray[Random.Range(0, pickupPrefabArray.Length)], transform, true);
            pickup.transform.localPosition = new Vector3(width, height, 0);
        }
    }
}
