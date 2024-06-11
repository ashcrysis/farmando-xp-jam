using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callElevator : MonoBehaviour
{
    [SerializeField] private float minDistance;
    public GameObject elevator;
    public bool canInteract;
    private Transform elevatorStartPosition;
    private Transform elevatorEndPosition;
    [SerializeField] private GameObject botao;

    void Start()
    {
        elevatorStartPosition = elevator.GetComponent<Elevator>().starterPosition;
        elevatorEndPosition = elevator.GetComponent<Elevator>().endPosition;
    }

    void LateUpdate()
    {
        canInteract = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < minDistance;

        botao.SetActive(canInteract);

       
        if (Input.GetKeyDown(KeyCode.C) && !GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying && !IsDialogueActive())
        {
            float threshold = 1f;
            if (Vector2.Distance(elevator.transform.position, elevatorStartPosition.position) < threshold)
            {
                StartCoroutine(elevatorChange(1f,elevatorEndPosition));
                return;
            }
            if (Vector2.Distance(elevator.transform.position, elevatorEndPosition.position) < threshold)
            {
                StartCoroutine(elevatorChange(1f,elevatorStartPosition));
                return;
            }
        }
        
    }
    private IEnumerator elevatorChange(float delay, Transform destiny){
        Elevator elevatorComponent = elevator.GetComponent<Elevator>();
        yield return new WaitForSeconds(delay);
        elevatorComponent.destiny = destiny;
    }
    public void SetBotao(bool situation)
    {
        this.botao.SetActive(situation);
    }

    bool IsDialogueActive()
    {
        GameObject[] dialogueObjects = GameObject.FindGameObjectsWithTag("dialogue");
        foreach (GameObject obj in dialogueObjects)
        {
            if (obj.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
