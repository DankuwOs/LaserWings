using UnityEngine;
using UnityEngine.Events;
using VTNetworking;

public class WingsSync : VTNetSyncRPCOnly
{
    // Yoinked from Tempyz78 :~)
    private HPEquipLaserWings _laserWings;
    
    protected override void OnNetInitialized()
    {
        if (netEntity == null)
            Debug.LogError("WingsSync has no netEntity!");
        _laserWings = GetComponent<HPEquipLaserWings>();
        if (isMine)
            _laserWings.firedEvent.AddListener(new UnityAction<bool>(FiredWings));
    }

    public void FiredWings(bool fired)
    {
        if (isMine)
        {
            SendRPC("RPC_FiredWings", new object[] { fired? 1 : 0 });
        }
    }
    
    [VTRPC]
    public void RPC_FiredWings(int fired)
    {
        bool shouldFire = fired == 1;
        if (!isMine) // yes, i am adding in checks for the unmp mod inside of a mod that no one will ever try to hack
            _laserWings.Fire(shouldFire);	
    }
}