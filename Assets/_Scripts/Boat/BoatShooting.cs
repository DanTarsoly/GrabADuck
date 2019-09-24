using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatShooting : MonoBehaviour {

    public int PlayerNumber = 1;
    public Rigidbody BombPrefab;
	public Rigidbody SpikeBombPrefab;
    public Rigidbody SoapBombPrefab;
    public Transform LeftFireTransform;
    public Transform RightFireTransform;
    public Transform BackFireTransform;
    public Slider LeftAimSlider;
    public Slider RightAimSlider;
    public AudioSource ShootingAudio;
    public AudioClip ChargingClip;
    public AudioClip ShootClip;
    public AudioClip MineClip;
    public float MinLaunchForce = 15f;
    public float MaxLaunchForce = 30f;
    public float MaxChargeTime = 0.75f;
    public float LoatTime = 1f;
    public BombType BombType = BombType.BOMB;
    public int SpecialCount;
    public GameObject ShootSmokePrefab;

	private string RightBubbleBomb;
	private string LeftBubbleBombButton;
	private string SpikeBombButton;
    private string LeftFireButton;
    private string RightFireButton;
    private float LeftCurrentLaunchForce;
    private float RightCurrentLaunchForce;
    private float ChargeSpeed;
    private float LeftCurrentLoadTime;
    private float RightCurrentLoadTime;
    private bool LeftFired;
    private bool RightFired;

    void Start()
    {
        LeftFireButton = "FireLeft" + PlayerNumber;
        RightFireButton = "FireRight" + PlayerNumber;
        ChargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;
    }

    void OnEnable()
    {
        LeftCurrentLaunchForce = MinLaunchForce;
        RightCurrentLaunchForce = MinLaunchForce;
        if (LeftAimSlider != null)
            LeftAimSlider.value = MinLaunchForce;
        if (RightAimSlider != null)
            RightAimSlider.value = MinLaunchForce;
        BombType = BombType.BOMB;
    }
    
    void Update()
    {
        if (LeftAimSlider != null)
            LeftAimSlider.value = LeftCurrentLaunchForce;
        if (RightAimSlider != null)
            RightAimSlider.value = RightCurrentLaunchForce;
        LeftCurrentLoadTime -= Time.deltaTime;
        RightCurrentLoadTime -= Time.deltaTime;

        CheckLeftShooting();
        CheckRightShooting();
    }

    private void CheckLeftShooting()
    {

        if (BombType == BombType.SPIKEBOMB && Input.GetButtonDown(LeftFireButton))
            FireLeft();

        if (LeftCurrentLoadTime > 0)
            return;
        
        if (LeftCurrentLaunchForce >= MaxLaunchForce)
        {
            LeftCurrentLaunchForce = MaxLaunchForce;
            FireLeft();
        }
        else if (Input.GetButtonDown(LeftFireButton))
          {
            LeftFired = false;
            LeftCurrentLaunchForce = MinLaunchForce;
            ShootingAudio.clip = ChargingClip;
            ShootingAudio.volume = 0.2f;
            ShootingAudio.Play();
        }
        else if (Input.GetButtonUp(LeftFireButton))
        {
            FireLeft();
        }
        else if (Input.GetButton(LeftFireButton))
        {
            LeftCurrentLaunchForce += ChargeSpeed * Time.deltaTime;
        }
    }
    private void CheckRightShooting()
    {
        if (BombType == BombType.SPIKEBOMB && Input.GetButtonDown(RightFireButton))
            FireRight();

        if (RightCurrentLoadTime > 0)
            return;

        if (RightCurrentLaunchForce >= MaxLaunchForce)
        {
            RightCurrentLaunchForce = MaxLaunchForce;
            FireRight();
        }
        else if (Input.GetButtonDown(RightFireButton))
        {
            RightFired = false;
            RightCurrentLaunchForce = MinLaunchForce;
            ShootingAudio.volume = 0.2f;
            ShootingAudio.clip = ChargingClip;
            ShootingAudio.Play();
        }
        else if (Input.GetButtonUp(RightFireButton))
        {
            FireRight();
        }
        else if (Input.GetButton(RightFireButton))
        {
            RightCurrentLaunchForce += ChargeSpeed * Time.deltaTime;
        }
    }

	private void PlantSpikeBomb ()
	{
		Instantiate (SpikeBombPrefab, gameObject.transform.position - Vector3.Normalize (gameObject.transform.forward) * 2, gameObject.transform.rotation);
	}

    private void FireLeft()
    {
        if (SpecialCount <= 0)
            BombType = BombType.BOMB;

        GameObject shootSmoke;
        switch (BombType)
        {
            case BombType.SPIKEBOMB:
                Rigidbody spikeBombInstance = Instantiate(SpikeBombPrefab, BackFireTransform.transform.position, BackFireTransform.rotation) as Rigidbody;
                ShootingAudio.clip = MineClip;
                ShootingAudio.volume = 1;
                --SpecialCount;
                break;
            case BombType.SOAPBOMB:
                Rigidbody soapBombInstance = Instantiate(SoapBombPrefab, LeftFireTransform.transform.position, LeftFireTransform.rotation) as Rigidbody;
                soapBombInstance.velocity = LeftCurrentLaunchForce * LeftFireTransform.forward;
                shootSmoke = Instantiate(ShootSmokePrefab, LeftFireTransform.transform.position, LeftFireTransform.rotation);
                Destroy(shootSmoke, 1);
                ShootingAudio.clip = ShootClip;
                ShootingAudio.volume = 1;
                --SpecialCount;
                break;
            default:
                Rigidbody bombInstance = Instantiate(BombPrefab, LeftFireTransform.transform.position, LeftFireTransform.rotation) as Rigidbody;
                bombInstance.velocity = LeftCurrentLaunchForce * LeftFireTransform.forward;
                shootSmoke = Instantiate(ShootSmokePrefab, LeftFireTransform.transform.position, LeftFireTransform.rotation);
                Destroy(shootSmoke, 1);
                ShootingAudio.volume = 1f;
                ShootingAudio.clip = ShootClip;
                break;
        }

        ShootingAudio.Play();

        LeftFired = true;
        LeftCurrentLoadTime = LoatTime;
        LeftCurrentLaunchForce = MinLaunchForce;
    }

    private void FireRight()
    {
        if (SpecialCount <= 0)
            BombType = BombType.BOMB;

        GameObject shootSmoke;
        switch (BombType)
        {
            case BombType.SPIKEBOMB:
                Rigidbody spikeBombInstance = Instantiate(SpikeBombPrefab, BackFireTransform.transform.position, BackFireTransform.rotation) as Rigidbody;
                ShootingAudio.volume = 1f;
                ShootingAudio.clip = MineClip;
                --SpecialCount;
                break;
            case BombType.SOAPBOMB:
                Rigidbody soapBombInstance = Instantiate(SoapBombPrefab, RightFireTransform.transform.position, RightFireTransform.rotation) as Rigidbody;
                soapBombInstance.velocity = RightCurrentLaunchForce * RightFireTransform.forward;
                shootSmoke = Instantiate(ShootSmokePrefab, RightFireTransform.transform.position, RightFireTransform.rotation);
                Destroy(shootSmoke, 1);
                ShootingAudio.volume = 1f;
                ShootingAudio.clip = ShootClip;
                --SpecialCount;
                break;
            default:
                Rigidbody bombInstance = Instantiate(BombPrefab, RightFireTransform.transform.position, RightFireTransform.rotation) as Rigidbody;
                bombInstance.velocity = RightCurrentLaunchForce * RightFireTransform.forward;
                shootSmoke = Instantiate(ShootSmokePrefab, RightFireTransform.transform.position, RightFireTransform.rotation);
                Destroy(shootSmoke, 1);
                ShootingAudio.volume = 1f;
                ShootingAudio.clip = ShootClip;
                break;
        }

        ShootingAudio.Play();

        RightFired = true;
        RightCurrentLoadTime = LoatTime;
        RightCurrentLaunchForce = MinLaunchForce;
    }
}
