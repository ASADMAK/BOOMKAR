using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class carcontroller : MonoBehaviour
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
    public float maxSteeringAngle;
    public float minsteeringAngle;
    public float maxBreakTorque;
    public float heighestspeed;
    private float speedfactor, currentsteer;

    private bool turningleft;
    private bool turningright;
    private bool isbreaking;
    private bool isaccelrating;
    private bool isacid;
    private bool isreverse;
    private bool gameispaused = false;

    public GameObject finalindicator;
    public GameObject missionfailed, game;
    public GameObject watersplash;
    public Slider fuelmeter;
    public Slider playerhealth;

    public TextMeshProUGUI minfloopy;
    public TextMeshProUGUI maxfloopy;
    public TextMeshProUGUI cointext;
    public TextMeshProUGUI coincollected1, coincollected2, keys1, keys2;
    public TextMeshProUGUI gascan1, gascan2, coin1, coin2;

    private int floppycollected = 0;
    private int moneycollected;
    private int gascanno;
    private int stars = 0;
    private int money;
    public int numberoffloppy;

    public AudioSource engine1;
    public AudioSource carbreaks1;
    public AudioSource carcrash1;
    public AudioSource gascan;
    public AudioSource coinsound;
    public AudioSource keysound;
    public AudioSource levelcomplete;
    public AudioSource levelfailed;
    public AudioSource water;

    int turn, forward, back;
    [SerializeField]

    
    public void Start()
    {
        money = PlayerPrefs.GetInt("gold", 0);
        PlayerPrefs.SetInt("gold", 5000);//extra gold for testing;
        playerhealth.maxValue = 50;
        fuelmeter.maxValue = 200;
        isbreaking = false;
        turningleft = false;
        turningright = false;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, .05f);
        maxfloopy.text = numberoffloppy.ToString();

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
        if (gameispaused == true)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        playaudio();
    }
    public void Update()
    {
        minfloopy.text = floppycollected.ToString();
        cointext.text = money.ToString();
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
        if (gameispaused == false)
            fuelmeter.value -= Time.deltaTime;
        if (floppycollected == numberoffloppy)
        {
            finalindicator.SetActive(true);
            FindObjectOfType<complete>().done();
            gamecomplete();
        }
        if (isacid == true)
        {
            if (gameispaused == false)
                playerhealth.value -= Time.deltaTime;
            if (playerhealth.value <= 0)
            {

                gameover();
            }
        }
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
    }
    public void tirebreak()
    {
        isbreaking = true;
        if(!carbreaks1.isPlaying)
        carbreaks1.Play();
    }
    public void tirebreakoff()
    {
        isbreaking = false;
        isreverse = false;
        forward = 0;
        carbreaks1.Stop();
    }
    public void booston()
    {
        forward = 1;
    }
    public void boostoff()
    {
        forward = 0;
    }
    public void floppycollect()
    {
        maxfloopy.text = numberoffloppy.ToString();
        minfloopy.text = floppycollected.ToString();
        floppycollected++;
        if(!keysound.isPlaying)
        {
            keysound.Play();
        }
    }
    public void acidin()
    {
        isacid = true;
        watersplash.SetActive(true);
        if(!water.isPlaying)
        {
            water.Play();
        }
    }
    public void acidout()
    {
        isacid = false;
        watersplash.SetActive(false);
        water.Stop();
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
    }
    public void gamepaused()
    {
        gameispaused = true;
    }
    public void gameison()
    {
        gameispaused = false;
    }
    public void gameover()
    {
        game.SetActive(false);
        missionfailed.SetActive(true);
        keys1.text = floppycollected.ToString();
        keys2.text = floppycollected.ToString();
        coincollected1.text = moneycollected.ToString();
        coincollected2.text = moneycollected.ToString();
        if(!levelfailed.isPlaying)
        {
            levelfailed.Play();
        }
    }
    public void gamecomplete()
    {
        gascan1.text = gascanno.ToString();
        gascan2.text = gascanno.ToString();
        coin1.text = moneycollected.ToString();
        coin2.text = moneycollected.ToString();
        if(!levelcomplete.isPlaying)
        {
            levelcomplete.Play();
        }
    }
    public void playaudio()
    {
        if (speedfactor < .1)
            speedfactor = .1f;
        engine1.pitch = speedfactor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "buidling")
        {
            if(!carcrash1.isPlaying)
            carcrash1.Play();
        }
    }
}

