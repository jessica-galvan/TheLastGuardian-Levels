using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private string _title = "Title";
    [SerializeField, TextArea] private string _description = "Description";
    
    [Header("Canvas")]
    [SerializeField] private TextMeshProUGUI _titleText = null;
    [SerializeField] private TextMeshProUGUI _descriptionText = null;

    [Header("Settings")]
    [SerializeField] private float _disappearTimer = 1.0f;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _titleText.text = _title;
        _descriptionText.text = _description;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _animator.SetTrigger("doAppear");
            Invoke("Disappear", _disappearTimer);
        }
    }

    private void Disappear()
    {
        _animator.SetTrigger("doDisappear");
    }
}
