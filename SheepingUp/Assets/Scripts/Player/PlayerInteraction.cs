using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    private Rigidbody rb;
    private bool playerDied;
    private CameraFollow cameraFollow;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    void Update() {
        if (!playerDied) {
            if (rb.velocity.sqrMagnitude > 60) {
                playerDied = true;
                cameraFollow.CanFollow = false;

                SoundManager.instance.GameEndSound();
                GameplayManager.instance.RestartGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Coin") {
            other.gameObject.SetActive(false);

            SoundManager.instance.PickedUpCoinSound();
            GameplayManager.instance.IncrementScore();
        }

        if (other.tag == "Spike") {
            gameObject.SetActive(false);
            cameraFollow.CanFollow = false;

            SoundManager.instance.GameEndSound();
            GameplayManager.instance.RestartGame();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "StartPlatform" || other.gameObject.tag == "EndPlatform") {
            SoundManager.instance.GameStartSound();
        }

        if (other.gameObject.tag == "EndPlatform") {
            GameplayManager.instance.RestartGame();
        }
    }
}
