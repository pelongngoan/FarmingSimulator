using System.Collections;
using UnityEngine;

public class DearRun : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float wanderRadius = 5f;
    public float wanderTimer = 2f;
    public Animator animator;

    private void Start()
    {
        StartCoroutine(RandomMovement());
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            // Chọn một hướng di chuyển ngẫu nhiên trong phạm vi của vùng di chuyển ngẫu nhiên
            Vector2 randomDirection = Random.insideUnitCircle.normalized * wanderRadius;

            // Tính toán vị trí mới dựa trên hướng và tốc độ di chuyển
            Vector2 newPosition = (Vector2)transform.position + randomDirection;

            // Giới hạn vị trí mới trong vùng di chuyển ngẫu nhiên
            newPosition.x = Mathf.Clamp(newPosition.x, transform.position.x - wanderRadius, transform.position.x + wanderRadius);
            newPosition.y = Mathf.Clamp(newPosition.y, transform.position.y - wanderRadius, transform.position.y + wanderRadius);

            // Xác định góc quay dựa trên hướng di chuyển
            float targetRotationY = (newPosition.x < transform.position.x) ? -180f : 0f;

            // Kích hoạt animation chạy
            animator.SetBool("running", true);

            // Di chuyển đối tượng đến vị trí mới
            while ((Vector2)transform.position != newPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);

                // Quay đối tượng dựa trên góc quay
                float currentRotationY = Mathf.LerpAngle(transform.eulerAngles.y, targetRotationY, 0.1f);
                transform.eulerAngles = new Vector3(0f, currentRotationY, 0f);

                yield return null;
            }

            // Kích hoạt animation đứng yên
            animator.SetBool("running", false);

            // Chờ một khoảng thời gian ngẫu nhiên trước khi di chuyển tiếp
            yield return new WaitForSeconds(Random.Range(1f, wanderTimer));
        }
    }
}