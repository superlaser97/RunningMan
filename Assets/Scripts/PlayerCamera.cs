using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    [SerializeField]
    private float CameraLerpSpeed = 1;

    [SerializeField]
    private Transform PlayerTransform;
    
    //Camera Offset from player (Original)
    private Vector2 CameraOffSet;

    [Header("Debug")]
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = Camera.main;
        PlayerTransform = PlayerController.instance.transform;

        CameraOffSet = new Vector2(m_Camera.transform.position.x, m_Camera.transform.position.y) - new Vector2(PlayerTransform.position.x, PlayerTransform.position.y);

        //500 is the perfect number for the speed we have. max speed / 500 will give us the enough speed for camera to be on middle when at max speed.
        //Faster Max speed for player = more speed for camera but the ratio for center is 500
        CameraLerpSpeed = PlayerController.instance.GetMaxSpeed() / 500;
    }
    
	// Update is called once per frame
	void Update () {
        LerpCamera();
	}

    //Lerp camera to the offset position
    public void LerpCamera()
    {
        //If the player move faster than lerp, it'll look like it's at the center when it's just right.
        Vector3 newOffsetPos = new Vector3(CameraOffSet.x,
            0,
            m_Camera.transform.position.z);

        m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, PlayerTransform.position + newOffsetPos, Time.deltaTime * CameraLerpSpeed);
    }
}
