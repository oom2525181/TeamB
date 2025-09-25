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
    private float hp;
    [SerializeField] float damage;      //�^����_���[�W
    [SerializeField] private float detectDistance = 5f; //���m���鋗��
    [SerializeField] private LayerMask enemyLayer;      //���m����G���e�B�e�B�̎�ނ̐ݒ�p
    [SerializeField] private int direction = 1;         //�G�����m��������p
    public float damageInterval = 1f;  //�_���[�W��^����Ԋu
    private float lastDamageTime = 0f; //�Ō�Ƀ_���[�W��^��������
    private bool isHit = false;        //�Ԃ����Ă��邩�ǂ���
    
    public EnemySpawner spawner;
    
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

    public void OnCollisionStay2D(Collision2D other)
    {
       

        GoTower gotower = other.gameObject.GetComponent<GoTower>();

        //if(gotower == null )
        //{
        //   Debug.Log("Gotower��null");
        //}

        //�Փ˂�����U��
        if (gotower != null)
        {
            if (other.gameObject.CompareTag("Enemy") && CompareTag("Ally"))
            {
                isHit = true;
                //Debug.Log("�Ԃ����Ă�");
                if (Time.time - lastDamageTime >= damageInterval)
                {
                    Debug.Log("�Ȃ�����");
                    gotower.TakeDamage(damage);
                    lastDamageTime = Time.time;
                }
            }
            else if (other.gameObject.CompareTag("Ally") && CompareTag("Enemy"))
            {
                isHit = true;
                //Debug.Log("�Ԃ����Ă�");
                if (Time.time - lastDamageTime >= damageInterval)
                {
                    Debug.Log("�Ȃ�����");
                    gotower.TakeDamage(damage);
                    lastDamageTime = Time.time;
                }
            }
        }

        //�������G�̓��ɍU��
        if (other.gameObject.CompareTag("Enemy'sTower") && CompareTag("Ally"))
        {
            //Debug.Log("Ally�ɂ���т���");
            Tower tower = other.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                if (Time.time - lastDamageTime >= damageInterval)
                {
                    tower.TakeDamage(damage, 1);
                    lastDamageTime = Time.time;
                }
            }
            else
            {
                Debug.Log("nulL!!!!!!");
            }
        }
        //�G�������̓��ɍU��
        else if (other.gameObject.CompareTag("Ally'sTower") && CompareTag("Enemy"))
        {
            //Debug.Log("Enemy�ɂ���т���");
            Tower tower = other.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                if (Time.time - lastDamageTime >= damageInterval)
                {
                    tower.TakeDamage(damage, 2);
                    lastDamageTime = Time.time;
                }
            }
            else
            {
                Debug.Log("nulL!!!!!!");
            }
        }
    }
    public void OnCollisionExit2D(Collision2D other)
    {
        isHit = false;
    }
    void Update()
    {
        if (isHit && target == null)
        {
            isHit = false;
        }

        Vector2 dirVector = Vector2.right * direction;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVector,detectDistance,enemyLayer); //���ʂɂ���G���e�B�e�B�̎擾
        if(hit.collider != null)
        {
           // Debug.Log("���[������");
            target = hit.transform;  
        }
        else
        {
            if (CompareTag("Ally"))
                target = GameObject.Find("Enemy'sTower").transform;
            else if (CompareTag("Enemy"))
                target = GameObject.Find("Ally'sTower").transform;
        }

        if (spawner.isSTOPED == false && spawner.isEND == false)
        {
            if (isHit == false)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    //��_���[�W�p
    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log("�Ă������߁[��");
       // hp = Mathf.Clamp(hp, 0, maxhp);
       if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
