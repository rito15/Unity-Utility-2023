using UnityEngine;
using UnityEngine.SceneManagement;

// 2020. 05. 03.
/*
   [ 기록]
   2023. 03. 26.
     - Ut23 라이브러리에 편입
*/

namespace Rito.ut23
{
    public static class SceneLib
    {
        public static void ReloadSingleScene()
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name, 
                LoadSceneMode.Single
            );
        }
    }
}
