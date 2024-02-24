using System.Collections;
using UnityEngine;

public class MovableGameCell : MonoBehaviour
{
    private CellBase cellBase;
    private IEnumerator moveCoroutine;

    private void Awake()
    {
        cellBase = GetComponent<CellBase>();
    }



    public void Move(int newX, int newY, float animationDuration)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = MoveCoroutine(newX, newY, animationDuration);
        StartCoroutine(moveCoroutine);
    }

    public IEnumerator MoveCoroutine(int newX, int newY, float animationDuration)
    {
        cellBase.x = newX;
        cellBase.y = newY;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = cellBase.grid.GetWorldPosition(newX, newY);
        float elapsed = 0;

        while (elapsed < animationDuration)
        {
            float t = elapsed / animationDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }



        cellBase.transform.position = endPosition;
    }

}
