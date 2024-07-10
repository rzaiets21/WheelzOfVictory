using System;
using Newtonsoft.Json;
using UnityEngine;

namespace MyOwn
{
    public sealed class DailyBonusController : Singleton<DailyBonusController>
    {
        private static string DailyBonusDateKey = "DailyBonusDateKey";

        public bool DailyBonusIsReady
        {
            get
            {
                if (!PlayerPrefs.HasKey(DailyBonusDateKey))
                    return true;
                
                var currentDate = DateTime.Now;
                var lastBonusDate = GetLastBonusDate();
                
                return lastBonusDate.Day != currentDate.Day || lastBonusDate.Month != currentDate.Month;
            }
        }

        public void MarkDailyBonusIsReceived()
        {
            var dateRaw = JsonConvert.SerializeObject(DateTime.Now);
            PlayerPrefs.SetString(DailyBonusDateKey, dateRaw);
        }

        private DateTime GetLastBonusDate()
        {
            var dateRaw = PlayerPrefs.GetString(DailyBonusDateKey);
            var date = JsonConvert.DeserializeObject<DateTime>(dateRaw);
            return date;
        }
    }
}