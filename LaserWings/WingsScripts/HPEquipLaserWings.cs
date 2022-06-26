using UnityEngine;
using UnityEngine.SceneManagement;

public class HPEquipLaserWings : HPEquippable, IMassObject
{
    // Yoinked from Tempyz78 :~)
    
    private bool fire;
    public ParticleSystem[] ParticleSystems;
    public WingsTouch[] Touchies;
    public AudioSource source;
    public bool ActiveOnStart = false;
    
    public BoolEvent firedEvent = new BoolEvent();

    public HPEquipLaserWings()
    {
        fullName = "LASER TWO: ELECTRIC BOOGY BOOGY";
        shortName = "Laser Wing";
        unitCost = 15000f;
        description = "Chocolate tastes best with whipped cream and lots of szechuan pepper.";
        subLabel = "Poo";
        armable = true;
        armed = true;
        jettisonable = false;
        allowedHardpoints = "15";
        baseRadarCrossSection = 0f;
    }

    public void Awake()
    {
        if (!ActiveOnStart)
        {
            if (SceneManager.GetActiveScene().name != VTOLScenes.VehicleConfiguration.ToString())
            {
                foreach (ParticleSystem ps in ParticleSystems)
                {
                    ps.Stop();
                }
            }

            foreach (WingsTouch c in Touchies)
            {
                c.enabled = false;
            }

            source.Stop();
        }
    }

    public void Fire(bool fire)
    {
        if (fire)
            OnStartFire();
        else
            OnStopFire();
    }

    public override void OnStartFire()
    {
        base.OnStartFire();
        fire = true;
        foreach (ParticleSystem ps in ParticleSystems)
        {
            ps.Play();
        }

        foreach (WingsTouch c in Touchies)
        {
            c.enabled = true;
        }
        source.Play();
        
        firedEvent.Invoke(true);
    }

    public override void OnStopFire()
    {
        base.OnStopFire();
        fire = false;

        foreach (ParticleSystem ps in ParticleSystems)
        {
            ps.Stop();
        }

        foreach (WingsTouch c in Touchies)
        {
            c.enabled = false;
        }
        source.Stop();
        
        firedEvent.Invoke(false);
    }

    public float GetMass()
    {
        return 0.005f;
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fire(true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            Fire(false);
        }
    }
}