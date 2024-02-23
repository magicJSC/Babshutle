using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutSceneSignal : MonoBehaviour
{
    public void StartCut()
    {
        Managers.Game.player.gameObject.SetActive(false);
        Managers.Game.gu.gameObject.SetActive(false);
    }

    public void EndCut()
    {
        Managers.Game.player.gameObject.SetActive(true);
        Camera.main.gameObject.GetComponent<CameraContrroller>().SetTarget();
    }

    public void GameStart()
    {
        Managers.Game.player.SetActive(true);
        Managers.Game.gu.gameObject.SetActive(true);
        Managers.Game.gu.GetComponent<GUController>().target = Managers.Game.player;
        Camera.main.gameObject.GetComponent<CameraContrroller>().SetTarget();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Main");
    }

    public void NoSound()
    {
        Managers.Game.desk.GetComponent<AudioSource>().enabled = false;
    }
}
