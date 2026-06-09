using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount;   //이동경로 개수
    private Transform[] wayPoints;  //이동경로 정보
    private int currentIndex = 0;   //현재 목표지점 인덱스
    private Movement2D movement2D;  //오브젝트 이동 제어
    private EnemySpawner enemySpawner;  // 적의 삭제를 본인이 하지 않고 EnemySpawner에 알려서 하기

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)    //적 초기설정
    {
        movement2D = GetComponent<Movement2D>();   //같은 오브젝트에 붙은 무브먼트 스크립트 찾기
        this.enemySpawner = enemySpawner;

        //적 이동 경로 wayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;    //이동경로 배열 저장

        //적의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;   

        //적 이동/ 목표지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        //다음 이동 방향 설정
        NextMoveTo();

        while (true)
        {
            //적의 현재위치와 목표위치의 거리가 0.02*movement2D.moveSpeed보다 작을때 if조건문 실행
            //Tip. movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직일 수 있따
            //if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있다
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                //다음 이동방향 설정
                NextMoveTo();
            }

            yield return null;   // 1프레임 기다렸다가 다시 진행 (매 프레임 반복)

        }
    }

    private void NextMoveTo() //다음 ㅇ
    {
        //아직 이동할 wayPoint가 남아있다면
        if (currentIndex < wayPointCount - 1)
        {
            //적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;
            //이동 방향 설정 => 다음 목표지점(wayPoints)
            currentIndex++;   
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;   // 현재 위치 → 다음 waypoint 방향 계산.
            movement2D.MoveTo(direction);
        }
        //현재 위치가 마지막 wayPoints라면
        else
        {
            //적 오브젝트 삭제
            //Destroy(gameObject);
            OnDie();
        }
    }

    public void OnDie()
    {
        //EnemySpawner에서 리스트로 적 정보를 관리하기 때문에 Destroy()를 직접 하지 않고
        //EnemySpawner에게 본인이 삭제될 때 필요한 처리를 하도록 DestroyEnemy() 함수 호출
        enemySpawner.DestroyEnemy(this);
    }
}