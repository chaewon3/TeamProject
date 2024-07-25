using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInteractionEvent : MonoBehaviour
{
    public Sprite overSpr;
    public Sprite IdleSpr;
    public RectTransform Image;
    PlayerMove player;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        player = FindObjectOfType<PlayerMove>();
    }

    public void LoadGame(string scenename)
    {
        player.canMove = true;
        player.canRotat = true;
        GameManager.Instance.LoadScene(scenename);
    }

    public void PointerEnter()
    {
        button.image.sprite = overSpr;
        Image.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void PointerExit()
    {
        button.image.sprite = IdleSpr;
        Image.localScale = new Vector3(1,1,1);
    }

}
