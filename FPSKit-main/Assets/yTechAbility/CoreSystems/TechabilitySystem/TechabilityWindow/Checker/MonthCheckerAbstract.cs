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


namespace Techability.Systems
{
    public abstract class MonthCheckerAbstract : MonoBehaviour
    {

        public TechAbilityStepsScriptable steps;
        public string scriptableName = "Month1";

        public MonthCheckerAbstract()
        {
            EnsureSteps();
        }

        public void EnsureSteps()
        {
            steps = (TechAbilityStepsScriptable)Resources.Load(scriptableName);
        }

        

        public abstract void CheckWeek1();
        public abstract void CheckWeek2();
        public abstract void CheckWeek3();
        public abstract void CheckWeek4();

        #region Reflection & Testing Helper Methods
        //-------------------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------------------------------------//
        //                                         NEW FUNCTIONALITY TESTING ZONE                                                  //
        //-------------------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------------------------------------//



        public void Test(Step step, bool conditions)
        {
            if (conditions && !step.completed)
            {
                TechAbility.Instance.AddPoints(step);
                step.completed = true;
            }

            if (step.completed)
            {
                userMessages.Clear();
            }
            else
            {
                step.feedback = "Check the console for errors.";
            }

            LogUserMessage((step.completed ? step.completedText : step.incompletedText) + "   - ", (!conditions && !step.completed && userMessages.Count == 0 && userMessages.Count == 0));
        }

        public void Test(Step step, params bool[] conditions)
        {
            bool isCompletable = true;
            if (conditions.Length < 1)
            {
                isCompletable = false;
                userMessages.Add("No conditions specified.  Test passes by default.");
            }

            foreach (bool condition in conditions)
            {
                isCompletable = isCompletable && condition;
            }

            Test(step, isCompletable);
        }
        
        void LogUserMessage(string optionalText = "", bool expectingError = false)
        {
            string userMessage = "";

            if (userMessages.Count > 0)
            {
                userMessage = userMessages.First();
            }
            userMessages.Clear();

            bool passed = userMessage == "" ^ expectingError;
            if (userMessage == "" && expectingError)
            {
                userMessage = "An error was expected, but no error message was received.  Check to make sure all lines are coded properly.";
            }

            Debug.Log((optionalText == "" ? "" : optionalText + " ") + (passed ? " Check Passed    - " : " Check Failed    - ") + userMessage);
        }

        public bool NavMeshBaked(NavMeshAgent agent = null)
        {
            if (agent == null)
            {
                agent = FindObjectOfType<NavMeshAgent>();

                if (agent == null)
                {
                    userMessages.Add("No NavMeshAgent can be found.  Make sure there is one in your scene.");
                    return false;
                }
            }

            Vector3 nmAPosition = FindObjectOfType<NavMeshAgent>().transform.position;

            List<Collider> colliders = FindObjectsOfType<Collider>().Where(x => x.transform.position != nmAPosition).ToList();

            foreach (Collider col in colliders)
            {
                if (col.transform.position != nmAPosition && NavMesh.CalculatePath(nmAPosition, col.transform.position, NavMesh.AllAreas, new NavMeshPath()))
                {
                    return true;
                }
            }

            userMessages.Add("No path can be found between NavMeshAgent '" + agent.name + "' and any objects with Colliders.  Make sure your NavMesh is baked and there are GameObjects with Colliders that can be pathed to on the NavMesh.");
            return false;
        }

        public bool ClassExists(string className, bool ignoreCase = false)
        {
            Type retCheck = Type.GetType(className, false, ignoreCase);

            return retCheck != null;
        }

        public GameObject FindGameObjectWithClass(string className, bool ignoreCase = false)
        {
            List<GameObject> withClasses = FindGameObjectsWithClass(className, ignoreCase);

            if (withClasses.Count > 0)
            {
                return withClasses.First();
            }

            return null;
        }

        public List<GameObject> FindGameObjectsWithClass(string className, bool ignoreCase = false)
        {
            List<GameObject> retVal = new List<GameObject>();
            if (ClassExists(className, ignoreCase))
            {
                foreach (GameObject go in FindObjectsOfType<GameObject>())
                {
                    if (go.GetComponent(Type.GetType(className, false, ignoreCase)) != null)
                    {
                        retVal.Add(go);
                    }
                }
            }
            return retVal;
        }


        public UnityEngine.Object GetAsset(string path, Type type)
        {
            return AssetDatabase.LoadAssetAtPath(path, type);
        }

        public Component getClassOnPrefab(string className, bool ignoreCaseClassName = false)
        {
            List<Component> classes = getClassesOnPrefabs(className, ignoreCaseClassName);
            if (classes.Count > 0)
            {
                return classes.First();
            }
            return null;
        }

        public List<Component> getClassesOnPrefabs(string className, bool ignoreCaseClassName = false)
        {
            List<Component> retVal = new List<Component>();

            foreach (GameObject go in GetAssetsWithClass(className, ignoreCaseClassName))
            {
                retVal.Add(go.GetComponent(Type.GetType(className, false, ignoreCaseClassName)));
            }

            return retVal;
        }

        public GameObject GetAssetWithClass(string className, bool ignoreCaseClassName = false)
        {
            List<GameObject> assets = GetAssetsWithClass(className, ignoreCaseClassName);
            if (assets.Count > 0)
            {
                return assets.First();
            }
            return null;
        }

        public List<GameObject> GetAssetsWithClass(string className, bool ignoreCaseClassName = false)
        {
            List<GameObject> retVal = new List<GameObject>();

            foreach (string path in AssetDatabase.GetAllAssetPaths())
            {
                UnityEngine.Object obj = GetAsset(path, typeof(GameObject));
                if (obj != null && ClassExists(className, ignoreCaseClassName))
                {
                    if (((GameObject)obj).GetComponent(Type.GetType(className, false, ignoreCaseClassName)) != null)

                        retVal.Add((GameObject)obj);
                }
            }

            if (retVal.Count == 0)
            {
                //userMessages.Add("No prefab Game object was found in the project with ");
            }

            return retVal;
        }

        public bool MethodExists(string methodName, string className, bool ignoreCaseMethodName = true, bool ignoreCaseClassName = false)
        {
            return GetMethod(methodName, className, ignoreCaseMethodName, ignoreCaseClassName) != null;
        }

        public MethodInfo GetMethod(string methodName, string className, bool ignoreMethodName = true, bool ignoreCaseClassName = false)
        {
            MethodInfo retVal = null;

            Type myType = Type.GetType(className, false, ignoreCaseClassName);
            if (myType != null)
            {
                retVal = myType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | (ignoreMethodName ? BindingFlags.IgnoreCase : BindingFlags.Public));

                if (retVal == null)
                {
                    if (!ignoreMethodName && MethodExists(methodName, className))
                    {
                        userMessages.Add("Method '" + methodName + "' exists, but does not use the expected case.  Make sure each letter is correctly capitalized or lower case.");
                    }
                    else
                    {
                        userMessages.Add("Method '" + methodName + "' does not exist.");
                    }
                }
            }
            else
            {
                if (Type.GetType(className, false, true) != null)
                {
                    userMessages.Add("Class '" + className + "' exists, but does not use the expected case.  Make sure each letter is correctly capitalized or lower case.");
                }
                else
                {
                    userMessages.Add("Class '" + className + "' does not exist.");
                }
            }

            return retVal;
        }

        public bool CheckMethodReturn(string methodName, string className, Type returnType = null, params Type[] paramTypes)
        {
            bool retVal = MethodExists(methodName, className);
            if (retVal)
            {
                MethodInfo mInfo = GetMethod(methodName, className);
                retVal = mInfo.ReturnType == returnType;

                ParameterInfo[] pInfos = mInfo.GetParameters();

                if (pInfos.Length != paramTypes.Length)
                {
                    userMessages.Add("Number of parameters does not match.  Expected: " + paramTypes.Length + "  Actual: " + pInfos.Length);
                }
                else
                {

                    bool paramMatch;
                    for (int i = 0; i < paramTypes.Length; i++)
                    {
                        paramMatch = pInfos[i].ParameterType == paramTypes[i];
                        if (!paramMatch)
                        {
                            userMessages.Add("Parameter '" + pInfos[i].Name + "' does not match the expected Type.  Expected: " + paramTypes[i] + "  Actual: " + pInfos[i].ParameterType);
                        }

                        retVal = retVal && paramMatch;
                    }
                }
            }

            return retVal;
        }

        public bool FieldExists(string fieldName, string className, bool ignoreCaseFieldName = true, bool ignoreCaseClassName = false)
        {
            FieldInfo retCheck = GetField(fieldName, className, ignoreCaseFieldName, ignoreCaseClassName);

            return retCheck != null;
        }

        public FieldInfo GetField(string fieldName, string className, bool ignoreCaseFieldName = true, bool ignoreCaseClassName = false)
        {
            FieldInfo retVal = null;

            Type myType = Type.GetType(className, false, ignoreCaseClassName);
            if (myType != null)
            {
                retVal = myType.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | (ignoreCaseFieldName ? BindingFlags.IgnoreCase : BindingFlags.Public));

                if (retVal == null)
                {
                    userMessages.Add("Field '" + fieldName + "' does not exist.");
                }
            }
            else
            {
                if (Type.GetType(className, false, true) != null)
                {
                    userMessages.Add("Class '" + className + "' exists, but does not use the expected case.  Make sure each letter is correctly capitalized or lower case.");
                }
                else
                {
                    userMessages.Add("Class '" + className + "' does not exist.");
                }
            }

            return retVal;
        }

        public string GetFieldName(string fieldName, string className, bool ignoreCaseFieldName = true, bool ignoreCaseClassName = false)
        {
            string retVal = "";
            FieldInfo field = GetField(fieldName, className, ignoreCaseFieldName, ignoreCaseClassName);
            if (field != null)
            {
                retVal = field.Name;
            }
            return retVal;
        }

        public object GetFieldValue(string fieldName, string className, UnityEngine.Object specificInstance, bool ignoreCaseFieldName = true, bool ignoreCaseClassName = false)
        { 
            if (specificInstance == null)
            {
                Type myType = Type.GetType(className, false, ignoreCaseClassName);

                if (myType != null && specificInstance == null)
                {
                    specificInstance = FindObjectOfType(myType);
                }
                else
                {
                    if (Type.GetType(className, false, true) != null)
                    {
                        userMessages.Add("Class '" + className + "' exists, but does not use the expected case.  Make sure each letter is correctly capitalized or lower case.");
                    }
                    else
                    {
                        userMessages.Add("Class '" + className + "' does not exist.");
                    }
                    return null;
                }
            }
           
            if (specificInstance == null)
            {
                userMessages.Add("An object of Type '" + className + "' cannot be found.  The class may not be attached to an object.");
                return null;
            }

            if (!ignoreCaseFieldName && FieldExists(fieldName, className, true, ignoreCaseClassName))
            {
                userMessages.Add("Field '" + fieldName + "' exists, but does not use the expected case.  Make sure each letter is correctly capitalized or lower case.");
                return null;
            }

            FieldInfo field = GetField(fieldName, className, ignoreCaseFieldName, ignoreCaseClassName);
            if (field == null )
            {
                Debug.Log("failed");
            }
            Debug.Log("f: " + field.ToString());
            Debug.Log("GFV");
            try
            {
                Debug.Log("GFVa");
                Debug.Log("f: " + field.ToString());
                if (field.Attributes == FieldAttributes.Private)
                {
                    userMessages.Add("Field '" + fieldName + "' is private and the value may not return the correct default value.");
                }
                Debug.Log("GFV2");
                return field.GetValue(specificInstance);
            }
            catch
            {
                if (FieldExists(fieldName, className, ignoreCaseFieldName, ignoreCaseClassName))
                {
                    userMessages.Clear();
                    userMessages.Add("Invalid Unit Test... Field '" + fieldName + "' cannot be found in '" + specificInstance + "'.     -    '" + specificInstance + "' object may not exist. Test specifies invalid object.");
                }
                Debug.Log("GFV3");
                return null;
            }
        }

        public object GetFieldValue(string fieldName, string className, bool ignoreCaseFieldName = true, bool ignoreCaseClassName = false)
        {
            return GetFieldValue(fieldName, className, null, ignoreCaseFieldName, ignoreCaseClassName);
        }

        public bool FieldHasNonDefaultValue(string fieldName, string className, Type expectedType, object defaultValue, UnityEngine.Object specificInstance, bool ignoreCaseFieldName = true, bool ignoreCaseClassName = false)
        {
            try
            {
                return GetField(fieldName, className).FieldType == expectedType && GetFieldValue(fieldName, className, specificInstance) != defaultValue;
            }
            catch
            {
                return false;
            }
        }

        public bool FieldHasNonDefaultValue(string fieldName, string className, Type expectedType, object defaultValue, bool ignoreCaseFieldName = true, bool ignoreCaseClassName = false)
        {
            return FieldHasNonDefaultValue(fieldName, className, expectedType, defaultValue, null, ignoreCaseFieldName, ignoreCaseClassName);
        }

        public bool TagExists(string tagName, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                tagName = tagName.ToLower();
            }

            foreach (string tag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                if ((ignoreCase ? tag.ToLower() : tag) == tagName)
                {
                    return true;
                }
            }

            userMessages.Add("Tag " + tagName + " does not exist.");
            return false;
        }

        public string GetTagCasing(string tagName)
        {
            foreach (string tag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                if (tag.ToLower() == tagName.ToLower())
                {
                    return tag;
                }
            }

            userMessages.Add("Tag " + tagName + " does not exist.");
            return tagName;
        }

        public bool HasTag(GameObject go, string tag)
        {
            if (!TagExists(tag, true) || go == null)
            {
                return false;
            }

            string actualTag = GetTagCasing(tag);
            if (actualTag == go.tag)
            {
                return true;
            }

            userMessages.Add("Tag on " + go.name + " is set as '" + go.tag + "', instead of the expected tag, '" + actualTag + "'.");
            return false;
        }

        #endregion

        //-------------------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------------------------------------//
        //                                          Methods To Check Steps :                                                       //
        //-------------------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------------------------------------//


        //Useful tools

        /*
         *  Gettings script as a text file
         * 
         *  TextAsset txt = Resources.Load<TextAsset>("Scripts/TestScript");
         *   if (txt.text.Contains("void Awake()"))
         *   {
         *       database.GetStep(1, "CodeAwake").completed = true;
         *       Debug.Log("this class contains CodeAwake");
         *   }
         * 
         * 
         */

        /// Checking if a given script exists in the project.
        /// 

        //          try
        //        {
        //            var o = (from assembly in System.AppDomain.CurrentDomain.GetAssemblies()
        //                     from type in assembly.GetTypes()
        //                     where type.Name == "HomingMissile"
        //                     select type).FirstOrDefault();

        //            if (o != null)
        //            {
        //                steps.codeHoming = true;
        //            }
        //        catch
        //          {
        //          }

        List<string> userMessages = new List<string>();

        public string GetLine(string lookFor, string script)
        {
            string[] lines = script.Split('\n');
            int a = 0;
            foreach (string line in lines)
            {
                if (line.Contains(lookFor))
                {
                    string l = line.Trim();
                    l = String.Concat(l.Where(c => !Char.IsWhiteSpace(c)));
                    return l;
                }
                a++;
            }
            return "404";
        }

        /// <summary>
        /// Checks if a script contains a partical line of code.
        /// </summary>
        /// <param name="lookFor">Line to look for</param>
        /// <param name="script">Script to look through</param>
        /// <returns></returns>
        public bool ContainsLinePartial(string lookFor, string script)
        {

            string[] lines = script.Split('\n');

            lookFor = String.Concat(lookFor.Where(c => !Char.IsWhiteSpace(c)));
            int a = 0;
            foreach (string line in lines)
            {
                string l = line.Trim();
                l = String.Concat(l.Where(c => !Char.IsWhiteSpace(c)));
                if (l.Contains(lookFor))
                    return true;
                a++;
            }
            return false;
        }

        /// <summary>
        /// Checks if a given script has a line of code
        /// </summary>
        /// <param name="lookFor">Line to look for</param>
        /// <param name="script">Script to look through</param>
        /// <returns></returns>
        public bool ContainsLine(string lookFor, string script)
        {

            string[] lines = script.Split('\n');

            lookFor = String.Concat(lookFor.Where(c => !Char.IsWhiteSpace(c)));
            int a = 0;
            foreach (string line in lines)
            {
                string l = line.Trim();
                l = String.Concat(l.Where(c => !Char.IsWhiteSpace(c)));
                if (l.Equals(lookFor))
                    return true;
                a++;
            }
            return false;
        }

        /// <summary>
        /// Checks if a given script has a line of code
        /// </summary>
        /// <param name="lookFor">Line to look for</param>
        /// <param name="script">Script to look through</param>
        /// <returns></returns>
        public bool ContainsLineToLower(string lookFor, string script)
        {

            string[] lines = script.Split('\n');

            lookFor = String.Concat(lookFor.Where(c => !Char.IsWhiteSpace(c)));
            int a = 0;
            foreach (string line in lines)
            {
                string l = line.Trim();
                l = String.Concat(l.Where(c => !Char.IsWhiteSpace(c)));
                if (l.ToLower().Equals(lookFor.ToLower()))
                    return true;
                a++;
            }
            return false;
        }

        public int GetIntFromLine(string line)
        {
            string number = "";

            bool startCounting = false;

            foreach (Char c in line)
            {
                if (Char.IsNumber(c))
                {
                    startCounting = true;
                    number += c;
                }
                else if (startCounting == true)
                    break;
            }
            return int.Parse(number);
        }
        public float GetFloatFromLine(string line)
        {

            string number = "";

            bool startCounting = false;

            foreach (Char c in line)
            {
                if (Char.IsNumber(c) || (c.Equals('.') && (startCounting == true)))
                {
                    startCounting = true;
                    number += c;
                }
                else if (startCounting == true)
                    break;
            }
            return float.Parse(number);
        }

        public bool ContainsParam(UnityEditor.Animations.AnimatorController anim, string name)
        {
            foreach (AnimatorControllerParameter param in anim.parameters)
            {
                if (param.name == name) return true;
            }
            return false;
        }

        public string[] ScriptToArray(string s)
        {
            string[] returnArray = s.Split('\n');
            for (int i = 0; i < returnArray.Length; i++)
            {
                returnArray[i] = returnArray[i].Trim();
            }
            return returnArray;
        }


        /// <summary>
        /// Get a range of line numbers to know if given code is in that method
        /// Usage Note: if expecting a return value of false [ if (!GetRange(...)) ] - make sure generateMessages is false as to not generate erroneous messages
        /// </summary>
        /// <param name="s">Method name IE "void Update"</param>
        /// <returns></returns>
        ///
        //Set MetMethod t oobsolete in future versions
        //[Obsolete("GetMethod is deprecated. Use GetRange instead.", false)]
        public Vector2Int MethodRange(string s, string[] arraySplit, bool ignoreWhite = true, bool generateMessages = true)
        {
            return GetRange(s, arraySplit, Vector2Int.zero, ignoreWhite = true, generateMessages = true);
        }

        public Vector2Int GetRange(string s, string[] arraySplit, bool ignoreWhite = true, bool generateMessages = true)
        {
            return GetRange(s, arraySplit, Vector2Int.zero, ignoreWhite = true, generateMessages = true);
        }


        public Vector2Int GetRange(string s, string[] arraySplit, Vector2Int withinRange, bool ignoreWhite = true, bool generateMessages = true)
        {
            Vector2Int range = Vector2Int.zero;

            bool checkBrackets = false;
            int openBrackets = 0;

            string original = s;

            if (ignoreWhite)
            {
                s = s.Trim();
                s = String.Concat(s.Where(c => !Char.IsWhiteSpace(c)));
            }

            if (withinRange == Vector2.zero)
            {
                withinRange.y = arraySplit.Length;
            }

            for (int i = withinRange.x; i < withinRange.y; i++)
            {
                string newLine = arraySplit[i];
                if (ignoreWhite)
                {
                    newLine = newLine.Trim();
                    newLine = String.Concat(newLine.Where(c => !Char.IsWhiteSpace(c)));
                }
                if (newLine.Contains(s))
                {
                    checkBrackets = true;
                }
                if (newLine.Contains('{') && checkBrackets == true)
                {
                    openBrackets++;
                    if (range.x == 0)
                    {
                        range.x = i;
                    }
                }

                if (newLine.Contains('}') && checkBrackets == true)
                {
                    openBrackets--;
                    if (openBrackets == 0)
                    {
                        range.y = i;
                        return range;
                    }
                }
            }
            if (generateMessages)
            {
                userMessages.Add("The statement '" + original + "' cannot be found. It may not be in in the code or it is in the wrong location.");
            }
            return range;
        }



        public bool ValidScript(string[] arraySplit, string scriptName = "")
        {
            int openBrackets = 0;

            for (int i = 0; i < arraySplit.Length; i++)
            {
                if (scriptName != "")
                {
                    if (arraySplit[i].Contains("class"))
                    {
                        if (arraySplit[i].Contains(scriptName))
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
                if (arraySplit[i].Contains('{'))
                {
                    openBrackets++;
                }

                if (arraySplit[i].Contains('}'))
                {
                    openBrackets--;
                }
            }
            if (openBrackets == 0)
                return true;
            else
                return false;

        }

        /// <summary>
        /// Contains a line in a given range
        /// </summary>
        /// <param name="lookFor"></param>
        /// <param name="list"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool ContainsLineInRange(string lookFor, string[] list, Vector2Int range, bool ignoreWhite)
        {
            if (ignoreWhite)
            {
                lookFor = lookFor.Trim();
                lookFor = String.Concat(lookFor.Where(c => !Char.IsWhiteSpace(c)));

            }
            for (int i = range.x; i < range.y; i++)
            {
                string newLine = list[i];
                if (ignoreWhite)
                {
                    newLine = newLine.Trim();
                    newLine = String.Concat(newLine.Where(c => !Char.IsWhiteSpace(c)));
                }
                if (newLine.Contains(lookFor))
                {
                    return true;
                }
            }
            return false;
        }

        // Usage Note: if expecting a return value of false [ if (!LineInRange(...)) ] - make sure generateMessages is false as to not generate erroneous messages
        public bool LineInRange(string lookFor, string[] list, Vector2Int range, bool ignoreWhite = true, bool generateMessages = true)
        {
            if (range == Vector2Int.zero)
            {
                return false;
            }

            string original = lookFor;

            if (ignoreWhite)
            {
                lookFor = lookFor.Trim();
                lookFor = String.Concat(lookFor.Where(c => !Char.IsWhiteSpace(c)));

            }
            for (int i = range.x; i < range.y; i++)
            {
                string newLine = list[i];
                if (ignoreWhite)
                {
                    newLine = newLine.Trim();
                    newLine = String.Concat(newLine.Where(c => !Char.IsWhiteSpace(c)));
                }
                if (newLine.Equals(lookFor))
                {
                    return true;
                }
            }
            if (generateMessages)
            {
                userMessages.Add("'" + original + "' cannot be found.  It may be typed incorrectly or in the wrong location.");
            }
            return false;
        }
    }
}