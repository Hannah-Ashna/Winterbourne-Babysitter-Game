using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [Header("UI Sprite Image")]
    [SerializeField] private Image spriteImageObj;
    [SerializeField] private Sprite[] spriteArray;

    private float animSpeed = .3f;

    private int spriteIndex;
    Coroutine CoroutineAnim;
    bool isAnimating;

    public void playUIAnim()
    {
        isAnimating = true;
        StartCoroutine(doAnimation());
    }

    public void stopUIAnim()
    {
        isAnimating = false;
        StopCoroutine(doAnimation());
    }
    IEnumerator doAnimation()
    {
        yield return new WaitForSeconds(animSpeed);
        if (spriteIndex >= spriteArray.Length)
        {
            spriteIndex = 0;
        }
        spriteImageObj.sprite = spriteArray[spriteIndex];
        spriteIndex += 1;

        if (isAnimating == true)
            StopCoroutine(doAnimation());
            CoroutineAnim = StartCoroutine(doAnimation());

        Debug.Log(spriteIndex);
    }
}
