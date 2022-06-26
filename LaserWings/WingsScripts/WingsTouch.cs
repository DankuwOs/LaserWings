﻿using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using VTOLVR.Multiplayer;

public class WingsTouch : MonoBehaviour
{
    public AnimationCurve curve;
    public float maxDist;
    public float damage;
    public float radius;
    public Transform _rayOrigin;
    
    private Actor _actor;

    public void Start()
    {
        if (_actor == null)
            _actor = GetComponentInParent<Actor>();
        Debug.Log("WingsTouch Actor: " + _actor.name);
        if (_rayOrigin == null)
            _rayOrigin = gameObject.transform;
    }

    private void FixedUpdate()
    {
        if (_actor == null || _rayOrigin == null)
        {
            _actor = this.gameObject.GetComponentInParent<Actor>();
            _rayOrigin = this.gameObject.transform;
            Debug.Log("WingsTouch Actor: " + _actor.name);
        }
        
        Ray ray = new Ray(_rayOrigin.position, _rayOrigin.forward);
        
        var hit = Physics.SphereCastAll(ray, radius, maxDist);

        if (hit.Length == 0 || hit == null) return;
        
        foreach (var h in hit)
        {
            var hitbox = h.collider.GetComponent<Hitbox>();
            if (hitbox == null) continue;
            
            var health = hitbox.health;
            
            if (health.currentHealth <= 0 || health.actor == _actor) continue;
                
            var distance = h.distance;
            var damageCalc = damage * curve.Evaluate(distance / maxDist);
                
            Debug.Log("Dealing damage: " + damageCalc + " to " + hitbox.health.actor.name);
                
            health.Damage(damageCalc, health.transform.position, Health.DamageTypes.Impact, _actor);
        }
    }
}