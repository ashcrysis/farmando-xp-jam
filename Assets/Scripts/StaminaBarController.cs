using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{
    public RectTransform staminaBarFillRect;
    public float maxWidth; 
    private Stamina staminaScript; 

    void Start()
    {
        StartCoroutine(FindPlayerStaminaScript());
    }

    void Update()
    {
        if (staminaScript != null)
        {
            UpdateStaminaBar();
        }
    }

    IEnumerator FindPlayerStaminaScript()
    {
        while (staminaScript == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                staminaScript = player.GetComponent<Stamina>();
            }
            yield return new WaitForSeconds(0.5f); // Retry every 0.5 seconds
        }
    }

    void UpdateStaminaBar()
    {
        float currentWidth = (staminaScript.stamina / staminaScript.MaxStamina) * maxWidth;

        staminaBarFillRect.sizeDelta = new Vector2(currentWidth, staminaBarFillRect.sizeDelta.y);
    }
}
