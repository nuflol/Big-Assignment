using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private float _moveSpeed = 5f;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    
    [SerializeField] private float _bulletSpeed;
    private float _lastFire;
    public float fireDelay;
    public GameObject bulletPrefab;
    
    Vector2 movement;
    
    
    
    // Update is called once per frame
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");
        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > _lastFire + fireDelay) {
            Shoot(shootHorizontal, shootVertical);
            _lastFire = Time.time;
        }
    }

    void Shoot(float x, float y) {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent <Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent <Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * _bulletSpeed : Mathf.Ceil(x) * _bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * _bulletSpeed : Mathf.Ceil(y) * _bulletSpeed,
            0
        );
    }

    // More reliable at handling physics
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * _moveSpeed * Time.fixedDeltaTime);
        
    }
}
