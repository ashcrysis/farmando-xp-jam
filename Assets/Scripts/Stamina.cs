using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float stamina;
    public float MaxStamina = 100;
    public float staminaLossBase = 10;
    public float staminaDashValue;
    public float staminaRunValue;
    public float staminaJumpValue;
    public PlayerMovement playerMovement;

    public Dash dash;
    void Start()
    {
        stamina = MaxStamina;
        staminaDashValue = staminaLossBase*2;
        staminaRunValue = staminaLossBase;
        staminaJumpValue = staminaLossBase;
    }
void Update(){
    if (stamina > MaxStamina){
        stamina = MaxStamina;
    }
}
    public void Actions(int actionID){
        switch (actionID){

            case 0:
                if (stamina > 0){
                    stamina -= staminaRunValue  * Time.fixedDeltaTime;
                }
            break;

            case 1:
                if (stamina > 0){
                    stamina -= staminaDashValue;
                }
            break;

            case 2:
                if (stamina < MaxStamina && !dash.isDashing){
                    if (playerMovement.moving == 0){
                        stamina += 8 * Time.fixedDeltaTime;
                    }
                     if (playerMovement.moving == 1){
                        stamina += 2 * Time.fixedDeltaTime;
                    }
                }
            break;
              case 3:
                if (stamina > 0){
                    stamina -= staminaJumpValue;
                }
            break;

        }
    }
}
