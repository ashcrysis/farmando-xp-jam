using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeusaNoite : MonoBehaviour
{

    public Image fadeImage;
    public Image fadeText;
    public float fadeDuration = 1f; 
    private bool isPlayingAnim = false;

    public void EndGame()
    {
        StartCoroutine(FadeAndLoadScene(0));
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("endgame") == 1)
        {
            GetComponent<DialoguePlayer>().enabled = false;
        }
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C) && PlayerPrefs.GetInt("endgame") == 1)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsGrounded())
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<DashTrail>().SetEnabled(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0,GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity.y);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isJumping",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isFalling",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isJumping",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isFalling",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetInteger("moving",0);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetInteger("moving",0);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isDashing",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("isRunning",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isDashing",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isRunning",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("reset",true);
            GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Animator>()[1].SetBool("reset",true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioController>().DisableAllAudio();
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("cutscene",true);
            isPlayingAnim = true;
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().GetBool("cutscene") == true){
        if(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            if (isPlayingAnim){
            {
                StartCoroutine(FadeAndLoadScene(0));              
            }
        }
        
        }
    }
    IEnumerator FadeImage(Image img)
    {
        float elapsedTime = 0f;
        Color color = img.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            img.color = color;
            yield return null;
        }

        color.a = 1f;
        img.color = color;
    }
    IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        StartCoroutine(FadeImage(fadeImage));
        
        yield return new WaitForSeconds(fadeDuration);
        
        StartCoroutine(FadeImage(fadeText));
        
        yield return new WaitForSeconds(fadeDuration);

        SceneManager.LoadScene(sceneIndex);
    }
    
}
