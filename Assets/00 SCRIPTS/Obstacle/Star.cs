using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.score++;
            AudioManager.Instance.PlaySound(AudioManager.Instance.score);
            if (PlayerController.Instance.score > 999)
            {
                PlayerController.Instance.score = 999;
            }
            UIController.Instance.UpdateScoreText(PlayerController.Instance.score);
            Destroy(gameObject);
        }
    }
}

