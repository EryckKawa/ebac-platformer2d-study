using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;

    public KeyCode keyToTrigger = KeyCode.G;
    public KeyCode keyToExit = KeyCode.H;

    public string triggerToPlay = "GrowthBool";

    private void OnValidate()
    {
        if (animator == null) animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(keyToTrigger))
        {
            animator.SetBool(triggerToPlay, true);
        }
        else if (Input.GetKeyDown(keyToExit))
        {
            animator.SetBool(triggerToPlay, false);
        }
    }
}
