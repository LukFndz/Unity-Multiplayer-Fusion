using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class PlayerThrow : NetworkBehaviour
{
    [SerializeField] private Animator _animatorCaña;
    [SerializeField] private Animator _animatorSelect;
    [SerializeField] private RectTransform _selectTransform;
    [SerializeField] private Animator _animatorTabla;
    [SerializeField] private TMPro.TextMeshProUGUI _txtScore;
    private bool _isFishing;
    private bool _isInMinigame;

    private bool _haveFish;

    [Networked(OnChanged = nameof(ChangeScore))]
    public int score { get; set; }

    public Animator AnimatorSelect { get => _animatorSelect; set => _animatorSelect = value; }
    public Animator AnimatorTabla { get => _animatorTabla; set => _animatorTabla = value; }

    public void SetAnimSelect(Animator anim, RectTransform rect, TMPro.TextMeshProUGUI txt)
    {
        _animatorSelect = anim;
        _selectTransform = rect;
        _txtScore = txt;
    }

    private void Start()
    {
        FindObjectOfType<CanvasPlayer>().SetPlayerAnim(this);
        score = 0;
    }

    #region STANDALONE
    //void Update()
    //{
    //    if (Input.GetMouseButton(0) && !_isFishing)
    //    {
    //        _animatorSelect.speed = 1;
    //        _animatorSelect.Play("Empty");
    //        _animatorCaña.Play("Throw");
    //        _isFishing = true;
    //    }

    //    if(_isInMinigame)
    //    {
    //        if(Input.GetMouseButton(0))
    //        {
    //            _animatorSelect.speed = 0;

    //            if (_selectTransform.anchoredPosition.x > 49 && _selectTransform.anchoredPosition.x < 60)
    //            {
    //                _animatorCaña.Rebind();
    //                _animatorCaña.Update(0f);
    //                _animatorCaña.gameObject.SetActive(false);
    //                _animatorSelect.gameObject.SetActive(false);
    //                _animatorTabla.gameObject.SetActive(true);
    //                GetComponent<Player>().UnlockInputs();
    //                _haveFish = true;
    //            }
    //            else
    //            {
    //                _animatorCaña.Play("Back");
    //                StartCoroutine(WaitUnlock());
    //            }
    //        }
    //    }
    //}

    #endregion
    #region NETWORK
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            if (networkInputData.isFishPressed && !_isFishing)
            {
                _animatorSelect.speed = 1;
                _animatorSelect.Play("Empty");
                _animatorCaña.Play("Throw");
                _isFishing = true;
            }

            if (_isInMinigame)
            {
                if (Input.GetMouseButton(0))
                {
                    _animatorSelect.speed = 0;

                    if (_selectTransform.anchoredPosition.x > 49 && _selectTransform.anchoredPosition.x < 60)
                    {
                        _animatorCaña.Rebind();
                        _animatorCaña.Update(0f);
                        _animatorCaña.gameObject.SetActive(false);
                        _animatorSelect.gameObject.SetActive(false);
                        _animatorTabla.gameObject.SetActive(true);
                        GetComponent<Player>().UnlockInputs();
                        _haveFish = true;
                    }
                    else
                    {
                        _animatorCaña.Play("Back");
                        StartCoroutine(WaitUnlock());
                    }
                }
            }
        }
    }
    #endregion
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cofre")
        {
            if (Input.GetKeyDown(KeyCode.E) && _haveFish)
            {
                score++;
                GetComponent<Player>().BlockInputs();
                _animatorTabla.Play("BackTabla");
                _haveFish = false;
            }
        }
    }

    static void ChangeScore(Changed<PlayerThrow> changed)
    {
        float nScore = changed.Behaviour.score;
        changed.LoadOld();

        if(changed.Behaviour.score < nScore)
        {
            changed.Behaviour.SetScoreUI(nScore);
            Debug.Log("NEW: " + nScore);
        }
    }

    public void SetScoreUI(float nScore)
    {
        _txtScore.text = score.ToString();
        _isFishing = false;
        _isInMinigame = false;
        _animatorCaña.gameObject.SetActive(true);
    }
}
