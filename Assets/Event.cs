using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Event : MonoBehaviour
{
    public int Day = 0; //???? ????
    public int windchance = 33; // ?? ???? ?????? ????
    public int Daytime = 600;
    public Player player;
    public Image Screen;
    private float time = 1000;
    private int PriateDay;
    private bool PriateInvade;
    public bool mainisland;
    private bool ship = false;
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        PriateDay = 6 + Random.Range(-2, 2);
        Debug.Log($"?????????? {PriateDay}");
    }

    // Update is called once per frame
    void Update()
    {
        if (time > Daytime)
        {
            Day++;

            //Text.GetComponent<TextMeshProUGUI>().text = "Day " + Day;


            Debug.Log($"???? ?? {Day}");
            time = 0;
            if (ship)
            {
                if (Random.Range(1, 101) > windchance)
                {
                    //?????? ?? ???????? ?????? ?????? ?????? ??????.
                }
            }
            if (Day >= PriateDay)
            {
                PriateEvent();
                PriateDay = (Day + 6) + Random.Range(-2, 2);
                Debug.Log($"???? ???? ?????? {PriateDay}");
            }
        }
        if (!PriateInvade)
        {
            if (mainisland)
                time += Time.deltaTime;
            else if (ship)
            {
                time += Time.deltaTime * 25;
            }
        }
    }
    void PriateEvent()
    {
        PriateInvade = true;
        if (ship)
        {

            //?? ?????? ???????? ???????? ???????? ???????? ???????? ??????.
        }
        else
        {
            StartCoroutine(Priate());
        }
    }
    IEnumerator Priate()
    {
        Debug.Log("???????????? ?????????? ????");
        for (float alpah = 0.1f; alpah <= 1.1f; alpah += 0.1f)
        {
            Screen.color = new Color(0f, 0f, 0f, alpah);
            yield return new WaitForSeconds(0.05f);
        }
        SoundManager.Instance.ChangeClip("????", true);

        //?????? ???? ?????? ?????? ???? ???????? ????
        player.Invoke("Pirate", 0);
        yield return new WaitForSeconds(2);
        SoundManager.Instance.ChangeClip("????", false);
        yield return new WaitForSeconds(1);
        for (float alpah = 1f; alpah >= -0.1f; alpah -= 0.1f)
        {
            Screen.color = new Color(0f, 0f, 0f, alpah);
            yield return new WaitForSeconds(0.05f);
        }
        PriateInvade = false;
        yield return new WaitForSeconds(0.1f);
    }
}
