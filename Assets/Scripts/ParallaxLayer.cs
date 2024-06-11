using UnityEngine;
 
[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public Vector2 parallaxFactor;

    public void Move(Vector2 delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta.x * parallaxFactor.x;
        newPos.y -= delta.y * parallaxFactor.y;

        transform.localPosition = newPos;
    }
}
