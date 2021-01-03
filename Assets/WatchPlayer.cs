using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchPlayer : MonoBehaviour
{
    Transform player;
    Vector3 playerVector;
    Vector3 cameraVector;

    private void Start()
    {
        player = GameObject.Find("HeroKnight").transform;
        cameraVector.x = 1;
        cameraVector.y = 1;
        cameraVector.z = 0;
    }

    private void FixedUpdate()
    {
        playerVector = player.position;
        playerVector.z = -10;
        transform.position = Vector3.Lerp(transform.position, playerVector + cameraVector, 3 * Time.deltaTime);
    }
}
