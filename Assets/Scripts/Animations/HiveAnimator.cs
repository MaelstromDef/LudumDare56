using UnityEngine;

public class HiveAnimator : MonoBehaviour
{
    Animator hiveAnimator;

    private void Start()
    {
        hiveAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        hiveAnimator.SetTrigger("isClicked");
    }
}
