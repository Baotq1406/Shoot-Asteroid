using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float speedBullet = 8f;

    // Cap nhat duoc goi moi khung hinh
    void Update()
    {
        // Lay vi tri hien tai cua vien dan
        Vector2 position = transform.position;

        // Tinh toan vi tri moi cua vien dan
        position = new Vector2(position.x, position.y + speedBullet * Time.deltaTime);

        // Cap nhat vi tri cua vien dan
        transform.position = position;

        // Diem tren cung ben phai cua man hinh
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Neu vien dan di ra ngoai man hinh o tren thi huy no
        if (transform.position.y > max.y)
        {
            //Destroy(gameObject);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Neu vien dan va cham voi doi tuong co tag la "Obstacle" thi huy no
        if (collision.gameObject.CompareTag(CONSTANT.TAG_OBSTACLE))
        {
            //Destroy(gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
