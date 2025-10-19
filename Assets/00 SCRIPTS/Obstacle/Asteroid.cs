using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite[] _asteroidSprites;

    Rigidbody2D _rigi;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigi = GetComponent<Rigidbody2D>();
        _spriteRenderer.sprite = _asteroidSprites[Random.Range(0, _asteroidSprites.Length)];

        // random push direction
        float pushX = Random.Range(-1f, 1);
        float pushY = Random.Range(-1f, 0);

        _rigi.velocity = new Vector2(pushX, pushY);
    }

    // Update is called once per frame
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
