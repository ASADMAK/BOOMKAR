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

    public GameObject[] marks;
    public GameObject[] star;
    public GameObject[] levelcompletestar;
    public GameObject finalindicator;
    public GameObject missionfailed, game;
    public Slider fuelmeter;
    public Slider playerhealth;

    public TextMeshProUGUI minfloopy;
    public TextMeshProUGUI maxfloopy;
    public TextMeshProUGUI cointext;

    private int floppycollected = 0;
    private int stars = 0;
    private int money;
    public int numberoffloppy;


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
            Debug.Log(rb.velocity.magnitude);
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
        if (rb.velocity.magnitude > heighestspeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, heighestspeed);
        }
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
            missionfailed.SetActive(true);
        }
        fuelmeter.value -= Time.deltaTime;
        if (floppycollected == numberoffloppy)
        {
            finalindicator.SetActive(true);
            FindObjectOfType<complete>().done();
        }
        if (isacid == true)
        {
            playerhealth.value -= Time.deltaTime;
            if (playerhealth.value <= 0)
            {
                missionfailed.SetActive(true);
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
    }
    public void tirebreak()
    {
        isbreaking = true;
    }
    public void tirebreakoff()
    {
        isbreaking = false;
    }
    public void booston()
    {
        forward = 1;
        isreverse = false;
    }
    public void boostoff()
    {
        forward = 0;
    }
    public void playerstar()
    {
        stars++;

        switch (stars)
        {
            case 1:
                star[0].SetActive(true);
                levelcompletestar[0].SetActive(true);
                break;
            case 2:
                star[1].SetActive(true);
                levelcompletestar[1].SetActive(true);
                break;
            case 3:
                star[2].SetActive(true);
                levelcompletestar[2].SetActive(true);
                break;


        }
    }
    public void floppycollect()
    {
        maxfloopy.text = numberoffloppy.ToString();
        minfloopy.text = floppycollected.ToString();
        floppycollected++;
    }
    public void acidin()
    {
        isacid = true;
    }
    public void acidout()
    {
        isacid = false;
    }
    public void increasegold()
    {
        money++;
        PlayerPrefs.SetInt("gold", money);
    }
}

