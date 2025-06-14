using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] EnemyNotice noticeScript;
    [SerializeField] playerHealth healthScript;
    [SerializeField] float cooldown = 0f;
   
    void Start() {
     noticeScript = gameObject.GetComponent<EnemyNotice>();
    GameObject player = GameObject.FindWithTag("Player");
    healthScript = player.GetComponent<playerHealth>();
    }
   
    void Update() {
       
        cooldown = cooldown + Time.deltaTime;
        if (noticeScript.inSight)
        {
            StartCoroutine(Shooting());
        }
    }
    IEnumerator Shooting()
    {

            yield return new WaitForSeconds(3f);
        if (cooldown > 2)
        {
            GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation);
            Vector3 unitVector = noticeScript.lookPos.normalized;
            bulletClone.GetComponent<Rigidbody>().AddForce(unitVector * 400);
            Destroy(bulletClone, 3);
            cooldown = 0;
        }
          

    }
   
}
