using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ZoomIn : MonoBehaviour
{
    public bool isDashing = false;
    private PixelPerfectCamera camera;
    private Dash playerDash;
    public float normalZoom = 16f;
    public float dashZoom = 13f;
    public float zoomSpeed = 1f;
    private float currentZoom;

    void Start()
    {
        camera = GetComponent<PixelPerfectCamera>();
        currentZoom = normalZoom; 
    }

    void Update()
    {
        if (playerDash == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerDash = player.GetComponent<Dash>();
            }
        }

        if (playerDash != null)
        {
            isDashing = playerDash.isDashing;

            float targetZoom = isDashing ? dashZoom : normalZoom;

            currentZoom = Mathf.MoveTowards(currentZoom, targetZoom, zoomSpeed);
            Debug.Log(currentZoom);
            camera.assetsPPU = Mathf.RoundToInt(currentZoom);
        }
    }
}
