using System.Collections;
using System.Collections.Generic;  //6/8추가
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; //적 프리팹
    [SerializeField]
    private float spawnTime;  //적 생성 주기
    [SerializeField]
    private Transform[] wayPoints; //현재 스테이지의 이동 경로
    private List<Enemy> enemyList; // 현재 맵에 존재하는 모든 적의 정보, 6/8추가

    //적의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요 없다
    public List<Enemy> EnemyList => enemyList;  // 6/8추가

    private void Awake()
    {
        //적 리스트 메모리 할당
        enemyList = new List<Enemy>();  // 6/8 추가
        //적 생성 코루틴 함수 호출
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab); //적 오브젝트 생성
            Enemy enemy = clone.GetComponent<Enemy>(); //방금 생성된 적의 enemy컴포넌트 스크립트 가져오기

            enemy.Setup(this, wayPoints); //wayPoint 정보를 매개변수로 Setup()호출 , 이동경로를 알려주는거
            enemyList.Add(enemy);   //리스트에 방금 생성된 적 정보 저장

            yield return new WaitForSeconds(spawnTime); //spawnTime 시간동안 대기
        }
    }

    public void DestroyEnemy(Enemy enemy)
    {
        //리스트에서 사망하는 적 정보 삭제
        enemyList.Remove(enemy);
        //적 오브젝트 삭제
        Destroy(enemy.gameObject);
    }
}
