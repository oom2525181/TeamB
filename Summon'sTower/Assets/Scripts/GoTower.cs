using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoTower : MonoBehaviour
{
    //private float speed = 5.0f;
    [SerializeField] Transform target;  //�ړ�����ꏊ�A�ړI�n
    [SerializeField] float speed;       //�ړ����x
    [SerializeField] float maxhp;          //�ő�̗�
   /* [HideInInspector]*/ public float hp;
    [SerializeField] float damage;      //�^����_���[�W
    //[SerializeField] private float detectDistance = 5f; //���m���鋗��
    [SerializeField] private LayerMask enemyLayer;      //���m����G���e�B�e�B�̎�ނ̐ݒ�p
    //[SerializeField] private int direction = 1;         //�G�����m��������p
    public float damageInterval = 1f;  //�_���[�W��^����Ԋu
    public int poison = 0;             //�ł̃_���[�W���󂯂��
    private float poisonTimer = 0f;    //�ł̎��Ԍo�ߗp
    private float lastDamageTime = 0f; //�Ō�Ƀ_���[�W��^��������
    public bool Type_Metal = false;    //��_���[�W1�_���[�W�Œ�
    public bool OneAttack = false;     //1��U�������������
    //private bool isHit = false;        //�Ԃ����Ă��邩�ǂ���
    public bool noReverse = false;     //���]�������邩�ǂ���

    public EnemySpawner spawner;
    GameObject director;
    public GameDirector gameDirector;
    public AudioClip sound1;
    AudioSource audioSource;
    private AttackRange attackRange;
    //GameObject closestEnemy = null;
    //float closestDist = float.MaxValue;

    private SpriteRenderer sr;
    private Color originalColor;

    private ParticleManager particleManager; //�p�[�e�B�N���p

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color; // ���̐F��ۑ�
    }

    private void Start()
    {
        if (speed <= 0) //�f�t�H���g�̃X�s�[�h
            speed = 1.5f;
        if (maxhp <= 0) //�f�t�H���g�̗̑�
            maxhp = 40;
       
        hp = maxhp;
        //target�̐ݒ�
        if (CompareTag("Ally")) 
        target = GameObject.Find("Enemy'sTower").transform;
        else if (CompareTag("Enemy"))
            target = GameObject.Find("Ally'sTower").transform;
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        this.director = GameObject.Find("GameDirector");
        gameDirector = director.GetComponent<GameDirector>();
        attackRange = GetComponentInChildren<AttackRange>();  //�q�I�u�W�F�N�g�̎Q��

        particleManager = FindFirstObjectByType<ParticleManager>();
        audioSource = GetComponent<AudioSource>();
    }
   /* public void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("�Ԃ�����");
            if (other.gameObject.CompareTag("Enemy") && CompareTag("Ally"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Ally") && CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            
            if (other.gameObject.CompareTag("Ally'sTower") && CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                Debug.Log("�s�k");
                spawner.isEND = true;
            }
            else  if (other.gameObject.CompareTag("Enemy'sTower") && CompareTag("Ally"))
            {
               Destroy(other.gameObject);
               Destroy(gameObject);
               Debug.Log("����");
               spawner.isEND = true;
            }
    }*/

    //public void OnCollisionStay2D(Collision2D other)
    //{
       

    //    GoTower gotower = other.gameObject.GetComponent<GoTower>();

    //    //if(gotower == null )
    //    //{
    //    //   Debug.Log("Gotower��null");
    //    //}

    //    //�Փ˂�����U��
    //    if (gotower != null && (spawner.isSTOPED == false && spawner.isEND == false))
    //    {
    //        if (other.gameObject.CompareTag("Enemy") && CompareTag("Ally"))
    //        {
    //            isHit = true;
    //            //Debug.Log("�Ԃ����Ă�");
    //            if (Time.time - lastDamageTime >= damageInterval)
    //            {
    //                Debug.Log("�Ȃ�����");
    //                gotower.TakeDamage(damage);
    //                lastDamageTime = Time.time;
    //            }
    //        }
    //        else if (other.gameObject.CompareTag("Ally") && CompareTag("Enemy"))
    //        {
    //            isHit = true;
    //            //Debug.Log("�Ԃ����Ă�");
    //            if (Time.time - lastDamageTime >= damageInterval)
    //            {
    //                Debug.Log("�Ȃ�����");
    //                gotower.TakeDamage(damage);
    //                lastDamageTime = Time.time;
    //            }
    //        }
    //    }

    //    //�������G�̓��ɍU��
    //    if (other.gameObject.CompareTag("Enemy'sTower") && CompareTag("Ally"))
    //    {
    //        //Debug.Log("Ally�ɂ���т���");
    //        Tower tower = other.gameObject.GetComponent<Tower>();
    //        if (tower != null)
    //        {
    //            if (Time.time - lastDamageTime >= damageInterval)
    //            {
    //                tower.TakeDamage(damage);
    //                lastDamageTime = Time.time;
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("nulL!!!!!!");
    //        }
    //    }
    //    //�G�������̓��ɍU��
    //    else if (other.gameObject.CompareTag("Ally'sTower") && CompareTag("Enemy"))
    //    {
    //        //Debug.Log("Enemy�ɂ���т���");
    //        Tower tower = other.gameObject.GetComponent<Tower>();
    //        if (tower != null)
    //        {
    //            if (Time.time - lastDamageTime >= damageInterval)
    //            {
    //                tower.TakeDamage(damage);
    //                lastDamageTime = Time.time;
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("nulL!!!!!!");
    //        }
    //    }
    //}
    //public void OnCollisionExit2D(Collision2D other)
    //{
    //    isHit = false;
    //}
    void Update()
    {
        //if (isHit && target == null)
        //{
        //    isHit = false;
        //}

        //Vector2 dirVector = Vector2.right * direction;

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVector,detectDistance,enemyLayer); //���ʂɂ���G���e�B�e�B�̎擾
        //if(hit.collider != null)
        //{
        //   // Debug.Log("���[������");
        //    target = hit.transform;  
        //}
        //else
        //{
        //    if (CompareTag("Ally"))
        //        target = GameObject.Find("Enemy'sTower").transform;
        //    else if (CompareTag("Enemy"))
        //        target = GameObject.Find("Ally'sTower").transform;
        //}

        //if (spawner.isSTOPED == false && spawner.isEND == false)
        //{
        //    if (isHit == false)
        //    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //}

        if (poison > 0 && !gameDirector.isEND)
        {
            sr.color = new Color(0.4f, 0.0f, 0.6f); // ���ɂ���
            poisonTimer += Time.deltaTime;
            if (poisonTimer >= 0.5f)
            {
                poisonTimer = 0f;
                hp -= (maxhp * 0.015f) + (hp * 0.01f) + 1;
                ParticleManager.Instance.PlayEffect("Poison", transform.position);
                poison--;
                if (hp <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            sr.color = originalColor; // �ł��؂ꂽ�猳�ɖ߂�
        }


        //target �� null ���j�󂳂�Ă��Ȃ����m�F
        if (target == null || target.gameObject == null)
        {
            target = FindClosestTarget();
        }

        // �U���Ώۂ����邩�`�F�b�N
        bool hasEnemyInRange = false;
        foreach (GameObject enemy in attackRange.enemiesInRange)
        {
            if (enemy != null)
            {
                hasEnemyInRange = true;
                break;
            }
        }

        // �G�����Ȃ���Έړ�
        if (!hasEnemyInRange && target != null && !gameDirector.isSTOPED && !gameDirector.isEND)
        {
            if (!noReverse)
            {
                Vector3 dir = target.position - transform.position;
                Vector3 scale = transform.localScale;
                scale.x = (dir.x < 0) ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (!gameDirector.isSTOPED && !gameDirector.isEND)
        {
            // �U������
            AttackClosestEnemy();
            
        }
    }

    // �ł��߂��G�����Ԃ�
    Transform FindClosestTarget()
    {
        float closestDist = float.MaxValue;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in attackRange.enemiesInRange)
        {
            if (enemy == null) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
            return closestEnemy.transform;

        // �G�����Ȃ���Ώ���^�[�Q�b�g��
        if (CompareTag("Ally"))
            return GameObject.Find("Enemy'sTower")?.transform;
        else
            return GameObject.Find("Ally'sTower")?.transform;
    }

    // �U������
    void AttackClosestEnemy()
    {
        if (Time.time - lastDamageTime < damageInterval) return;

        GameObject closestEnemy = null;
        float closestDist = float.MaxValue;

        foreach (GameObject enemy in attackRange.enemiesInRange)
        {
            if (enemy == null) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            GoTower enemyUnit = closestEnemy.GetComponent<GoTower>();
            Tower enemyTower = closestEnemy.GetComponent<Tower>();

            if (enemyUnit != null)
            {
                enemyUnit.TakeDamage(damage);
                if(OneAttack)
                    Destroy(gameObject);
            }
            else if (enemyTower != null)
            {
            
                enemyTower.TakeDamage(damage);
                if (OneAttack)
                    Destroy(gameObject);
            }

            lastDamageTime = Time.time;
            Debug.Log($"{name} �� {closestEnemy.name} ���U�������I");
            audioSource.PlayOneShot(sound1);
        }
    }
    //��_���[�W�p
    public void TakeDamage(float dmg)
    {
        if(poison > 0)
        {
            dmg *= 1.1f;
        }
       
        if (!Type_Metal)
        {
            Debug.Log($"{name} �� {dmg} �_���[�W���󂯂��I");
            hp -= dmg;
        }
        else
        {
            Debug.Log($"{name} �� {1} �_���[�W���󂯂��I");
            hp -= 1;
        }
        //Debug.Log("�Ă������߁[��");
        // hp = Mathf.Clamp(hp, 0, maxhp);

        if (particleManager != null)
        {
            particleManager.PlayEffect(transform.position);
            
        }
            

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public bool IsEnemy(GameObject obj)
    {
        if (CompareTag("Ally") && obj.CompareTag("Enemy")) return true;
        if (CompareTag("Enemy") && obj.CompareTag("Ally")) return true;
        return false;
    }
}
