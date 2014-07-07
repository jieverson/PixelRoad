using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour
{

    public AudioSource selectSound;
    public Transform heroSprite, blackMageSprite, linkSprite;
    public Transform heroPrefeb, blackMagePrefeb, linkPrefeb;

    private enum Action { None, Hero, BlackMage, Link }

    private Action _action;

    void Start()
    {

    }

    void Update()
    {
        if (_action == Action.None)
        {
            if (Input.GetButton("Back"))
            {
                Application.LoadLevel(0);
            }

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Hit(Input.GetTouch(0).position);
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                Hit(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            }
        }
        else if (!selectSound.isPlaying)
        {
            Application.LoadLevel(2);
        }

        if (_action != Action.None)
        {
            heroSprite.renderer.enabled = false;
            blackMageSprite.renderer.enabled = false;
            linkSprite.renderer.enabled = false;
            if (_action == Action.Hero)
            {
                heroSprite.renderer.enabled = true;
            }
            else if (_action == Action.BlackMage)
            {
                blackMageSprite.renderer.enabled = true;
            }
            else if (_action == Action.Link)
            {
                linkSprite.renderer.enabled = true;
            }
        }
    }

    private void Hit(Vector2 pos)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
        if (hitInfo)
        {
            if (hitInfo.transform == heroSprite)
            {
                _action = Action.Hero;
                Hero.SelectedHero = heroPrefeb;
                selectSound.Play();
            }
            else if (hitInfo.transform == blackMageSprite)
            {
                _action = Action.BlackMage;
                Hero.SelectedHero = blackMagePrefeb;
                selectSound.Play();
            }
            else if (hitInfo.transform == linkSprite)
            {
                _action = Action.Link;
                Hero.SelectedHero = linkPrefeb;
                selectSound.Play();
            }
        }
    }

}
