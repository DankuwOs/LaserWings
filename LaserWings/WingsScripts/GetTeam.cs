using System;
using UnityEngine;

public class GetTeam : MonoBehaviour
{
    public GameObject bluFor;
    public GameObject opFor;
    
    
    private Actor _actor;
    public String GetMyTeam()
    {
        _actor = gameObject.GetComponentInParent<Actor>();
        if (_actor == null)
        {
            Debug.Log("GetMyTeam: Actor is null");  
        }

        var team = _actor.team;
        if (team == Teams.Allied)
        {
            bluFor.SetActive(true);
            opFor.SetActive(false);
            return "Allied";
        }
        
        if (team == Teams.Enemy)
        {
            bluFor.SetActive(false);
            opFor.SetActive(true);
            return "Enemy";
        }
        return "Null";
    }
}