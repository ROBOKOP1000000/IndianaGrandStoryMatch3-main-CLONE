using System.Collections;
using UnityEngine;

public class ClearableGameCell : MonoBehaviour
{

    public bool isBeingCleared { get; private set; }
    [SerializeField] private bool UseAnimation;

    [HideInInspector] public CellBase cell;
    [SerializeField] private float animationDuration = 0.125f;
    [SerializeField] private AnimationClip clearAnimation;
    private Animator anim;

    private void Awake()
    {
        cell = GetComponent<CellBase>();
        anim = GetComponent<Animator>();
    }



    public virtual void Clear()
    {

        cell.grid.level.OnCellCleared(cell);
        isBeingCleared = true;
        if (UseAnimation)
        {
            ClearAnimation();
        }
        else
        {
            StartCoroutine(ClearCoroutine());
        }

    }

    private IEnumerator ClearCoroutine()
    {
        float elapsed = 0;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;


        while (elapsed < animationDuration)
        {
            float t = elapsed / animationDuration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator ClearAnimation()
    {
        if (anim)
        {
            anim.Play(clearAnimation.name);
            yield return new WaitForSeconds(clearAnimation.length);
            Destroy(gameObject);
        }
    }
}
