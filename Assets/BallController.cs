using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    public GameObject ballPrefab;
    private int ballsToPlace = 3;
    private float placementTime = 15f;
    private bool placingBalls = true;

    private void Update() {
        if (placingBalls) {
            placementTime -= Time.deltaTime;
            if (placementTime <= 0f) {
                placingBalls = false;
                DropBalls();
            } else {
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceBall();
                }
            }
        }
    }

    private void PlaceBall() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Vector3 newBallPosition = new Vector3(mousePosition.x+25, mousePosition.y+8, mousePosition.z-2.5f);
        GameObject newBall = Instantiate(ballPrefab, newBallPosition, Quaternion.identity);
        SphereCollider collider = newBall.GetComponent<SphereCollider>();
        if (collider != null) {
            collider.isTrigger = false; 
            PhysicMaterial ballMaterial = new PhysicMaterial();
            ballMaterial.bounciness = 0.8f;
            collider.material = ballMaterial;
        }

        Rigidbody rigidbody = newBall.GetComponent<Rigidbody>();
        if (rigidbody == null) {
            rigidbody = newBall.AddComponent<Rigidbody>();
            rigidbody.mass = 1f;
        }

        ballsToPlace--;
        if (ballsToPlace <= 0) {
            placingBalls = false;
            DropBalls();
        }
    }




    private void DropBalls() {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PlayerBall")) {
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody == null) {
                ball.AddComponent<Rigidbody>();
                ballRigidbody = ball.GetComponent<Rigidbody>();
                ballRigidbody.mass = 1f;
                ballRigidbody.drag = 0.5f;
            }

            ballRigidbody.useGravity = true;
            Collider ballCollider = ball.GetComponent<Collider>();
            if (ballCollider != null) {
                if (ballCollider.material == null) {
                  
                    PhysicMaterial ballMaterial = new PhysicMaterial();
                    ballMaterial.bounciness = 0.8f;
                    ballCollider.material = ballMaterial;
                }
            }
        }
    }
}
