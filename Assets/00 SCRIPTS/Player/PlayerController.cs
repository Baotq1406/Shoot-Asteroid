using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private float _moveSpeed = 0.5f;

    [SerializeField] GameObject _playerBulletPrefab;
    [SerializeField] GameObject _bulletPosition;
    [SerializeField] private float _fireCooldown = 0.25f; // co the chinh trong Inspector
    private float _nextShootTime = 0f; // thoi diem co the ban tiep theo

    [SerializeField] PlayerState _playerState = PlayerState.Idle;
    [SerializeField] AnimationController _anim;

    [SerializeField] private float _boost = 1f;
    public float boost { get { return _boost; } set { _boost = value; } }
    private float _boostPower = 3f;
    private bool _isBoosting = false;

    [SerializeField] private float _energy;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _energyRegen;

    Rigidbody2D _rigi;
    [SerializeField] private GameObject _frameBoost;
    Vector2 _playerDirection;

    // Ham Awake duoc goi khi doi tuong duoc khoi tao
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

        if (_frameBoost != null)
            _frameBoost.SetActive(false);
    }

    // Ham Start duoc goi khi bat dau
    void Start()
    {
        _rigi = GetComponent<Rigidbody2D>();
        _energy = _maxEnergy;
        UIController.Instance.UpdateEnergySlider(_energy, _maxEnergy);
    }

    // Update: chi doc input, xu ly ban, boost, nang luong, animation
    void Update()
    {
        // Doc input huong di chuyen
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _playerDirection = new Vector2(x, y).normalized;

        // Xu ly boost bang phim Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            EnterBoost(_frameBoost);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitBoost(_frameBoost);
        }

        // Ban dan voi cooldown
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextShootTime)
        {
            Shoot();
            _nextShootTime = Time.time + _fireCooldown;
        }

        // Cap nhat trang thai/animation (dua tren van toc frame truoc)
        UpdateState();
        _anim.UpdateAnimation(_playerState);
    }

    // FixedUpdate: thuc hien di chuyen va gioi han vi tri
    void FixedUpdate()
    {
        Moving();
    }

    // Ham Moving: cap nhat van toc, lat mat va gioi han vi tri (Physics)
    void Moving()
    {
        // Di chuyen bang Rigidbody2D.velocity (co he so boost)
        _rigi.velocity = _playerDirection * _moveSpeed;

        // Lat nguoi choi theo huong di chuyen (dua tren van toc ngang)
        if (_rigi.velocity.x > 0)
            this.transform.localScale = new Vector3(-1, 1, 1);
        else
            this.transform.localScale = new Vector3(1, 1, 1);

        // Cap nhat nang luong
        if (_isBoosting)
        {
            if (_energy >= 0.2f)
                _energy -= 0.2f;
            else
                ExitBoost(_frameBoost);
        }
        else
        {
            if (_energy < _maxEnergy)
                _energy += _energyRegen;
        }
        UIController.Instance.UpdateEnergySlider(_energy, _maxEnergy);

        // Gioi han vi tri trong man hinh
        ClampToViewport(0.4f, 0.4f);
    }

    // Ham cap nhat trang thai nguoi choi
    void UpdateState()
    {
        if (_rigi.velocity.x != 0)
            _playerState = PlayerState.Moving;
        else
            _playerState = PlayerState.Idle;
    }

    // Gioi han vi tri theo viewport voi le x/y
    void ClampToViewport(float borderX, float borderY)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));

        max.x -= borderX;
        min.x += borderX;
        max.y -= borderY;
        min.y += borderY;

        // Cap nhat vi tri cua nguoi choi
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        transform.position = pos;
    }

    // Ham ban dan
    void Shoot()
    {
        if (_playerBulletPrefab == null || _bulletPosition == null)
            return;

        GameObject bullet01 = Instantiate(_playerBulletPrefab);
        bullet01.transform.position = _bulletPosition.transform.position;
    }

    // Ham tang toc
    void EnterBoost(GameObject gameObject)
    {
        if (gameObject != null && _energy > 10)
        {
            gameObject.SetActive(true);
            _boost = _boostPower;
            _isBoosting = true;
        }
    }

    // Ham thoat tang toc
    void ExitBoost(GameObject gameObject)
    {
        if (gameObject != null)
            gameObject.SetActive(false);

        _boost = 1f;
        _isBoosting = false;
    }

    // Trang thai nguoi choi
    public enum PlayerState
    {
        Idle,
        Moving,
    }
}
