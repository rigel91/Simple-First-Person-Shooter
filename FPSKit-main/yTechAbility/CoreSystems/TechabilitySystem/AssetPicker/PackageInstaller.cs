using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using Techability.Systems;
using System;
using System.IO;

public static class PackageInstaller
{
    static AddRequest Request;

    //public static void InstallTest()
    //{
    //    InstallCharacter("Female1.unitypackage");
    //}
    public static void InstallCharacter(string name)
    {
        AssetDatabase.ImportPackage($"Assets\\zSysFiles\\Packs\\C\\P\\{name}.unitypackage", true);
        AssetDatabase.importPackageCompleted += CharacterProgress;
    }

    static void CharacterProgress(string packageName)
    {
        try
        {
            AssetPicker.Refresh();
        }
        catch
        {

        }
        AssetDatabase.importPackageCompleted -= CharacterProgress;
    }

    public static void InstallEnvironment(string v)
    {
        AssetDatabase.ImportPackage($"Assets\\zSysFiles\\Packs\\E\\P\\{v}.unitypackage", true);
        AssetDatabase.importPackageCompleted += EnvironmentProgress;
    }
    static void EnvironmentProgress(string packageName)
    {
        CreateEmptyFile();
        try
        {
            AssetPicker.Refresh();
        }
        catch
        {

        }
        AssetDatabase.importPackageCompleted -= EnvironmentProgress;
    }


    public static void InstallEnemy(string v)
    {
        Debug.Log($"Assets\\zSysFiles\\Packs\\EE\\P\\{v}.unitypackage");
        AssetDatabase.ImportPackage($"Assets\\zSysFiles\\Packs\\EE\\P\\{v}.unitypackage", true);
        AssetDatabase.importPackageCompleted += EnemyProgress;
    }
    static void EnemyProgress(string packageName)
    {
        CreateEnemyFile();
        try
        {
            AssetPicker.Refresh();
        }
        catch
        {

        }
        AssetDatabase.importPackageCompleted -= EnemyProgress;
    }

    public static void CreateEnemyFile()
    {
        string p = Application.dataPath;
        string name = "Enemy" + MonthChecker.GetDateTime().x+"x"+MonthChecker.GetDateTime().y;

        p += $"\\yTechAbility\\CoreSystems\\TechabilitySystem\\AssetPicker\\Loaded\\{name}.txt";
        using (File.Create(p)) { }
        File.Create(p).Dispose();
        Debug.Log("File created at: " + p);
    }

    public static void CreateEmptyFile()
    {
        string p = Application.dataPath;
        string name = "Environment" + MonthChecker.GetDateTime().x;

        p += $"\\yTechAbility\\CoreSystems\\TechabilitySystem\\AssetPicker\\Loaded\\{name}.txt";
        using (File.Create(p)) { }
        File.Create(p).Dispose();
        Debug.Log("File created at: " + p);
    }
}
