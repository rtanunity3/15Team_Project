using UnityEngine;

public class Fruit : MonoBehaviour
{
    // NOTE: 프리펩 하나로 해서 세팅하려했으나
    // 과일 종류별로 프리펩을 만드는게 오버헤드가 적은것 같아 프리펩으로 해결

    public FruitsType type;

    private void Update()
    {
        // 좌표 이하로 떨어지는 경우 비활성화 풀에 반납
        if (gameObject.transform.position.y < -8)
        {
            gameObject.SetActive(false);
        }
    }
}
