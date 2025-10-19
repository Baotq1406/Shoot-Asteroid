using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Update()
    {
        float moveY = (GameManager.Instance.worldSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);

        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
}
