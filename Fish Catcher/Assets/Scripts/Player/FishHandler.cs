using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class FishHandler : NetworkBehaviour
{
    [SerializeField] private Animator _animatorCaña;
    [SerializeField] private Animator _animatorSelect;
    [SerializeField] private RectTransform _selectTransform;
    [SerializeField] private Animator _animatorTabla;
    public TMPro.TextMeshProUGUI txtScore;

    //[Networked(OnChanged = nameof(OnFishingChange))]
    bool IsFishing { get; set; }

    [Networked(OnChanged = nameof(OnScoreChanged))]
    float Score { get; set; }

    bool HaveFish { get; set; }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            if (networkInputData.isFishingPressed)
            {
                if (!IsFishing && !HaveFish)
                    Fish();
                else if(!HaveFish)
                    MiniGame();
            }
        }
    }

    void Fish()
    {
        GetComponent<CharacterMovementHandler>().blockInput = true;
        IsFishing = true;
        _animatorSelect.speed = 1;
        _animatorSelect.Play("Empty");
        _animatorCaña.Play("Throw");

    }

    void MiniGame()
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
                GetComponent<CharacterMovementHandler>().blockInput = false;
                HaveFish = true;
                IsFishing = false;
            }
            else
            {
                _animatorCaña.Play("Back");
                StartCoroutine(WaitUnlock());
            }
        }
    }

    //static void OnFishingChange(Changed<FishHandler> changed)
    //{

    //}

    static void OnScoreChanged(Changed<FishHandler> changed)
    {
        changed.Behaviour.txtScore.text = changed.Behaviour.Score.ToString();
    }

    public IEnumerator WaitUnlock()
    {
        yield return new WaitForSeconds(1.2f);
        _animatorSelect.gameObject.SetActive(false);
        GetComponent<CharacterMovementHandler>().blockInput = false;
        _animatorCaña.Play("Empty");
        IsFishing = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            if (networkInputData.isInteractPressed && HaveFish)
            {
                Score++;
                GetComponent<CharacterMovementHandler>().blockInput = true;
                _animatorTabla.Play("BackTabla");
                HaveFish = false;
            }
        }
    }



}
