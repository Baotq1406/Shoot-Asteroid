using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState = PlayerController.PlayerState;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;

    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    private Material _defaultMaterial;
    [SerializeField] private Material _flashMaterial;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _anim = this.GetComponent<Animator>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _defaultMaterial =  _spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Update animation based on player state
    public void UpdateAnimation(PlayerState playerState)
    {
        for (int i = 0; i <= (int)PlayerState.Moving; i++)
        {
            string stateName = ((PlayerState)i).ToString(); 
            if (playerState == (PlayerState)i)
            {
                _anim.SetBool(stateName, true);
            }
            else
            {
                _anim.SetBool(stateName, false);
            }
        }
    }

    public void FlashDamage()
    {
        _spriteRenderer.material = _flashMaterial;
        StartCoroutine(ResetMaterial());
    }

    private IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.material = _defaultMaterial;
    }
}
