using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scenes {
        MainMenuScene,
        GameScene,
        LoadingScene
    }

    private static Scenes targetScene;
    public static void Load(Scenes targetSceneName) {
        targetScene = targetSceneName;
        SceneManager.LoadScene(Scenes.LoadingScene.ToString());
    }
    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());

    }
}
