using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoTower : MonoBehaviour
{
    //private float speed = 5.0f;
    [SerializeField] Transform target;  //移動する場所、目的地
    [SerializeField] float speed;       //移動速度
    [SerializeField] float maxhp;          //最大体力
   /* [HideInInspector]*/ public float hp;
    [SerializeField] float damage;      //与えるダメージ
    //[SerializeField] private float detectDistance = 5f; //感知する距離
    [SerializeField] private LayerMask enemyLayer;      //感知するエンティティの種類の設定用
    [SerializeField] float GetChance;      //この敵をゲットできる確率
    //[SerializeField] private int direction = 1;         //敵を感知する方向用
    public float damageInterval = 1f;  //ダメージを与える間隔
    public int poison = 0;             //毒のダメージを受ける回数
    private float poisonTimer = 0f;    //毒の時間経過用
    private float lastDamageTime = 0f; //最後にダメージを与えた時間
    public bool Type_Metal = false;    //被ダメージ1ダメージ固定
    public bool OneAttack = false;     //1回攻撃したら消える
    public int PoisonOnAttack = 0;     //与える毒の量
    //private bool isHit = false;        //ぶつかっているかどうか
    public bool noReverse = false;     //反転処理するかどうか

    public GameObject attacker;
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

    private ParticleManager particleManager; //パーティクル用

    [Header("倒したときの報酬キャラ名")]
    public string rewardCharacterName;

    private PartyManager partyManager;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color; // 元の色を保存
    }

    private void Start()
    {
        if (speed <= 0) //デフォルトのスピード
            speed = 1.5f;
        if (maxhp <= 0) //デフォルトの体力
            maxhp = 40;
       
        hp = maxhp;
        //targetの設定
        if (CompareTag("Ally")) 
        target = GameObject.Find("Enemy'sTower").transform;
        else if (CompareTag("Enemy"))
            target = GameObject.Find("Ally'sTower").transform;
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        this.director = GameObject.Find("GameDirector");
        gameDirector = director.GetComponent<GameDirector>();
        attackRange = GetComponentInChildren<AttackRange>();  //子オブジェクトの参照

        particleManager = FindFirstObjectByType<ParticleManager>();
        audioSource = GetComponent<AudioSource>();
        partyManager = FindFirstObjectByType<PartyManager>();
}
/* public void OnCollisionEnter2D(Collision2D other)
 {
     //Debug.Log("ぶつかった");
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
             Debug.Log("敗北");
             spawner.isEND = true;
         }
         else  if (other.gameObject.CompareTag("Enemy'sTower") && CompareTag("Ally"))
         {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Debug.Log("勝利");
            spawner.isEND = true;
         }
 }*/

//public void OnCollisionStay2D(Collision2D other)
//{


//    GoTower gotower = other.gameObject.GetComponent<GoTower>();

//    //if(gotower == null )
//    //{
//    //   Debug.Log("Gotowerがnull");
//    //}

//    //衝突したら攻撃
//    if (gotower != null && (spawner.isSTOPED == false && spawner.isEND == false))
//    {
//        if (other.gameObject.CompareTag("Enemy") && CompareTag("Ally"))
//        {
//            isHit = true;
//            //Debug.Log("ぶつかってる");
//            if (Time.time - lastDamageTime >= damageInterval)
//            {
//                Debug.Log("なぐった");
//                gotower.TakeDamage(damage);
//                lastDamageTime = Time.time;
//            }
//        }
//        else if (other.gameObject.CompareTag("Ally") && CompareTag("Enemy"))
//        {
//            isHit = true;
//            //Debug.Log("ぶつかってる");
//            if (Time.time - lastDamageTime >= damageInterval)
//            {
//                Debug.Log("なぐった");
//                gotower.TakeDamage(damage);
//                lastDamageTime = Time.time;
//            }
//        }
//    }

//    //味方が敵の塔に攻撃
//    if (other.gameObject.CompareTag("Enemy'sTower") && CompareTag("Ally"))
//    {
//        //Debug.Log("Allyによるよびだし");
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
//    //敵が味方の塔に攻撃
//    else if (other.gameObject.CompareTag("Ally'sTower") && CompareTag("Enemy"))
//    {
//        //Debug.Log("Enemyによるよびだし");
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

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVector,detectDistance,enemyLayer); //正面にいるエンティティの取得
        //if(hit.collider != null)
        //{
        //   // Debug.Log("たーげっと");
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
            sr.color = new Color(0.4f, 0.0f, 0.6f); // 紫にする
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
            sr.color = originalColor; // 毒が切れたら元に戻す
        }


        //target が null か破壊されていないか確認
        if (target == null || target.gameObject == null)
        {
            target = FindClosestTarget();
        }

        // 攻撃対象がいるかチェック
        bool hasEnemyInRange = false;
        foreach (GameObject enemy in attackRange.enemiesInRange)
        {
            if (enemy != null)
            {
                hasEnemyInRange = true;
                break;
            }
        }

        // 敵がいなければ移動
        if (!hasEnemyInRange && target != null && !gameDirector.isSTOPED && !gameDirector.isEND)
        {
           
                Vector3 dir = target.position - transform.position;
             if (!noReverse)
            {
                Vector3 scale = transform.localScale;
                scale.x = (dir.x < 0) ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (!gameDirector.isSTOPED && !gameDirector.isEND)
        {
            // 攻撃処理
            AttackClosestEnemy();
            
        }
    }

    // 最も近い敵か城を返す
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

        // 敵がいなければ城をターゲットに
        if (CompareTag("Ally"))
            return GameObject.Find("Enemy'sTower")?.transform;
        else
            return GameObject.Find("Ally'sTower")?.transform;
    }

    // 攻撃処理
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
                if (PoisonOnAttack > 0)
                {
                    enemyUnit.poison += PoisonOnAttack;
                }
                enemyUnit.attacker = this.gameObject;
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
            Debug.Log($"{name} が {closestEnemy.name} を攻撃した！");

            if(audioSource != null)
            audioSource.PlayOneShot(sound1);
        }
    }
    //被ダメージ用
    public void TakeDamage(float dmg)
    {
        if(poison > 0)
        {
            dmg *= 1.1f;
        }
       
        if (!Type_Metal)
        {
            Debug.Log($"{name} が {dmg} ダメージを受けた！");
            hp -= dmg;
            hp = Mathf.Max(hp, 0);
        }
        else
        {
            Debug.Log($"{name} が {1} ダメージを受けた！");
            hp -= 1;
            hp = Mathf.Max(hp, 0);
        }
        //Debug.Log("ていくだめーじ");
        // hp = Mathf.Clamp(hp, 0, maxhp);

        if (particleManager != null)
        {
            ParticleManager.Instance.PlayEffect("Hit", transform.position);

        }
            

        if (hp <= 0)
        {
            if (CompareTag("Enemy"))
            {
                // 味方に倒された場合
                if (attacker != null && attacker.CompareTag("Ally"))
                {
                    //float chance = 0.05f; // 5%
                    //float chance = 1.00f; // 100%
                    if (Random.value <= GetChance)
                    {
                        // PartyManagerからキャラクター取得
                        CharacterData rewardCharacter = PartyManager.Instance.GetCharacterByName(rewardCharacterName);
                        if (rewardCharacter != null && !rewardCharacter.isOwned)
                        {
                            rewardCharacter.isOwned = true;
                            PlayerData.SaveCharacterOwned(rewardCharacter.characterName);
                            Debug.Log($"{rewardCharacter.characterName} を獲得しました！");
                        }
                    }
                }
            }
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
