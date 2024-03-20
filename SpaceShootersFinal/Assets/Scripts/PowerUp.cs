using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public abstract class PowerUp: ScriptableObject
{
        
    public string powerUpName;
    public float duration;

    public abstract void Activate();
    public abstract void Deactivate();

    protected IEnumerator RemoveAfterTime(GameObject player)
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }
}