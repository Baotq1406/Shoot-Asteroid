using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite[] _asteroidSprites;

    private Material _defaultMaterial;
    [SerializeField] private Material _flashMaterial;

    [SerializeField] private GameObject _destroyEffect;
    [SerializeField] private int _lives;

    Rigidbody2D _rigi;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigi = GetComponent<Rigidbody2D>();
        _spriteRenderer.sprite = _asteroidSprites[Random.Range(0, _asteroidSprites.Length)];
        _defaultMaterial = _spriteRenderer.material;

        // random push direction
        float pushX = Random.Range(-1f, 1);
        float pushY = Random.Range(-1f, 0);

        _rigi.velocity = new Vector2(pushX, pushY);

        // random scale
        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale = new Vector3(randomScale, randomScale);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            _spriteRenderer.material = _flashMaterial;
            StartCoroutine(ResetMaterial());
            AudioManager.Instance.PlaySound(AudioManager.Instance.hitRock);
            _lives--;
            if (_lives <= 0)
            {
                Instantiate(_destroyEffect, transform.position, transform.rotation);
                AudioManager.Instance.PlaySound(AudioManager.Instance.destroyAsteroid);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.material = _defaultMaterial;
    }
}
