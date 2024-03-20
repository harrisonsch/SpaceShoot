using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class PowerUp
{
        
    public string powerUpName;
    public float duration;

    public abstract void Activate(GameObject player);
    public abstract void Deactivate(GameObject player);

    protected IEnumerator RemoveAfterTime(GameObject player)
    {
        yield return new WaitForSeconds(duration);
        Deactivate(player);
    }
}