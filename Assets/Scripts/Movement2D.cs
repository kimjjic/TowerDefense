using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed => moveSpeed;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;   // 현재위치 += 방향*속도*1프레임에 걸린시간(이거 추가해야 컴퓨터 사양 달라도 같은 속도)
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
