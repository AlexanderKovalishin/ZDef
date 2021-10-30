using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.Game.Data;
using ZDef.Game.UI;

namespace ZDef.Game
{
    public class GameScenarioController: MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private float _startTimout = 1f;
        [SerializeField] private UILoadingFader _loadingFader;
        [SerializeField] private UIRestartWindow _victoryWindow;
        [SerializeField] private UIRestartWindow _defeatWindow;

        private EventBus _eventBus;
        private int _enemiesCount;

        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<VictoryEvent>(VictoryEventListener);
            _eventBus.Subscribe<DefeatEvent>(DefeatEventListener);
            _eventBus.Subscribe<ReturnEnemyEvent>(ReturnEnemyEvent);
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }

        private void UnSubscribe()
        {
            _eventBus.UnSubscribe<VictoryEvent>(VictoryEventListener);
            _eventBus.UnSubscribe<DefeatEvent>(DefeatEventListener);
            _eventBus.UnSubscribe<ReturnEnemyEvent>(ReturnEnemyEvent);
        }
        
        private void ReturnEnemyEvent(ReturnEnemyEvent args)
        {
            _enemiesCount--;
            if (_enemiesCount > 0) return;
            UnSubscribe();
            StartCoroutine(ShowFaderAndRestart(_victoryWindow));
        }
        
        private void VictoryEventListener(VictoryEvent args)
        {
            UnSubscribe();
            StartCoroutine(ShowFaderAndRestart(_victoryWindow));
        }

        private void DefeatEventListener(DefeatEvent args)
        {
            UnSubscribe();
            StartCoroutine(ShowFaderAndRestart(_defeatWindow));
        }

        private IEnumerator ShowFaderAndRestart(UIRestartWindow restartDialog)
        {
            yield return restartDialog.ShowAsync();
            yield return _loadingFader.ShowAsync();
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.path);
        }

        private void Start()
        {
            StartCoroutine(StartGameAsync());
        }

        private IEnumerator StartGameAsync()
        {
            yield return new WaitForSeconds(_startTimout);
            _enemiesCount = _gameConfig.GetRandomEnemiesCount(); 
            _eventBus.Send(new StartSpawnEnemies(_enemiesCount, _gameConfig.MinSpawnTimeout, _gameConfig.MaxSpawnTimeout));
            yield return _loadingFader.HideAsync();
            
        }
    }
}