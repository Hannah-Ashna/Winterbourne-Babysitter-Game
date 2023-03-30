using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    [Header("UI Sprite Image")]
    [SerializeField] private Image spriteImageObj;
    [SerializeField] private Sprite[] spriteArray;

    public float animSpeed = .4f;

    private int spriteIndex;
    Coroutine CoroutineAnim;
    bool isAnimating;

    void Start()
    {
        isAnimating = true;
        StartCoroutine(doAnimation());
    }

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
    }
}
