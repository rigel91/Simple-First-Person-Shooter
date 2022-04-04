using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Techability.Systems;
using Techability;
using UnityEditor;
using System.Reflection;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;

namespace Techability.Systems
{
    public static class MonthChecker
    {

        #region Base Steps
        public static Month1Check month1 = new Month1Check();
        public static Month2Check month2 = new Month2Check();
        public static Month3Check month3 = new Month3Check();
        public static Month4Check month4 = new Month4Check();

        public static TechAbilityStepsScriptable GetSteps()
        {
            Vector3Int date = GetDateTime();
            if (date.x == 1)
                if (month1.steps != null)
                    return month1.steps;
            if (date.x == 2)
                if (month2.steps != null)
                    return month2.steps;
            if (date.x == 3)
                if (month3.steps != null)
                    return month3.steps;
            if (date.x == 4)
                if (month4.steps != null)
                    return month4.steps;

            Debug.LogWarning("Unable to grab steps from abstract class.");

            if (date.x == 1)
                return (TechAbilityStepsScriptable)Resources.Load("Month1");
            if (date.x == 2)
                return (TechAbilityStepsScriptable)Resources.Load("Month2");
            if (date.x == 3)
                return (TechAbilityStepsScriptable)Resources.Load("Month3");
            
            return (TechAbilityStepsScriptable)Resources.Load("Month4");
        }

        #endregion

        public static Month1ExtraCheck month1Extra = new Month1ExtraCheck();
        public static Month2ExtraCheck month2Extra = new Month2ExtraCheck();
        public static Month3ExtraCheck month3Extra = new Month3ExtraCheck();
        public static Month4ExtraCheck month4Extra = new Month4ExtraCheck();

        #region Extra Steps

        public static TechAbilityExtraSteps GetExtraSteps()
        {
            Vector3Int date = GetDateTime();
            if (date.x == 1)
                if (month1.steps != null)
                    return month1Extra.steps;
            if (date.x == 2)
                if (month2.steps != null)
                    return month2Extra.steps;
            if (date.x == 3)
                if (month3.steps != null)
                    return month3Extra.steps;
            if (date.x == 4)

                if (month4.steps != null)
                    return month4Extra.steps;

            Debug.LogWarning("Unable to grab steps from abstract class.");

            if (date.x == 1)
                return (TechAbilityExtraSteps)Resources.Load("Month1Extra");
            if (date.x == 2)
                return (TechAbilityExtraSteps)Resources.Load("Month2Extra");
            if (date.x == 3)
                return (TechAbilityExtraSteps)Resources.Load("Month3Extra");

            return (TechAbilityExtraSteps)Resources.Load("Month4Extra");
        }

        #endregion


        #region Other Methods
        public static void AddPoints(Step s, object owner, string codeAddition = "")
        {
            EditorCoroutineUtility.StartCoroutine(_AddPoints(s, codeAddition), owner);
        }

        private static IEnumerator _AddPoints(Step s, string codeAddition)
        {
            int index = 0;
            foreach (Points p in s.points)
            {
                int cat1 = p.GetSubCategory();
                int points1 = p.subCategoryPoints;
                int coins = s.coins;
                string disc = s.description;

                string activityCode = TechAbility.PROJECT_NAME + "_" + s.name + codeAddition;
                if (activityCode.Length > 16)
                {
                    activityCode = activityCode.Substring(0, 16);
                    activityCode += index.ToString();
                }


                string l = $"https://techability.education/webservice/log_Activity.php?" +
                    $"Student_Id={month1.steps.studentID.Trim()}&" +
                    $"Unity_Activity_Code={activityCode}" +
                    $"&sub_category_ID1={cat1}&" +
                    $"sub_category_ID2={0}&" +
                    $"sub_category_ID3={0}&" +
                    $"points1={points1}&" +
                    $"points2={0}&" +
                    $"points3={0}&" +
                    $"Coins={coins}&" +
                    $"Description={disc}";

                //   Debug.Log($"SubCat1={cat1}, SubCat2={0},SubCat3={0},Points1={points1},Points2=0,Points3=0,coints={coins},desc={disc}");

                // Debug.Log(l);

                UnityWebRequest web = UnityWebRequest.Head(l);

                yield return web.SendWebRequest();
                index++;
            }

        }

        public static Vector3Int GetDateTime()
        {
            Vector3Int t = new Vector3Int();
            DateTime dt;
            //Set if key does not exist
            if(!PlayerPrefs.HasKey("DateO"))
            {
                PlayerPrefs.SetInt("DateO", 0);
            }

            if (PlayerPrefs.GetInt("DateO") == 2)
            {
                try
                {
                    dt = new DateTime(PlayerPrefs.GetInt("year"), PlayerPrefs.GetInt("month"), PlayerPrefs.GetInt("day"));
                }catch
                {
                    dt = DateTime.Now;
                    Debug.LogError("Error, no set date stored. Using default");
                }
            }
            else if (PlayerPrefs.GetInt("DateO") == 1)
            {
                dt = DateTime.Now;
                dt = dt.AddDays(PlayerPrefs.GetInt("DayOffset"));
            }
            else
            {
                dt = DateTime.Now;
            }

            #region GetMonth
            if (dt.Month == 2 || dt.Month == 6 || dt.Month == 10)
                t.x = 4;
            if (dt.Month == 3 || dt.Month == 7 || dt.Month == 11)
                t.x = 1;
            if (dt.Month == 4 || dt.Month == 8 || dt.Month == 12)
                t.x = 2;
            if (dt.Month == 5 || dt.Month == 9 || dt.Month == 1)
                t.x = 3;
            #endregion

            t.y = GetWeekNumberOfMonth(dt);

            t.z = (int)dt.DayOfWeek;

            if (dt.Day < 12)
            {
                if (t.y == 5)
                {
                    t.x -= 1;
                    if (t.x == 0)
                        t.x = 4;
                }
            }

            return t;
        }


        static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }
        #endregion

    }
}