using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainManagers
{
    public static class ApplicationUtils
    {
        public static async UniTask OpenURLAsync(string url)
        {
            await UniTask.DelayFrame(1); // Ждем один кадр, чтобы избежать блокировки основного потока
            try
            {
                Application.OpenURL(url); // Открываем ссылку
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при открытии ссылки {url}: {e.Message}");
            }
        }
    }
}