using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] Player player;
    // Start is called before the first frame update
    // Update is called once per frame
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().flipX = !player.GetComponent<SpriteRenderer>().flipX;
    }
    public void CanCatchFish()
    {
        player.CanCatchFish = true;
        print("canCatch");
    }
    public void Catch()
    {
        GetComponent<Animator>().SetBool("Catch", true);
    }
    public void FishingEnd()
    {
        player.FishingEnd();
    }
}
