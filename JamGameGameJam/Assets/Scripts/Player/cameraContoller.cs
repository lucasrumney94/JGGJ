using UnityEngine;
using System.Collections;

public class cameraContoller : MonoBehaviour {

    public GameObject player;
    public GameObject anchor;
    public Camera mainCamera;

    public float followSmoothTime = 0.5f;
    public float turnSpeed = 4.0f;
    public float followDistanceScale = 1.0f;

    private playerStats pStats;
    private UnityStandardAssets.Utility.SmoothFollow sFollow;
    private float initialDistance;
    private float followVelocity;

    private float initialHeight;
    private float followScaleSnaked = 1.0f;
    private float originalFollowScale = 1.0f;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pStats = player.GetComponent<playerStats>();
        mainCamera = Camera.main;
        sFollow = mainCamera.GetComponent<UnityStandardAssets.Utility.SmoothFollow>();
        initialDistance = sFollow.distance;
        anchor = GameObject.FindGameObjectWithTag("Anchor");
        initialHeight = sFollow.height;
        followScaleSnaked = 1.0f;
        originalFollowScale = followDistanceScale;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float xAxis = Input.GetAxis("rotateCamera");
        anchor.transform.Rotate(new Vector3(0.0f, xAxis * turnSpeed, 0.0f));
        if (GameObject.FindGameObjectWithTag("Anchor").GetComponent<globController>().snaked)
            followDistanceScale = followScaleSnaked;
        else
            followDistanceScale = originalFollowScale;

        sFollow.distance = Mathf.SmoothDamp(sFollow.distance, initialDistance + pStats.playerRadius*followDistanceScale, ref followVelocity, followSmoothTime);


        float yAxis = Input.GetAxis("rotateCameraVertical");
        sFollow.height += yAxis;
        sFollow.height = Mathf.Clamp(sFollow.height, -3*pStats.playerRadius, +3*pStats.playerRadius);
    }
}
