using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float backgroundImageHeight;

    // Start is called before the first frame update
    void Start()
    {
        Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
        // tinh chieu cao cua nen
        backgroundImageHeight = sprite.texture.height / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    // Di chuyen nen theo huong y
    void Update()
    {
        float moveY = (moveSpeed * PlayerController.Instance.boost) 
            * Time.deltaTime;
        transform.position += new Vector3(0, moveY);

        // neu nen di chuyen vuot qua chieu cao cua no thi dat lai ve vi tri ban dau
        if (Mathf.Abs(transform.position.y) - backgroundImageHeight > 0)
        {
            transform.position = new Vector3(transform.position.x, 0f);
        }
    }
}
