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

    public void ActivateGeneratonIdle(bool isGenerating) {
        if (isGenerating) hiveAnimator.SetTrigger("generatingHoney");
        else hiveAnimator.ResetTrigger("generatingHoney");
    }
}
