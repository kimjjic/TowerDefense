using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;

    public void Setup(Transform target)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;   //  타워가 설정해준 타겟
    }

    private void Update()
    {
        if (target != null)  //  타겟이 존재하면
        {
            // 발사체를 타겟의 위치로 이동
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else   // 타겟이 사라지면
        {
            //발사체 오브젝트 삭제
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;  //적이 아닌 대상과 부딪히면
        if (collision.transform != target) return;  // 현재 타겟인 적이 아닐때

        collision.GetComponent<Enemy>().OnDie();
        Destroy(gameObject);
}
}

//타워가 발사하는 기본 발사체에 부탁

//기능
//Update() - 타겟이 존재하면 타겟 방향으로 이동하고 , 타겟이 존재하지 않으면 발사체 삭제
//OnTriggerEnter2D() - 타겟으로 설정된 적과 부딪혔을때 둘다 삭제
