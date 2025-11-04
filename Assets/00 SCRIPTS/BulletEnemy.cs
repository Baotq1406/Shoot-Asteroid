using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletEnemy : MonoBehaviour
{
    void Update()
    {
        if (IsOutOfViewport())
        {
            Destroy(gameObject);
        }
    }

    // Kiem tra neu vien dan di ra ngoai
    private bool IsOutOfViewport()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));

        Vector3 pos = transform.position;
        return pos.x < min.x || pos.x > max.x || pos.y < min.y || pos.y > max.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.TAG_OBSTACLE))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag(CONSTANT.TAG_PLAYER))
        {
            PlayerController.Instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}