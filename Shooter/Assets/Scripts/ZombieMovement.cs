using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Splines;

public class ZombieMovement : MonoBehaviour
{   
    public NavMeshAgent agent ;
    public Transform curwalkpoint;
    
    public LayerMask zombie ;
    public WalkPointsData walkPath ;
    public Animator animator ;
    
    public Transform player;
    
    public float attackRange ;
    public bool pointReached;

    public float radius;
    float angle = 192;
    public float outOfFOVResetTime = 3f;
    float outOfFOVTimer = 0f;
    FieldOfView fov;

    ZombieHealth health;
    bool canSeePlayer = false;
    bool startAttacking = false;
    bool inRunRange = false;
    bool attacking = false;
    bool lostPlayer = false;

    public float attackspeed;
    public float attackDamage;
    float attackTimer;

    public Vector3 target;
    
    public ZombieEar zombieEar ;

    public LayerMask playerLayer = 8;
    public PlayerHealth playerHealth;

    float audioCalledInterval = 3f;
    float audioTimer  = 0f;
    void Start()
    {
        walkPath = new WalkPointsData();
        fov = new FieldOfView();
        zombieEar = new ZombieEar();//gameObject.GetComponent<ZombieHear>();
        zombie.value = 256;
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        health = gameObject.GetComponent<ZombieHealth>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {

        
        NewBehavier();
        FollowToPosition(target);
        UpdateAnimatorParameters();
        
    }

    //Idlewalk all great
    void idleWalk(){
        pointReached = walkPath.checkIfReached(curwalkpoint.position,zombie);
        if (pointReached || lostPlayer){
            curwalkpoint = walkPath.SelectNextPoint(curwalkpoint);
            //idle walk animation
            animator.SetBool("lostPlayer", lostPlayer);
            
        }
        target = curwalkpoint.position;
    }

    void NewBehavier()
    {
        float zombiePlayerDistance = Vector3.Distance(transform.position, player.position);
        if (audioTimer < Time.time)
        {
            audioTimer = Time.time + audioCalledInterval;
            FindAnyObjectByType<Audio>().play("zombieAudio", 1 / zombiePlayerDistance);
        }
        
        canSeePlayer = fov.InFieldOfView(transform, canSeePlayer, radius, angle);
        zombieEar.CanHearShoot(transform , zombieEar.hearshoot);
        if(!health.getAttacked || zombiePlayerDistance <= radius )
            if (zombiePlayerDistance <= attackRange)
            {
                if (!fov.ObstacleBetweenTarget(transform, (player.position - transform.position).normalized, zombiePlayerDistance))
                {
                    startAttacking = true;
                    Attack();
                
                }
                NotChasing();
            }
            else if (zombieEar.hearshoot && !canSeePlayer)
            {
                if (!(Vector3.Distance(ZombieEar.Target , transform.position) < 5f))
                {
                    Chase(ZombieEar.Target);
                    animator.SetTrigger("hearShot");
                    OutOfAttackRange();
                }
                else
                {
                    lostPlayer = true;
                    idleWalk();
                    OutOfAttackRange();
                }
                    
            }
            else if (zombiePlayerDistance <= radius)
            {
                if (canSeePlayer)
                {
                    inRunRange = true;
                    Chase(player.position);
                    lostPlayer = false;
                    outOfFOVTimer = Time.time + outOfFOVResetTime;
                    zombieEar.hearshoot = false;
                }
                else if (!canSeePlayer && !lostPlayer)
                {
                    if (outOfFOVTimer < Time.time || walkPath.checkIfReached(target , zombie))
                    {
                        lostPlayer = true;
                        idleWalk();
                        outOfFOVTimer = 0f;
                        NotChasing() ;
                    }
                }

                OutOfAttackRange();

            }

            else         
            {
                idleWalk();
                NotChasing();
                OutOfAttackRange();
            }

        else 
        {
            if (!attacking)
            {
                    animator.SetTrigger("gotAttacked");
                    target = player.position;
            }
        }

    }
    void Chase(Vector3 chasetarget) 
    {
        target = chasetarget;
        animator.SetBool("inRunRange", inRunRange);
        animator.SetTrigger("gotAttacked");
        
    }
    void OutOfAttackRange() 
    {
        attacking = false;
        startAttacking = false;
    }
    void NotChasing() 
    {
        inRunRange = false; 
    }
    void FollowToPosition(Vector3 positon )
    {
        agent.SetDestination(positon);
    }

    //attack working great
    void Attack()
    {
        target = player.position;
        if (startAttacking && !attacking)
        {
            //Debug.Log("isAttacking");
            animator.SetTrigger("startAttack");
            attacking = true;
            startAttacking = false;
        }

        if (canSeePlayer)
        {
            //Debug.Log("0001");
            if (!fov.ObstacleBetweenTarget(transform, (player.position - transform.position).normalized,Vector3.Distance(player.position , transform.position)))
            {
                //Debug.Log("GotTarget");
                if (Time.time > attackTimer)
                {
                    playerHealth.GotHit(attackDamage);
                    attackTimer = Time.time + 1 / attackspeed;
                }
            }
        }
        
    }
    void UpdateAnimatorParameters()
    {
        animator.SetBool("canSeePlayer", canSeePlayer);
        animator.SetBool("inRunRange", inRunRange);
        animator.SetBool("inAttackRange", attacking);
        animator.SetBool("lostPlayer", lostPlayer);
    }












}
