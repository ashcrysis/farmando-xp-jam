using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dialogue_with_portrait : MonoBehaviour
{
    public TextMeshProUGUI speakerNameComponent;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public string[] speakers;

    public float textSpeed;
    public int index;
    [SerializeField] public Sprite[] portraits;
    [SerializeField]public Image imageComponent;

    private AudioSource audio;
    private float oldnavmeshspeed;
    private static bool buttonclicked = false;
    List<char> listPause = new List<char> { ',', '.','?' };
    public string playerName;
   void Start(){
    playerName = System.Environment.UserName;
    for(int i = 0; i < lines.Length; i++) {
        lines[i] = lines[i].Replace("$playerName", playerName);
    }
}
    void OnEnable()
    {
        speakerNameComponent.text = speakers[index];
        textComponent.text = string.Empty;
        audio = GetComponent<AudioSource>();
        StartDialogue();
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().enabled = false;

              if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsGrounded()){
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<DashTrail>().SetEnabled(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0,GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity.y);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetInteger("moving",0);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isDashing",false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetBool("isRunning",false);
            var enemies = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled =  false;
            }

        }

        if (buttonclicked)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();

                textComponent.text = lines[index];

            }
        buttonclicked = false;
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

IEnumerator TypeLine()
{
    string line = lines[index];
    textComponent.text = string.Empty;
    imageComponent.sprite = portraits[index];
    int tagStart = 0;
    int tagEnd = 0;
    bool containsColourTag = false;

    for (int i = 0; i < line.Length; i++)
    {
        if (i % 2 == 0 && !listPause.Contains(line[i])){

        audio.Play();
        }
        if (line[i] == '.'){
            audio.Play();
            yield return new WaitForSeconds(0.2f);
        }
        if (line[i] == '<')
        {
            tagEnd = line.IndexOf('>', i);

            if (tagEnd != -1)
            {
                string tag = line.Substring(i, tagEnd - i + 1);
                textComponent.text += tag;
                // Check if it's a color tag
                if (tag.ToLower().Contains("color"))
                {
                    // Check if it's a red, yellow, or orange color tag
                    if (tag.ToLower().Contains("red") || tag.ToLower().Contains("yellow") || tag.ToLower().Contains("orange") || tag.ToLower().Contains("blue"))
                    {
                        containsColourTag = true;
                    }
                    else
                    {
                        containsColourTag = false;
                    }
                }

                i = tagEnd;

                yield return null; // Wait for one frame before processing the next character.
            }
        }
        else
        {
            if (containsColourTag)
            {
                textComponent.text += line[i];
            }
            else
            {
                textComponent.text += "<color=white>" + line[i] + "</color>";
            }

            yield return new WaitForSeconds(textSpeed);
        }
        if (listPause.Contains(line[i])){
            yield return new WaitForSeconds(0.4f);
        }
    }
textComponent.text = lines[index];
}

public void OnButtonClick(){
    ButtonClick();
    //Debug.Log("Has Clicked");
}
void ButtonClick(){
    //Debug.Log(buttonclicked);
    buttonclicked = true;
}


   void NextLine()
{
    if (index < lines.Length - 1)
    {
        index++;
        speakerNameComponent.text = speakers[index];
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());

    }
    else
    {
        gameObject.SetActive(false);
        if (gameObject.CompareTag("starterdialogue"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<IdleWatcher>().gameStarted = true;
            PlayerPrefs.SetInt("gameStarted",1);
            GameObject.FindGameObjectWithTag("Timer").GetComponent<TMP_Text>().enabled = true;
            GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().enabled = true;
        }
        GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().enabled = true;

        index = 0;
        speakerNameComponent.text = speakers[index]; // Resetando o falante para o primeiro da lista
        textComponent.text = lines[index];
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().enabled = true;
        var enemies = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled =  true;
            }
    }
}

}
