using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class
PlayerController : MonoBehaviour
{
    [SerializeField]
    private float fuelAmount;

    [SerializeField]
    private float fuelUsageRate;

    [SerializeField]
    private float thrustForce;

    [SerializeField]
    private float torque;

    [SerializeField]
    private GameObject exhaust;

    [SerializeField]
    private TextMeshProUGUI fuelIndicator;
    
    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera camera;

    private Cinemachine.CinemachineBasicMultiChannelPerlin perlinChannel;

    [SerializeField]
    private GameObject retryButton;

    [SerializeField]
    private GameObject quitButton;

    [HideInInspector]
    public Rigidbody2D rigidbody;

    private bool loseControl;
    private float loseControlTimer;

    private bool isSlowmoActive;
    private float isSlowmoActiveTimer;

    private float checkEndingTimer;
    
    [SerializeField]
    public float initialSpeed;

    [SerializeField]
    public float maxSpeed;

    [SerializeField]
    public float shakeAmmount;

    private AudioSource jetAudioSource;

    float sinWaveTime = 0;

    [SerializeField]
    float disruption = 50;

    private bool touchGround;


    void
    Start()
    {
        loseControl = false;
        loseControlTimer = 3.0f;

        isSlowmoActive = false;
        isSlowmoActiveTimer = 3.0f;

        checkEndingTimer = 2.0f;

        exhaust.SetActive(false);

        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = new Vector2(0, -initialSpeed);

        perlinChannel = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        jetAudioSource = GetComponent<AudioSource>();

        resultText.enabled = false;
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        // retryButton.GetComponent<Renderer>().enabled = false;
        // quitButton.GetComponent<Renderer>().enabled = false;

        touchGround = false;
    }

    void
    FixedUpdate()
    {
        exhaust.SetActive(false);
        perlinChannel.m_AmplitudeGain = 0;
        jetAudioSource.Pause();

        sinWaveTime += Time.deltaTime / 15;

        if(sinWaveTime > 1)
        {
            sinWaveTime -= 1;
        }

        rigidbody.AddTorque((Mathf.Sin(Mathf.PI * 2 * sinWaveTime) + Mathf.Cos(Mathf.PI * 2 * (sinWaveTime + 0.2f) * 1.3f)) * disruption);

        if (isSlowmoActive)
        {
            if (Time.timeScale > 0.5)
            {
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
            else if (isSlowmoActiveTimer >= 0)
            {
                isSlowmoActiveTimer -= Time.deltaTime;
            }
            else
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02F;
                isSlowmoActive = false;
                isSlowmoActiveTimer = 3;
            }
        }

        if (loseControl)
        {
            loseControlTimer -= Time.deltaTime;

            if (loseControlTimer <= 0)
            {
                loseControl = false;
                loseControlTimer = 3;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A)) 
                rigidbody.AddTorque(torque * Time.deltaTime);
            if (Input.GetKey(KeyCode.D))
                rigidbody.AddTorque(-torque * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space))
            {
                if (fuelAmount > 0)
                {
                    exhaust.SetActive(true);

                    fuelAmount -= fuelUsageRate;
                    fuelIndicator.text = Math.Truncate(fuelAmount) + "%";
                    
                    Vector3 forceToAdd = transform.up * thrustForce;
                    rigidbody.AddForce(forceToAdd);
                    perlinChannel.m_AmplitudeGain = shakeAmmount;
                    jetAudioSource.Play();
                }
            }

            if (rigidbody.velocity.y < -maxSpeed)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxSpeed);
        }
        
        if (touchGround)
        {
            if (rigidbody.velocity.y <= 0.5f)
            {
                checkEndingTimer -= Time.deltaTime;

                if (checkEndingTimer <= 0)
                {
                    rigidbody.velocity = Vector2.zero;

                    if (rigidbody.transform.up.y < 0.95f)
                        resultText.text = "Better luck next time :(";

                    resultText.enabled = true;
                    retryButton.SetActive(true);
                    quitButton.SetActive(true);
                }
            }
            
            Debug.Log(rigidbody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Suelo")
        {
            loseControl = true;
            loseControlTimer = 999999;
            touchGround = true;
        }
    }

    public void AddFuel(float _fuelAmount)
    {
        fuelAmount += _fuelAmount;
        if (fuelAmount > 100)
        {
            fuelAmount = 100;
        }

        fuelIndicator.text = fuelAmount.ToString() + "%";
    }


    public void SetLoseControl()
    {
        loseControl = true;
    }

    public void SetSlowmo()
    {
        isSlowmoActive = true;
    }
}
