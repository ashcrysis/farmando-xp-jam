using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float stamina;
    public float MaxStamina = 100;
    public float staminaLossBase = 10;
    public float staminaDashValue = 20;
    public float staminaRunValue = 10;
    public float staminaJumpValue = 15;
    public PlayerMovement playerMovement;

    public Dash dash;
    void Start()
    {
        stamina = MaxStamina;
    }
void Update(){
    if (stamina > MaxStamina){
        stamina = MaxStamina;
    }
}
    public void Actions(int actionID){
        switch (actionID){

            case 0: // Corrida
                if (stamina > 0){
                    stamina -= staminaRunValue  * Time.fixedDeltaTime;
                }
            break;

            case 1: // Dash
                if (stamina > 0){
                    stamina -= staminaDashValue;
                }
            break;

            case 2: // Recuperando stamina (quando tá parado recupera mais, e quando ta andando recupera menos, nao recupera ao correr)
                if (stamina < MaxStamina && !dash.isDashing){
                    if (playerMovement.moving == 0){
                        stamina += 8 * Time.fixedDeltaTime;
                    }
                     if (playerMovement.moving == 1){
                        stamina += 2 * Time.fixedDeltaTime;
                    }
                }
            break;
              case 3: //Pulo
                if (stamina > 0){
                    stamina -= staminaJumpValue;
                }
            break;

        }
    }
}
