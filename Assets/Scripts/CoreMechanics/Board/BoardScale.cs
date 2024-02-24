using UnityEngine;

public class BoardScale : MonoBehaviour
{
    public void ResizeBoard(int columnCount)
    {
        
        if (!TryGetComponent<SpriteRenderer>(out var sr))
            return;

        transform.localScale = Vector3.one;

        var width = sr.sprite.bounds.size.x;

        var worldScreenHeight = Camera.main.orthographicSize / (columnCount / 2.4f);

        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(worldScreenWidth / width, worldScreenWidth / width, 1f);
    }
}