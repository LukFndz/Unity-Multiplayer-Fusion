using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private Animator _animatorCaña;
    [SerializeField] private Animator _animatorSelect;
    [SerializeField] private RectTransform _selectTransform;
    [SerializeField] private Animator _animatorTabla;
    private bool _isFishing;
    private bool _isInMinigame;

    public Animator AnimatorSelect { get => _animatorSelect; set => _animatorSelect = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !_isFishing)
        {
            _animatorSelect.speed = 1;
            _animatorSelect.Play("Empty");
            _animatorCaña.Play("Throw");
            _isFishing = true;
        }

        if(_isInMinigame)
        {
            if(Input.GetMouseButton(0))
            {
                _animatorSelect.speed = 0;

                if (_selectTransform.anchoredPosition.x > 49 && _selectTransform.anchoredPosition.x < 60)
                {
                    _animatorCaña.gameObject.SetActive(false);
                    _animatorSelect.gameObject.SetActive(false);
                    _animatorTabla.gameObject.SetActive(true);
                    GetComponent<Player>().UnlockInputs();
                    _isFishing = false;
                    _isInMinigame = false;
                }
                else
                {
                    _animatorCaña.Play("Back");
                    StartCoroutine(WaitUnlock());
                }
            }
        }

    }

    public void StartMiniGame()
    {
        _isInMinigame = true;
    }

    public IEnumerator WaitUnlock()
    {
        yield return new WaitForSeconds(1.2f);
        _animatorSelect.gameObject.SetActive(false);
        GetComponent<Player>().UnlockInputs();
        _animatorCaña.Play("Empty");
        _isInMinigame = false;
        _isFishing = false;
    }
}
