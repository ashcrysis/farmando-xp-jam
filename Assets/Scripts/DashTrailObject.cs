using UnityEngine;
using System.Collections;

public class DashTrailObject : MonoBehaviour
{
    public SpriteRenderer mRenderer;
    public Color mStartColor, mEndColor;

    private bool mbInUse;
    private Vector2 mPosition;
    private float mDisplayTime;
    private float mTimeDisplayed;
    private DashTrail mSpawner;
    private Shader shaderGUItext;
	private Shader shaderSpritesDefault;

    // Use this for initialization
    void Start ()
    {
        mRenderer.enabled = false;
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (mbInUse)
        {
            transform.position = mPosition;

            mTimeDisplayed += Time.deltaTime;

            mRenderer.color = Color.Lerp (mStartColor, mEndColor, mTimeDisplayed / mDisplayTime);
            mRenderer.gameObject.transform.localScale = new Vector2(1,1);
            mRenderer.flipX = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>().flipX;
            if (mTimeDisplayed >= mDisplayTime)
            {
                mSpawner.RemoveTrailObject (gameObject);
                mbInUse = false;
                mRenderer.enabled = false;
                //Destroy (gameObject);
            }
        }
    }

    public void Initiate (float displayTime, Sprite sprite, Vector2 position, DashTrail trail)
    {
        mDisplayTime = displayTime;
        mRenderer.sprite = sprite;
        mRenderer.enabled = true;
        mPosition = position;
        mTimeDisplayed = 0;
        mSpawner = trail;
        mbInUse = true;
    }
}
