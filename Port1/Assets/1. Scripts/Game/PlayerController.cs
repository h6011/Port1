using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    

    

    [SerializeField] PlayerSettings playerSettings;


    Animator animator;
    CapsuleCollider2D capsuleCollider;
    SpriteRenderer spriteRenderer;

    GameManager gameManager;
    EffectManager effectManager;
    MainCanvasManager mainCanvasManager;
    RankingManager rankingManager;

    Vector2 moveDir;

    [Header("Prefab")]
    [SerializeField] GameObject playerBullet;

    [Header("Vars")]
    [SerializeField] Transform dynamic;
    [SerializeField] Transform shotPos;

    [Header("Stat")]
    [SerializeField] float speed = 1f;
    [SerializeField] float shotDelay = 0.1f;

    [SerializeField] int hp;
    [SerializeField] int maxHp = 3;

    /// <summary>
    /// �÷��̾��� ���� ü��
    /// </summary>
    public int Hp => hp;

    public Transform Dynamic => dynamic;

    private float shotTimer;

    [SerializeField] bool isDead = false;

    /// <summary>
    /// �÷��̾ �׾�����
    /// </summary>
    public bool IsDead => isDead;

    [Header("Invincibility")]
    [SerializeField] float InvincibilityTime = 2f;
    float InvincibilityTimer;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            //checkFps();
        }
    }

    private void Start()
    {
        LockInCamera.Instance.SetPlayer(capsuleCollider.size);

        //checkFps();
        fillHp();

        spriteRenderer = GetComponent<SpriteRenderer>();

        gameManager = GameManager.Instance;
        effectManager = EffectManager.Instance;
        mainCanvasManager = MainCanvasManager.Instance;
        rankingManager = RankingManager.Instance;
    }

    private void Update()
    {
        timerAction();

        moveAction();
        animationAction();
        shotAction();
        invincibilityAction();
    }

    private void invincibilityAction()
    {
        if (IsDead) return;
        if (isInvincibility())
        {
            Color _color = spriteRenderer.color;
            _color.a = 0.5f;
            spriteRenderer.color = _color;
        }
        else
        {
            Color _color = spriteRenderer.color;
            _color.a = 1f;
            spriteRenderer.color = _color;
        }
    }

    /// <summary>
    /// �÷��̾��� ���� ����
    /// </summary>
    /// <returns></returns>
    public bool isInvincibility()
    {
        return InvincibilityTimer > 0;
    }

    /// <summary>
    /// ü���� �ִ�ü������ ��ġ����
    /// </summary>
    private void fillHp()
    {
        hp = maxHp;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        mainCanvasManager.SetVisibleUIFromName("GameOver", true);
        mainCanvasManager.SetGameOverScreen();
    }

    


    IEnumerator whenDeadActionCor()
    {
        Debug.Log("whenDeadActionCor");
        float _Time = 2f;
        float _Timer = 0;

        Color _color = spriteRenderer.color;
        _color.a = 1f;
        spriteRenderer.color = _color;

        while (true)
        {
            float unScaledDeltaTime = Time.unscaledDeltaTime;

            _Timer += unScaledDeltaTime / _Time;

            float _rotateAmount = 100f * unScaledDeltaTime;

            transform.rotation *= Quaternion.Euler(0, 0, _rotateAmount);
            transform.localScale = new Vector3((1 - _Timer), (1 - _Timer));

            if (_Timer >= 1)
            {
                break;
            }

            yield return null;
        }

        GameOver();

        yield return null;
    }

    private void whenDeadAction()
    {
        gameManager.IsGameOver = true;
        StartCoroutine(whenDeadActionCor());
    }

    /// <summary>
    /// ������ �ִ� �õ��ϱ�
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns>���������� �������� �־�����</returns>
    public bool TryDamage(int _damage)
    {
        if (!isInvincibility())
        {
            GetDamage(_damage);
            return true;
        }
        return false;
    }

    /// <summary>
    /// �÷��̾ �������� �޾����� ȣ��
    /// </summary>
    /// <param name="_damage">������ ��</param>
    public void GetDamage(int _damage)
    {
        _damage = Mathf.Clamp(_damage, 0, 999);

        if (isDead) return;

        hp -= _damage;

        InvincibilityTimer = InvincibilityTime;

        if (hp <= 0)
        {
            isDead = true;
            whenDeadAction();

            float explosionSize = Mathf.Max(spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height);
            effectManager.CreateEffect(eEffectType.Explosion1, transform.position, explosionSize);



        }
    }

    private void timerAction()
    {
        if(isDead) return;
        shotTimer += Time.deltaTime;

        InvincibilityTimer -= Time.deltaTime;
        InvincibilityTimer = Mathf.Clamp(InvincibilityTimer, 0, InvincibilityTime);
    }

    /// <summary>
    /// �Ѿ� �߻�
    /// </summary>
    private void shot()
    {
        if (isDead) return;
        GameObject newBullet = Instantiate(playerBullet, shotPos.position, Quaternion.Euler(0, 0, 0), dynamic);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();
    }

    private void shotAction()
    {
        if (Input.GetKey(playerSettings.playerKeySettings.shotKey) && !gameManager.GamePaused && !isDead)
        {
            if (shotTimer >= shotDelay)
            {
                shotTimer = 0f;
                shot();

            }
        }
        
    }


    

    private void moveAction()
    {
        if (isDead) return;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);

        moveDir.x = x;
        moveDir.y = y;

        LockInCamera.Instance.CheckPosition(transform);
    }

    private void animationAction()
    {
        if (!gameManager.GamePaused && !isDead)
        {
            animator.SetInteger("Horizontal", (int)moveDir.x);
        }
    }

    /// <summary>
    /// �÷��̾��� Ű ���� ��������
    /// </summary>
    /// <returns>�÷��̾��� Ű ����</returns>
    public PlayerSettings.PlayerKeySettings GetKeySettings()
    {
        return playerSettings.playerKeySettings;
    }



}
