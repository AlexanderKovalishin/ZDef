using System;
using System.Collections.Generic;
using UnityEngine;
using ZDef.Core;

namespace ZDef.Game.Enemies
{
    public class EnemySpawnPoints: MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        
        private readonly List<Transform> _shuffleList = new List<Transform>();
        private readonly Queue<Transform> _pointsQueue = new Queue<Transform>();

        private void FillQueue()
        {
            _shuffleList.Clear();
            _shuffleList.AddRange(_points);
            _shuffleList.Shuffle();
            foreach (Transform point in _shuffleList)
            {
                _pointsQueue.Enqueue(point);
            }
        }

        public Transform DequeuePoint()
        {
            if (_pointsQueue.Count == 0)
                FillQueue();
            if (_pointsQueue.Count == 0)
                throw new ArgumentException("field _points is empty");
            return _pointsQueue.Dequeue();
        }
    }
}