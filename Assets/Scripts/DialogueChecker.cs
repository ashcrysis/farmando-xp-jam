using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChecker : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(GetComponent<dialogue>()){
            GetComponent<dialogue>().OnButtonClick();
            }
            else if(GetComponent<dialogue_with_portrait>())
            {
                GetComponent<dialogue_with_portrait>().OnButtonClick();
            }
        }
    }
}
