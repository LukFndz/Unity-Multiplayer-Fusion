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
    private bool _blockFish;
    private bool _haveFish;

    private int _score;

    private bool _onZone = false;
    public Animator AnimatorSelect { get => _animatorSelect; set => _animatorSelect = value; }
    public Animator AnimatorTabla { get => _animatorTabla; set => _animatorTabla = value; }
    public bool OnZone { get => _onZone; set => _onZone = value; }

    public void SetAnimSelect(Animator anim, RectTransform rect, TMPro.TextMeshProUGUI txt)
    {
        _animatorSelect = anim;
        _selectTransform = rect;
        _txtScore = txt;
    }

    private void Start()
    {
        FindObjectOfType<CanvasPlayer>().SetPlayerAnim(this);
        _score = 0;
    }

    public override void Spawned()
    {
        GetComponent<PlayerThrow>().BlockFish();
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
        if(!_blockFish)
        {
            if (GetInput(out NetworkInputData networkInputData))
            {
                if (networkInputData.isFishPressed && !_isFishing && _onZone)
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
                        UnlockFish();
                        _animatorSelect.speed = 0;

                        if (_selectTransform.anchoredPosition.x > 49 && _selectTransform.anchoredPosition.x < 61)
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
                _score++;
                SendScore();
                GetComponent<Player>().BlockInputs();
                _animatorTabla.Play("BackTabla");
                _haveFish = false;
            }
        }
    }

    public void SetScoreUI()
    {
        _txtScore.text = _score.ToString();
        _isFishing = false;
        _isInMinigame = false;
        _animatorCaña.gameObject.SetActive(true);
    }

    public void SendScore()
    {
        RPC_SendScore(transform.name,_score);
    }

    [Rpc(RpcSources.All , RpcTargets.All)]
    public void RPC_SendScore(string name, int score, RpcInfo info = default)
    {
        FindObjectOfType<ScoreManager>().RPC_SetScore(transform.name, _score); 
    }
    
    public void BlockFish()
    {
        _blockFish = true;
    }

    public void UnlockFish()
    {
        _blockFish = false;
    }

}

