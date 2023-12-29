using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
  모든씬에 걸쳐 사용될 기능들
*/
public class SettingManager : MonoBehaviour
{
    private void Awake()
    {
        Object.DontDestroyOnLoad(this.gameObject);
    }
    public void restart() //스테이지 재시작하기
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    //게임종료
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
