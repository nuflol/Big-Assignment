using System.Collections;
using UnityEngine;

public enum EnemyState {
    Wander,
    Follow,
    Die,
    Attack
}

public class EnemyController : MonoBehaviour {
    private GameObject player;

    public EnemyState currState = EnemyState.Wander;

    public float range;
    public float speed;
    public float _coolDown;
    public float attackRange;
    private bool _chooseDir = false;
    // TODO respawn enemies
    //private bool _dead = false;
    private bool _attackCooldown = false;
    private Vector3 _randomDir;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update() {
        switch (currState) {
            case(EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

        if (_isPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Follow;
        }
        else if(!_isPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Wander;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange) {
            currState = EnemyState.Attack;
        }
    }

    private bool _isPlayerInRange(float range) {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection() {
        _chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 9f));
        _randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(_randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0f, 0f));
        _chooseDir = false;
    }

    void Wander() {
        if (!_chooseDir) {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if (_isPlayerInRange(range)) {
            currState = EnemyState.Follow;
        }
    } 

    void Follow() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack() {
        if (!_attackCooldown) {
            GameController.DamagePlayer(1);
            StartCoroutine(routine: CoolDown());
        }
        
    }

    private IEnumerator CoolDown() {
        _attackCooldown = true;
        yield return new WaitForSeconds(_coolDown);
        _attackCooldown = false;
    }

    public void Death() {
        Destroy(gameObject);
    }
}
