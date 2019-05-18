using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour {

    [SerializeField]
    private Transform[] spikes;

    [SerializeField]
    private GameObject coin;

    private bool fallDown;

    void Start() {
        ActivatePlatform();
    }

    void ActivateSpike() {
        int index = Random.Range(0, spikes.Length);
        spikes[index].gameObject.SetActive(true);
        spikes[index].DOLocalMoveY(0.7f, 1.3f).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(3f, 5f));
    }

    void AddCoin() {
        GameObject go = Instantiate(coin);
        go.transform.position = transform.position;
        go.transform.SetParent(transform);
        go.transform.DOLocalMoveY(1f, 0f);
    }

    void ActivatePlatform() {
        int chance = Random.Range(0, 100);

        if (chance > 70) {
            int type = Random.Range(0, 3);

            switch (type) {
                case 0:
                    ActivateSpike();
                    break;
                case 1:
                    AddCoin();
                    break;
                case 2:
                    fallDown = true;
                    break;
            }
        }
    }

    void InvokeFalling() {
        gameObject.AddComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            //Invoke("InvokeFalling", 2f);

            if (fallDown) {
                fallDown = false;
                Invoke("InvokeFalling", 2f);
            }
        }
    }
}
