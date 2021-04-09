using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] Text text;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText(int value)
    {
        text.text = value.ToString();
        animator.SetTrigger("Change");
    }
}
