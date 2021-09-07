using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float lifeTime;
    
    void Start() {
        StartCoroutine(DestroyDelay());
    }
    IEnumerator DestroyDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Enemy") {
            GameController.Score++;
            collider.gameObject.GetComponent <EnemyController>().Death();
            Destroy(gameObject);
        }
    }
}
