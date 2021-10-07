using System;
using System.Collections.Generic;
using MarbleRaceBase.Utility;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class RealTimeData : MonoBehaviour
    {
        // Team information
        public Dictionary<int, Team> teams = new Dictionary<int, Team>();
        public int[] teamCount;
        private int _teamNum = 0;

        public void UpdateCount(int oldTeam, int newTeam)
        {
            if (teams.ContainsKey(oldTeam))
            {
                teams[oldTeam].tileCount--;
            }

            teams[newTeam].tileCount++;
        }

        public void UpdateCount(int newTeam)
        {
            teams[newTeam].tileCount++;
        }

        public void Setup()
        {
            var gm = MonoUtils.GetGM();
            // foreach (var index in gm.mapData.teams)
            // {
            //     teams.Add(index, gm.mapData.teamsBase.teams[index]);
            //     _teamNum++;
            // }

            // teamCount = new int[_teamNum];
        }

        private void Update()
        {
            // int i = 0;
            // foreach (var t in teams)
            // {
            //     teamCount[i] = t.Value.tileCount;
            //     i++;
            // }
        }
    }
}