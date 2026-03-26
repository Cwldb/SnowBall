using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Work.GMS.Code.Data;
using Work.KJY.Code.Core;
using Work.KJY.Code.Event;
using Work.Utils.EventBus;

namespace Work.KJY.Code.Manager
{
    public class PlazaManager : MonoSingleton<PlazaManager>
    {
        [SerializedDictionary("level", "Need Money")]
        public SerializedDictionary<int, int> levelDict = new();
        
        private int _curLevel = 1;

        private void Start()
        {
            _curLevel = DataContainer.Instance.PlazaLevel;

            foreach (GameObject obj in LevelObjectDataManager.Instance.level1Objs) { obj.SetActive(false); }
            foreach (GameObject obj in LevelObjectDataManager.Instance.level2Objs) { obj.SetActive(false); }
            foreach (GameObject obj in LevelObjectDataManager.Instance.level3Objs) { obj.SetActive(false); }

            if (_curLevel >= 2)
            {
                foreach (GameObject obj in LevelObjectDataManager.Instance.level1Objs) { obj.SetActive(true); }
            }
            if (_curLevel >= 3)
            {
                foreach (GameObject obj in LevelObjectDataManager.Instance.level2Objs) { obj.SetActive(true); }
            }
            if (_curLevel >= 4)
            {
                foreach (GameObject obj in LevelObjectDataManager.Instance.level3Objs) { obj.SetActive(true); }
            }
        }

        public int GetCurLevel() => _curLevel;
        public int GetNeedMoney() => levelDict.ContainsKey(_curLevel) ? levelDict[_curLevel] : -1;
        public bool IsMaxLevel => _curLevel >= levelDict.Count;

        public int LevelUp()
        {
            if (IsMaxLevel)
            {
                return -1;
            }
            
            _curLevel++;
            DataContainer.Instance.SetPlazaLevel(_curLevel);

            if (_curLevel == 2)
            {
                foreach (GameObject obj in LevelObjectDataManager.Instance.level1Objs)
                {
                    obj.SetActive(true);
                }
            }

            if (_curLevel == 3)
            {
                foreach (GameObject obj in LevelObjectDataManager.Instance.level2Objs)
                {
                    obj.SetActive(true);
                }
            }

            if (_curLevel == 4)
            {
                foreach (GameObject obj in LevelObjectDataManager.Instance.level3Objs)
                {
                    obj.SetActive(true);
                }
            }
            
            Bus<PlazaLevelUpgradedEvent>.Raise(new PlazaLevelUpgradedEvent(_curLevel));
            return _curLevel;
        }
    }
}