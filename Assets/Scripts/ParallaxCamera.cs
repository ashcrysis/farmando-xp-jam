using UnityEngine;
 
[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private Vector3 oldPosition;

    void Start()
    {
        oldPosition = transform.position;
    }

    void Update()
    {
        if (transform.position != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                Vector2 delta = oldPosition - transform.position;
                onCameraTranslate(delta);
            }

            oldPosition = transform.position;
        }
    }
}
