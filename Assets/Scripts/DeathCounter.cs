using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    public int deathCounter = 0;
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    private bool isFading = false;
    public bool isDying = false;
    void Start()
    {
        deathCounter = PlayerPrefs.GetInt("deathCounter");
        fadeImage = GameObject.FindGameObjectWithTag("deathcanva").GetComponent<Image>();
    }
    public void Died()
    {
        deathCounter += 1;
        PlayerPrefs.SetInt("deathCounter",deathCounter);
    }
    public void Die()
    {
        StartCoroutine(FadeAndKill());
    }
      IEnumerator FadeAndKill()
    {
        isDying = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -10f);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Dash>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("dead",true);
        GetComponent<PlayerAnimation>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider2D>().size = new Vector2(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider2D>().size.x,1.390677f);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetInteger("moving",0);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isDashing",false);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isRunning",false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<DashTrail>().SetEnabled(false);
        GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;

        isFading = true;
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        color.a = 1f;
        fadeImage.color = color;

        yield return new WaitForSeconds(1f);
        Died();
        SceneManager.LoadScene(1);
    }
}
