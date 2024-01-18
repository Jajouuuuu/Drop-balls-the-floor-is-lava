using UnityEngine;

public class BouncyObject : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("PlayerBall")) {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null) {
                Vector3 reflectDir = Vector3.Reflect(ballRigidbody.velocity, collision.contacts[0].normal);
                ballRigidbody.velocity = reflectDir;
            }
        }
    }
}
