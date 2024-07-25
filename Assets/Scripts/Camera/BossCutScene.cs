using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutScene : MonoBehaviour, IInteractable
{
    public void interaction(bool OnOff)
    {
        CanvasManager.ShowBossHPBar();
        SoundManager.Instance.BossBGM();
    }

}
