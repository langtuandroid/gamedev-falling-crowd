using UnityEngine;
using Zenject;

namespace MainManagers
{
    public class SceneContext : MonoInstaller
    {
        // [SerializeField]
        // private GameManagerfc _gameManagerfc;
        
        public override void InstallBindings()
        {
           // Container.Bind<GameManagerfc>().FromInstance(_gameManagerfc).AsSingle();
        }
    }
}
