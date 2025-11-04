using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            return;
        }

        if (_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] List<GameObject> _listBullets = new List<GameObject>();

    // Ham tra ve vien dan khong hoat dong trong pool
    public GameObject GetBullet()
    {
        // Tim vien dan khong hoat dong de su dung
        foreach (GameObject b in _listBullets)
        {
            if (b.activeSelf)
                continue;

            return b;

        }
        // Neu khong co vien dan nao khong hoat dong thi tao moi
        GameObject bullet = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity);
        _listBullets.Add(bullet);
        bullet.SetActive(false);
        return bullet;
    }
}
