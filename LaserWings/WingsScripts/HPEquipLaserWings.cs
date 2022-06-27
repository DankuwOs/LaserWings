using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPEquipLaserWings : HPEquippable, IMassObject
{
    // Yoinked from Tempyz78 :~)
    
    
    
    public GetTeam getTeam;
    
    public ParticleSystem[] bluForParticles;
    public ParticleSystem[] opForParticles;

    public WingsTouch[] bluForWings;
    public WingsTouch[] opForWings;
    
    public AudioSource source;
    public bool ActiveOnStart = false;
    
    public BoolEvent firedEvent = new BoolEvent();
    
    private ParticleSystem[] _particleSystems;
    private WingsTouch[] _touchies;
    
    private bool fire;
    private WeaponManager _wm;
    
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

    protected override void OnEquip()
    {
        _wm = weaponManager;
        
        switch (getTeam.GetMyTeam())
        {
            case "Allied":
                _particleSystems = bluForParticles;
                _touchies = bluForWings;
                Debug.Log("Player is Allied");
                break;
            case "Enemy":
                _particleSystems = opForParticles;
                _touchies = opForWings;
                Debug.Log("Player is Enemy");
                break;
            
            default:
            Debug.Log("Error: Team not found.");
            _particleSystems = opForParticles;
            _touchies = opForWings;
                break;
        }

        
        
        if (!ActiveOnStart)
        {
            if (SceneManager.GetActiveScene().name != VTOLScenes.VehicleConfiguration.ToString())
            {
                foreach (ParticleSystem ps in _particleSystems)
                {
                    ps.Stop();
                }
            }

            foreach (WingsTouch c in _touchies)
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
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Play();
        }

        foreach (WingsTouch c in _touchies)
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

        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Stop();
        }

        foreach (WingsTouch c in _touchies)
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