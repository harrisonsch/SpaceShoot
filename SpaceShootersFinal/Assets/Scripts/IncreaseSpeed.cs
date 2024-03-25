using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "IncreaseSpeed", menuName = "PowerUp/Speed/IncreaseSpeed")]
public class IncreaseSpeed : PowerUp
{
    private Coroutine speedIncreaseCoroutine;

    public IncreaseSpeed()
    {
        powerUpName = "IncreaseSpeed";
        duration = 10f; // Lasts for 10 seconds
        cost = 8f;
        description = "Increases speed by 1 every second for 10 seconds";
    }

    public override void Activate()
    {
        if (speedIncreaseCoroutine == null)
        {
            speedIncreaseCoroutine = GameController.Instance.StartCoroutine(IncreaseSpeedCoroutine());
        }
    }

    public override void Deactivate()
    {
        if (speedIncreaseCoroutine != null)
        {
            GameController.Instance.StopCoroutine(speedIncreaseCoroutine);
            speedIncreaseCoroutine = null;
        }
    }

    private IEnumerator IncreaseSpeedCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            GameController.Instance.currSpeedMult += 1;
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }
}
