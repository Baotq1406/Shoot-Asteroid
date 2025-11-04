using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : MonoBehaviour
{
    [SerializeField] private float _range;
    //[SerializeField] private Transform _target;
    [SerializeField] private float _fireRate;
    private float _nextFireTime;
    [SerializeField] private float _bulletForce;

    bool _deteched = false;

    Vector2 _direction;
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;


    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag(CONSTANT.TAG_PLAYER);
        if (player == null)
            return;

        float moveY = (GameManager.Instance.worldSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);

        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
        // Tim vi tri nguoi choi
        Vector2 targetPos = player.transform.position;

        // Tinh huong den nguoi choi
        _direction = targetPos - (Vector2)transform.position;

        // Kiem tra raycast den nguoi choi
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, _direction, _range);

        // Neu raycast cham vao nguoi choi
        if (rayInfo)
        {
            if (rayInfo.collider.CompareTag(CONSTANT.TAG_PLAYER))
            {
                // Kich hoat ban
                if (!_deteched)
                {
                    _deteched = true;
                    //Debug.Log("Player Detected");
                }
            }
            else
            {
                // Tat kich hoat ban
                if (_deteched)
                {
                    _deteched = false;
                    //Debug.Log("Player Lost");
                }
            }
        }

        if (_deteched)
        {
            // Quay dan ve huong nguoi choi
            _gun.transform.up = _direction;

            // Ban dan voi toc do ban
            if (Time.time > _nextFireTime)
            {
                // Cap nhat thoi gian ban tiep theo
                _nextFireTime = Time.time + 1f / _fireRate;
                Shoot();
            }
        }
    }

    // Ham ban dan
    void Shoot()
    {
        GameObject BulletIns = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(_direction.normalized * _bulletForce);

        //AudioManager.Instance.PlaySound(AudioManager.Instance.);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
