using UnityEngine;
using UnityEngine.SceneManagement;

// 2020. 05. 03.
/*
   [ ���]
   2023. 03. 26.
     - Ut23 ���̺귯���� ����
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
