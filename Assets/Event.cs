using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< Updated upstream
using TMPro;
=======
>>>>>>> Stashed changes

public class Event : MonoBehaviour
{
    public int Day=0; //���� ��¥
    public int windchance = 33; // �� Ÿ�� �ٶ��� Ȯ��
    public int Daytime = 600;
    public Player player;
    public Image Screen;
<<<<<<< Updated upstream
    private float time=1000;
    private int PriateDay;
    private bool PriateInvade;
=======
    private float time;
    private int PriateDay;
    private bool PriateInvade;
    public bool mainisland;
>>>>>>> Stashed changes
    private bool ship;
    public bool mainisland;
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        PriateDay = 6 + Random.Range(-2, 2);
        Debug.Log($"���������� {PriateDay}");
    }

    // Update is called once per frame
    void Update()
    {
        if (time > Daytime)
        {
            Day++;
<<<<<<< Updated upstream
            Text.GetComponent<TextMeshProUGUI>().text ="Day "+ Day;
=======
>>>>>>> Stashed changes

            Debug.Log($"���� �� {Day}");
            time = 0;
            if (ship)
            {
                if (Random.Range(1, 101) > windchance)
                {
                    //����� �� �ý����� ���ܾ� ����� ������ �մϴ�.
                }
            }
            if (Day >= PriateDay)
            {
                PriateEvent();
                PriateDay = (Day + 6) + Random.Range(-2, 2);
                Debug.Log($"���� ���� ������ {PriateDay}");
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

            //�� �α��� ��ü���� �ý����� ������ �г�Ƽ�� �ο��ҵ� �մϴ�.
        }
        else
        {
            StartCoroutine(Priate());
        }
    }
    IEnumerator Priate()
    {
        Debug.Log("���μ������� �����̺�Ʈ �ߵ�");
        for (float alpah = 0.1f; alpah <= 1.1f; alpah += 0.1f)
        {
            Screen.color = new Color(0f, 0f, 0f, alpah);
<<<<<<< Updated upstream
            yield return new WaitForSeconds(0.05f);
        }
        //Firework �Ҹ����

        //�̰��� �κ� ���ִ� �Լ��� �Բ� ���μ��� �Լ�
        player.Invoke("Pirate", 0);
        yield return new WaitForSeconds(3);
        for (float alpah = 1f; alpah >= -0.1f; alpah -= 0.1f)
        {
            Screen.color = new Color(0f, 0f, 0f, alpah);
            yield return new WaitForSeconds(0.05f);
        }
        PriateInvade = false;
=======
            yield return new WaitForSeconds(0.1f);
        }
        //Firework �Ҹ����
        yield return new WaitForSeconds(5);
        for (float alpah = 1f; alpah >= -0.1f; alpah -= 0.1f)
        {
            Screen.color = new Color(0f, 0f, 0f, alpah);
            yield return new WaitForSeconds(0.1f);
        }
        player.Invoke("Pirate", 0);
>>>>>>> Stashed changes
    }
}
