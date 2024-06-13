using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    private bool touching = false;
    private int interactionCount = 0;
    public bool isPlaying;
    public bool resetLineIndex = false;

    public GameObject[] dialogueObjects;

    void Update()
    {
        isPlaying = IsDialogueActive();
        if (GetComponentInChildren<Interavel>().canInteract)
        {
            if (Input.GetKeyDown(KeyCode.C) && !isPlaying && !GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().isDying)
            {
                interactionCount++;

                ActivateDialogues(interactionCount);

            }
        }
    }

    void ActivateDialogues(int count)
    {
        foreach (GameObject obj in dialogueObjects)
        {
            obj.SetActive(false);
        }

        if (count <= dialogueObjects.Length)
        {
            dialogueObjects[count - 1].SetActive(true);
        }
        else
        {
            dialogueObjects[dialogueObjects.Length - 1].SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            touching = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            touching = false;
        }
    }

    bool IsDialogueActive()
    {
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
