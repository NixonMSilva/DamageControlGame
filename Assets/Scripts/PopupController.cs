using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public Animator anim;

    public TextMeshProUGUI scoreText;

    void Start ()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    public void SetText (string text, Color color)
    {
        scoreText.text = text;
        scoreText.color = color;
        scoreText.faceColor = color;
    }
}
