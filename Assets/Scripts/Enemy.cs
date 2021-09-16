using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public AssetBundleManager assetManager;

    //Target
    protected Transform player;
    public GameObject HP;
    Vector3 localScale;

    //Enemy Stats
    [SerializeField] private float speed = 0.05f;
    public float health = 50.0f;
    public float damage = 20.0f;
    Animator animator;
    public string type;
    float maxHP;

    //Movement
    public bool move = false;

    //Attacking
    private bool reachPlayer = false;
    private float ticks = 0.0f;
    private const float INTERVAL = 5.0f;
    bool damage_received = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = health;
        localScale = HP.transform.localScale;
        player = GameObject.FindGameObjectWithTag("PlayerBody").transform.GetChild(1).transform;

        if(this.type == "METAL ARM")
        {
            RuntimeAnimatorController anim = AssetBundleManager.Instance.GetAsset<RuntimeAnimatorController>("metalarm controller", "MetalArm Controller");
            this.GetComponent<Animator>().runtimeAnimatorController = anim;
        }
        else if (this.type == "INSECT")
        {
            RuntimeAnimatorController anim = AssetBundleManager.Instance.GetAsset<RuntimeAnimatorController>("insect controller", "Insect Controller");
            this.GetComponent<Animator>().runtimeAnimatorController = anim;
        }
        else if (this.type == "WASP")
        {
            RuntimeAnimatorController anim = AssetBundleManager.Instance.GetAsset<RuntimeAnimatorController>("wasp controller", "Wasp Controller");
            this.GetComponent<Animator>().runtimeAnimatorController = anim;
        }
        //Boss
        else if (this.type == "MUTANT")
        {
            RuntimeAnimatorController anim = AssetBundleManager.Instance.GetAsset<RuntimeAnimatorController>("mutant controller", "Mutant Controller");
            this.GetComponent<Animator>().runtimeAnimatorController = anim;
        }
        else if (this.type == "FAT")
        {
            RuntimeAnimatorController anim = AssetBundleManager.Instance.GetAsset<RuntimeAnimatorController>("fat controller", "Fat Controller");
            this.GetComponent<Animator>().runtimeAnimatorController = anim;
        }
        else if (this.type == "COBRA")
        {
            RuntimeAnimatorController anim = AssetBundleManager.Instance.GetAsset<RuntimeAnimatorController>("cobra controller", "Cobra Controller");
            this.GetComponent<Animator>().runtimeAnimatorController = anim;
        }

        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Walk towards player if not near player
        if (transform.position.z > player.transform.position.z + 1.5)
        {
            if (move)
            {
                transform.LookAt(player.transform);
                transform.position += transform.forward * speed * Time.deltaTime;
            }           
        }

        else
        {
            reachPlayer = true;
        }

        if (move)
        {
            Attack();
        }       
    }

    private void Attack()
    {
        if (reachPlayer)
        {
            if(this.type != "WASP")
            {
                animator.SetBool("Reached", true);
            }
            
            ticks += Time.deltaTime;
            if(ticks >= INTERVAL)
            {
                SoundManagerScript.PlaySound("Monster");
                animator.SetBool("Attack", true);
                UICallbackScript.damaged = true;
                StartCoroutine("DamagePlayer");
                ticks = 0.0f;
            }

            else
            {
                animator.SetBool("Attack", false); 
            }
        }
    }

    public IEnumerator DamagePlayer()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerSystem.health -= damage;
        UICallbackScript.damaged = false;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        animator.SetFloat("Health", health);

        localScale.x = health / maxHP;
        HP.transform.localScale = localScale;

        if (health <= 0f)
        {
            localScale.x = 0;
            HP.transform.localScale = localScale;
            Die();
        }

    }

    void Die()
    {
        Destroy(this.gameObject, 1);
        if (!damage_received)
        {
            PlayerSystem.score += 50;
            PlayerSystem.credits += 50;
            damage_received = true;
        }      
    }
}
