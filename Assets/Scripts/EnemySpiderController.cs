using UnityEngine;
using System.Collections;

public class EnemySpiderController : MonoBehaviour {

    public float speed = 10;
    public float jumpSpeed = 1;
    public float rotateSpeed = 100;
    public Transform player;
    public GameController gameController;

    private Animator anim;
    private int InRangeHash = Animator.StringToHash("InRange");
    
    private int collidersHit;
    public int collidersToHit;
    public float knockBack;
    public int startingHP;
    private int currentHP;
    private Vector3 knockBackTarget;
    private Quaternion knockBackRotation;
    private Rigidbody rb;
    private BoltLauncher boltLauncher;
    private PlayerHealth playerHealth;


    private EnemySpiderSoundController soundController;

    private EnemyState state;

    enum EnemyState { Moving, Attacking, StickedToCamera, Fallen};
    

    void Awake() {

        //Get Components
        boltLauncher = FindObjectOfType<BoltLauncher>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        soundController = GetComponent<EnemySpiderSoundController>();
        player = GameObject.FindGameObjectWithTag("Head").transform;
        gameController = FindObjectOfType<GameController>();
        playerHealth = FindObjectOfType<PlayerHealth>();


    }

	
    void OnEnable() {
        //Reset stats
        state = EnemyState.Moving;
        collidersHit = 0;
        knockBackTarget = Vector3.zero;
        soundController.PlayWalkingSound();
    }

    void OnDisable() {
        if(state == EnemyState.StickedToCamera) {
            ResetFlagsAfterFall();
        }

        soundController.StopOnFaceSound();
    }

	
	void Update () {
        switch (state) {
            case EnemyState.Moving:
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;

            case EnemyState.StickedToCamera:
                transform.position = Vector3.MoveTowards(transform.position, player.position, jumpSpeed * Time.deltaTime);
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(-90, 180, 0), rotateSpeed * Time.deltaTime);
                break;

            case EnemyState.Fallen:
                if (knockBackTarget == Vector3.zero) {
                    knockBackTarget = player.transform.forward * knockBack;
                    knockBackRotation = transform.rotation;
                    knockBackRotation = Quaternion.AngleAxis(180, Vector3.forward);
                }
                Vector3 fallTarget = new Vector3(knockBackTarget.x, 1, knockBackTarget.z);

                transform.position = Vector3.MoveTowards(transform.position, fallTarget, jumpSpeed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, knockBackRotation, rotateSpeed * Time.deltaTime);
                break;

        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player Circle Area") && state == EnemyState.Moving){
            soundController.StopWalkingSound(); 
                    
            if (!gameController.hasSomethingOnFace) {
                 JumpAttack();
            } else {
                 MeleeAttackStance();
            }
        }

        if(other.CompareTag("Drone Fall Collider") && state == EnemyState.StickedToCamera ) {
            collidersHit += 1;
            if(collidersHit >= collidersToHit) {
                FallOff();              
            }
        }
    }

    void MeleeAttackStance() {
        state = EnemyState.Attacking;
        anim.SetBool(InRangeHash, true);
    }

    void MeleeAttack() {
        soundController.PlayStabSound();
        if(state == EnemyState.StickedToCamera) {
            playerHealth.TakeDamage(10);
        } else {
            playerHealth.TakeDamage(5);
        }
        
    }


    void JumpAttack() {
        transform.SetParent(player);     
        state = EnemyState.StickedToCamera;

        InvokeRepeating("MeleeAttack", 0, 2.0f);

        gameController.hasSomethingOnFace = true;
        boltLauncher.DisableShooting();
        soundController.PlayOnFaceSound();
    }

    void FallOff() {
        transform.SetParent(null);      
        state = EnemyState.Fallen;
        ResetFlagsAfterFall();
        soundController.LowerFaceSoundVolume();
    }

    void EnableJumpingOnFace() {
        gameController.hasSomethingOnFace = false;
    }

    public int GetCurrnetHP() {
        return currentHP;
    }

    public void DealDamage(int dmg) {
        currentHP -= dmg;
    }

    void ResetFlagsAfterFall() {
        CancelInvoke();

        Invoke("EnableJumpingOnFace", 10.0f);
        boltLauncher.EnableShooting();
    }
}
