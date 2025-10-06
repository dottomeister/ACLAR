using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class
MinimapController : MonoBehaviour
{
    [SerializeField]
    private Image spaceshipImage;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private TextMeshProUGUI heightText;

    // Update is called once per frame
    void FixedUpdate()
    {
        float percentage = Mathf.Clamp01((player.transform.position.y / 1000) + 1);

        heightText.text = "Height: " + Math.Truncate(player.transform.position.y + 1000).ToString();
        spaceshipImage.transform.localPosition = new Vector3(spaceshipImage.transform.localPosition.x, Mathf.Lerp(-150, 150, percentage), spaceshipImage.transform.localPosition.z);
    }
}
