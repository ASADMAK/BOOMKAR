﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playercon : MonoBehaviour
{

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;

    }
    public List<AxleInfo> axleInfos;
    Rigidbody rb;

    
    public float maxMotorTorque;
    private float maxSteeringAngle;
    private float minsteeringAngle;
    public float maxBreakTorque;
    private float heighestspeed;
    private float speedfactor, currentsteer;
    private float distancetoground;

    private bool isgrounded = false;
    private bool turningleft;
    private bool turningright;
    private bool isbreaking;
    private bool isaccelrating;
    private bool isacid;
    private bool isreverse;
    private bool onlyonce;
    private bool onlyonceaudio;
    private bool onlyoncecrash;


    public GameObject finalindicator;
    public GameObject missionfailed, game;
    public GameObject watersplash;
    public Slider fuelmeter;
    public Slider playerhealth;

    public TextMeshProUGUI minfloopy,minfloppy2;
    public TextMeshProUGUI cointext,cointext2;
    public TextMeshProUGUI[] coinscollected;
    public TextMeshProUGUI[] fuelcollected;
    public TextMeshProUGUI[] bookscollected;

    private int floppycollected = 0;
    private int moneycollected;
    private int gascanno;
    private int stars = 0;
    private int money;
    public int numberoffloppy;
    private int vibrate;
    private int highestlevel;
    private int currentlevel;

    public AudioSource[] engine;
    public AudioSource[] carbreaks;
    public AudioSource carcrash;
    public AudioSource gascan;
    public AudioSource coinsound;
    public AudioSource keysound;
    public AudioSource levelfailed;
    public AudioSource water;
    public AudioSource splash_audio;

    public ParticleSystem splash;
    private Vector3 oldvelocity;

    int turn, forward, back,skin;
    [SerializeField]
    public void Awake()
    {
        currentlevel = SceneManager.GetActiveScene().buildIndex;
        highestlevel = PlayerPrefs.GetInt("hightest", 1);
        if(currentlevel-1>highestlevel)
        {
            PlayerPrefs.SetInt("hightest", highestlevel+1);
        }
        skin = PlayerPrefs.GetInt("Skin", 0);
        minsteeringAngle = 30;
        maxSteeringAngle = PlayerPrefs.GetFloat("steer", 30);
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindObjectOfType<tutorial>().starttutorial();
        }
        money = PlayerPrefs.GetInt("gold", 0);
        PlayerPrefs.SetInt("gold", 5000);//extra gold for testing;
        playerhealth.maxValue = PlayerPrefs.GetFloat("health", 50);
        fuelmeter.maxValue = PlayerPrefs.GetFloat("fuel", 200);
        playerhealth.value = PlayerPrefs.GetFloat("health", 50);
        fuelmeter.value = PlayerPrefs.GetFloat("fuel", 200);
        heighestspeed = PlayerPrefs.GetFloat("highestspeed", 20);
        isbreaking = false;
        turningleft = false;
        turningright = false;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, .05f);
        vibrate = PlayerPrefs.GetInt("vibration", 1);
        engine[skin].Play();
        onlyonceaudio = false;
        onlyonce = false;
        onlyoncecrash = false;
        distancetoground = GetComponent<Collider>().bounds.extents.y;

    }
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {

        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;

    }

    public void FixedUpdate()
    {
        if (isgrounded == true)
        {
            speedfactor = rb.velocity.magnitude / heighestspeed;
            currentsteer = Mathf.Lerp(maxSteeringAngle, minsteeringAngle, speedfactor);
            float motor = maxMotorTorque * forward;
            float steering = currentsteer * turn;
            float breaking = maxBreakTorque;

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    if (isreverse == false)
                    {
                        if (isbreaking == false)
                        {
                            axleInfo.leftWheel.motorTorque = motor;
                            axleInfo.rightWheel.motorTorque = motor;
                            axleInfo.leftWheel.brakeTorque = 0;
                            axleInfo.rightWheel.brakeTorque = 0;
                            if (axleInfo.rightWheel.rpm < 0)
                            {
                                if (isaccelrating == true)
                                {
                                    axleInfo.leftWheel.motorTorque = 10 * motor;
                                    axleInfo.rightWheel.motorTorque = 10 * motor;
                                }

                            }
                        }
                        else if (isbreaking == true)
                        {

                            axleInfo.leftWheel.brakeTorque = breaking;
                            axleInfo.rightWheel.brakeTorque = breaking;
                            axleInfo.leftWheel.motorTorque = 0;
                            axleInfo.rightWheel.motorTorque = 0;
                            if (rb.velocity.magnitude <= 1)
                            {
                                isreverse = true;
                                forward = -1;
                            }
                        }
                    }
                    else if (isreverse == true)
                    {
                        axleInfo.leftWheel.brakeTorque = 0;
                        axleInfo.rightWheel.brakeTorque = 0;
                        axleInfo.leftWheel.motorTorque = motor;
                        axleInfo.rightWheel.motorTorque = motor;
                    }
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
            if (rb.velocity.magnitude > heighestspeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, heighestspeed);
            }
        }
        if (!Physics.Raycast(transform.position, -Vector3.up, distancetoground + .5f))
        {
            isgrounded = false;

        }
        else
        {
            isgrounded = true;


        }

    }
    public void Update()
    {
        
        cointext.text = money.ToString();
        cointext2.text = money.ToString();
        fuelmeter.value -= Time.deltaTime;
        if (fuelmeter.value > 0)
        {
            if (turningleft == true && turningright == false)
            {
                turn = -1;
            }
            else if (turningright == true && turningleft == false)
            {
                turn = 1;
            }
            else if (turningleft == false && turningright == false)
            {
                turn = 0;
            }
        }
        else
        {
            gameover();
        }
        if (floppycollected >= numberoffloppy)
        {
            finalindicator.SetActive(true);
            FindObjectOfType<complete>().done();
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (onlyonce == false)
                {
                    
                    FindObjectOfType<tutorial>().tutorial6active();
                    onlyonce = true;
                }
            }
        }
        if (isacid == true)
        {
            playerhealth.value -= Time.deltaTime;
            if (playerhealth.value <= 0)
            {

                gameover();
            }
        }
        else if(isacid==false)
        {
            water.Stop();
            playerhealth.value += Time.deltaTime;
            if (playerhealth.value >= PlayerPrefs.GetFloat("health", 50))
            {

                playerhealth.value = PlayerPrefs.GetFloat("health", 50);
            }
        }
        playaudio();
    }
    public void leftturn()
    {
        turningleft = true;
    }
    public void rightturn()
    {
        turningright = true;
    }
    public void straight()
    {
        turningleft = false;
        turningright = false;
    }
    public void refill_Fuel()
    {
        fuelmeter.value += 30;
        gascanno++;
        if(!gascan.isPlaying)
        {
            gascan.Play();
        }
        if(vibrate==1)
        Handheld.Vibrate();
    }
    public void tirebreak()
    {
        if (isgrounded == true)
        { 
        isbreaking = true;
        if (!carbreaks[skin].isPlaying)
            carbreaks[skin].Play();
    }
    }
    public void tirebreakoff()
    {

        isbreaking = false;
        isreverse = false;
        forward = 0;
        carbreaks[skin].Stop();
    }
    public void booston()
    {
        forward = 1;
        isaccelrating = true;
    }
    public void boostoff()
    {
        forward = 0;
        isaccelrating = false;
    }
    public void floppygot()
    {
        floppycollected++;
        if(!keysound.isPlaying)
        {
            keysound.Play();
        }
        if (vibrate == 1)
            Handheld.Vibrate();
    }
    public void acidin()
    {
        isacid = true;
        watersplash.SetActive(true);
        if(!water.isPlaying)
        {
            water.Play();
        }
        if(onlyonce==false)
        {
            splash.Play();
            splash_audio.Play();
            onlyonce = true;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindObjectOfType<tutorial>().tutorial4active();
        }
    }
    public void acidout()
    {
        isacid = false;
        watersplash.SetActive(false);
        water.Stop();
        onlyonce = false;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindObjectOfType<tutorial>().tutorial5active();
        }
    }
    public void increasegold()
    {
        money++;
        moneycollected++;
        PlayerPrefs.SetInt("gold", money);
        if(!coinsound.isPlaying)
        {
            coinsound.Play();
        }
        if (vibrate == 1)
            Handheld.Vibrate();
    }
    public void gamepaused()
    {
        Time.timeScale = 0;
        engine[skin].Stop();
    }
    public void gameison()
    {
        Time.timeScale = 1;
        engine[skin].Play();

    }
    public void gameover()
    { 
        game.SetActive(false);
        missionfailed.SetActive(true);
        if (!levelfailed.isPlaying)
        {
            if (onlyonceaudio == false)
            {

                FindObjectOfType<unityads>().showad();
                textto();
                levelfailed.Play();
                onlyonceaudio = true;
            }
        }
        rb.mass = 100000;

    }

    public void playaudio()
    {
        if (speedfactor < .1)
            speedfactor = .1f;
        engine[skin].pitch = speedfactor;
        minfloopy.text = floppycollected.ToString();
        minfloppy2.text = floppycollected.ToString();
    }

    public void doubleincreasegold()
    {
        money += 10;
        moneycollected += 10;
        PlayerPrefs.SetInt("gold", money);
        if (!coinsound.isPlaying)
        {
            coinsound.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "buidling")
        {
            Debug.Log("budidling");
            if (onlyoncecrash == false)
            {
                if (!carcrash.isPlaying)
                {
                    carcrash.Play();
                    carcrash.volume = speedfactor;
                }
                onlyoncecrash = true;
            }
        }
        else if(other.tag=="water")
        {
            acidin();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water")
        {
            acidout();
        }
        else if (other.tag == "buidling")
        {
            onlyoncecrash = false;
        }
    }
    public void doublecoin()
    {
        money += moneycollected;
        PlayerPrefs.SetInt("gold", money);
    }
    public void revived()
    {
        game.SetActive(true);
        missionfailed.SetActive(false);
        playerhealth.value = PlayerPrefs.GetFloat("health", 50);
        fuelmeter.value = PlayerPrefs.GetFloat("fuel", 200);
        onlyonceaudio = false;
        rb.mass = 3000;
    }
    public void playerstationary()
    {
        transform.position = new Vector3(-1, 1.73f, -232.87f);
    }
    public void textto()
    {
        for (int i = 0; i < coinscollected.Length; i++)
        {
            coinscollected[i].text = moneycollected.ToString();
        }
        for (int i = 0; i < fuelcollected.Length; i++)
        {
            fuelcollected[i].text = gascanno.ToString();
        }
        for (int i = 0; i < bookscollected.Length; i++)
        {
            bookscollected[i].text = floppycollected.ToString();
        }
        
    }

}

