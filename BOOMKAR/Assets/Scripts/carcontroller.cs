using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    private bool onlyonce = false;

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
    private int vibrate;
    private int highestlevel;
    private int currentlevel;

    public AudioSource engine1;
    public AudioSource carbreaks1;
    public AudioSource carcrash1;
    public AudioSource gascan;
    public AudioSource coinsound;
    public AudioSource keysound;
    public AudioSource levelfailed;
    public AudioSource water;
    public AudioSource splash_audio;

    public ParticleSystem splash;
    public ParticleSystem coindouble;
    public Animator fuelanim;
    public Animator healthanim;
    private Vector3 oldvelocity;

    int turn, forward, back;
    [SerializeField]
    public void Awake()
    {
        currentlevel = SceneManager.GetActiveScene().buildIndex;
        highestlevel = PlayerPrefs.GetInt("hightest", 1);
        if(currentlevel-1>highestlevel)
        {
            PlayerPrefs.SetInt("hightest", highestlevel+1);
        }
    }

    public void Start()
    {
        money = PlayerPrefs.GetInt("gold", 0);
        PlayerPrefs.SetInt("gold", 5000);//extra gold for testing;
        playerhealth.maxValue = PlayerPrefs.GetInt("fuel",50);
        fuelmeter.maxValue = PlayerPrefs.GetInt("health",200);
        isbreaking = false;
        turningleft = false;
        turningright = false;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, .05f);
        maxfloopy.text = numberoffloppy.ToString();
        vibrate = PlayerPrefs.GetInt("vibration", 1);
        
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
                         if (axleInfo.rightWheel.rpm < 0)
                        {
                            if (isaccelrating == true)
                            {
                                axleInfo.leftWheel.motorTorque = 10*motor;
                                axleInfo.rightWheel.motorTorque = 10*motor;
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
        playaudio();
    }
    public void Update()
    {
        
        minfloopy.text = floppycollected.ToString();
        cointext.text = money.ToString();
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
        if (floppycollected == numberoffloppy)
        {
            finalindicator.SetActive(true);
            FindObjectOfType<complete>().done();
            gamecomplete();
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
            playerhealth.value += Time.deltaTime;
            if (playerhealth.value >= 50)
            {

                playerhealth.value = 50;
            }
        }
        if(fuelmeter.value<50)
        {
            fuelanim.SetBool("fuellow",true);
        }
        else if (fuelmeter.value < 50)
        {
            fuelanim.SetBool("fuellow", false);
        }
        if(playerhealth.value<20)
        {
            healthanim.SetBool("healthloww", true);
        }
        else if (playerhealth.value > 20)
        {
            healthanim.SetBool("healthloww", false);
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
        if(vibrate==1)
        Handheld.Vibrate();
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
        isaccelrating = true;
    }
    public void boostoff()
    {
        forward = 0;
        isaccelrating = false;
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
       
    }
    public void acidout()
    {
        isacid = false;
        watersplash.SetActive(false);
        water.Stop();
        onlyonce = false;
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
        Time.timeScale = 0;
    }
    public void gameison()
    {
        Time.timeScale = 1;
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
    public void revived()
    {
        game.SetActive(true);
        missionfailed.SetActive(false);
        playerhealth.value = 50;
        fuelmeter.value = 200;
    }
    public void doublecoin()
    {
        money += moneycollected;
        PlayerPrefs.SetInt("gold", money);
        coindouble.Play();
    }
}

