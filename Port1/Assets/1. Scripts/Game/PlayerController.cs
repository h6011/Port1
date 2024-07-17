using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [System.Serializable]
    public class PlayerSettings
    {
        [System.Serializable]
        public class PlayerKeySettings
        {
            public KeyCode pauseKey = KeyCode.Escape;
            public KeyCode shotKey = KeyCode.Space;
        }

        public enum ePlayerSettingsFpsType
        {
            NoLimit,
            Fps30,
            Fps60,
            Fps120,
            Fps240,
            Fps300,
            Fps500,
        }

        public PlayerKeySettings playerKeySettings;
        public ePlayerSettingsFpsType fpsType;
    }

    

    [SerializeField] PlayerSettings playerSettings;


    Animator animator;
    CapsuleCollider2D capsuleCollider;
    GameManager gameManager;

    Vector2 moveDir;

    [Header("Prefab")]
    [SerializeField] GameObject playerBullet;

    [Header("Vars")]
    [SerializeField] Transform dynamic;
    [SerializeField] Transform shotPos;

    [Header("Stat")]
    [SerializeField] float speed = 1f;
    [SerializeField] float shotDelay = 0.1f;

    private float shotTimer;


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
            checkFps();
        }
    }

    private void Start()
    {
        LockInCamera.Instance.SetPlayer(capsuleCollider.size);

        checkFps();

        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        timerAction();

        moveAction();
        animationAction();
        shotAction();
        
    }

    private void timerAction()
    {
        shotTimer += Time.deltaTime;
    }

    private void shot()
    {
        //Debug.Log("Shot");

        GameObject newBullet = Instantiate(playerBullet, shotPos.position, Quaternion.Euler(0, 0, 0), dynamic);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

    }

    private void shotAction()
    {
        if (Input.GetKey(playerSettings.playerKeySettings.shotKey) && !gameManager.GamePaused)
        {
            if (shotTimer >= shotDelay)
            {
                shotTimer = 0f;
                shot();

            }
        }
        
    }


    private void checkFps()
    {

        if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.NoLimit) Application.targetFrameRate = -1;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps30) Application.targetFrameRate = 30;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps60) Application.targetFrameRate = 60;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps120) Application.targetFrameRate = 120;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps240) Application.targetFrameRate = 240;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps300) Application.targetFrameRate = 300;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps500) Application.targetFrameRate = 500;
    }

    private void moveAction()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);

        moveDir.x = x;
        moveDir.y = y;

        LockInCamera.Instance.CheckPosition(transform);
    }

    private void animationAction()
    {
        if (!gameManager.GamePaused)
        {
            animator.SetInteger("Horizontal", (int)moveDir.x);
        }
    }

    public PlayerSettings.PlayerKeySettings GetKeySettings()
    {
        return playerSettings.playerKeySettings;
    }



}
