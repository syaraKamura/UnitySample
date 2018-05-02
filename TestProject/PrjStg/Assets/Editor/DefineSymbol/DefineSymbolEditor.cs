using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DefineSymbolEditor : EditorWindow
{

    /// <summary>
    /// 分割する文字列
    /// </summary>
    private const char SPLIT_STRING = ';';

    private const string DEFINE_SYMBOL_ASSET_PATH = "Assets/DefineSymbols.asset";

    private string[] mSymbols;

    
    private List<DefineSymbol.Symbol> mDefineLit;

    private Vector2 mScroll = new Vector2(300,400);

    
    private DefineSymbol mSymbolAsset = null;


    private void Initialize()
    {
        string symbole = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        mSymbols = symbole.Split(SPLIT_STRING);

        mDefineLit = new List<DefineSymbol.Symbol>();

        if (mSymbolAsset == null)
        {
            mSymbolAsset = AssetDatabase.LoadAssetAtPath<DefineSymbol>(DEFINE_SYMBOL_ASSET_PATH);
        }

        for (int i = 0; i < mSymbols.Length; i++)
        {
            DefineSymbol.Symbol symbol = new DefineSymbol.Symbol(mSymbols[i], "", true);
            mDefineLit.Add(symbol);
        }

        if (mSymbolAsset != null)
        {

            var symbols = mSymbolAsset.mSymbols;

            for (int i = 0; i < symbols.Length; i++)
            {
                var symbol = symbols[i];

                for (int j = 0; j < mDefineLit.Count; j++)
                {
                    if (symbol.mSymbolName.Equals(mDefineLit[i].mSymbolName))
                    {
                        mDefineLit[j] = symbol;
                    }
                    else
                    {
                        mDefineLit.Add(symbol);
                    }
                }
            }

        }

    }


    private void OnEnable()
    {
        Initialize();
    }

    private void SaveDefineSymbol()
    {
        DefineSymbol assets = mSymbolAsset;

        string symbolsData = string.Empty;

        foreach (var symbol in mDefineLit)
        {

            if (symbol.mIsEnable)
            {
                symbolsData += symbol.mSymbolName + SPLIT_STRING;
            }

        }
        
        //すでにアセットが作成されていたら上書きする
        if (System.IO.File.Exists(DEFINE_SYMBOL_ASSET_PATH))
        {
            EditorUtility.SetDirty(mSymbolAsset);
        }
        else
        {
            assets = CreateInstance<DefineSymbol>();
            assets.mSymbols = mDefineLit.ToArray();
            AssetDatabase.CreateAsset(assets, DEFINE_SYMBOL_ASSET_PATH);
            AssetDatabase.Refresh();
        }

        AssetDatabase.ImportAsset(DEFINE_SYMBOL_ASSET_PATH);

        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbolsData);

        AssetDatabase.SaveAssets();

    }


    private void OnGUI()
    {

        if (Application.isPlaying || EditorApplication.isPlaying || EditorApplication.isCompiling)
        {
            EditorGUILayout.HelpBox("コンパイル中、実行中は変更できません", MessageType.Warning);
            return;
        }

        EditorGUILayout.BeginScrollView(mScroll, GUI.skin.box);
        {
            GUILayout.BeginVertical(GUI.skin.box);
            {
                for (int i = 0; i < mDefineLit.Count; i++)
                {
                    var define = mDefineLit[i];

                    define.mIsEnable = EditorGUILayout.Toggle(define.mIsEnable);

                    GUILayout.BeginVertical(GUI.skin.box);
                    {
                        define.mSymbolName = GUILayout.TextField(define.mSymbolName);
                        define.mInfomation = EditorGUILayout.TextField("説明", define.mInfomation);
                    }
                    GUILayout.EndVertical();
                    if (GUILayout.Button("Remove"))
                    {
                        mDefineLit.RemoveAt(i);
                        break;
                    }
                }
            }
            GUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();


        if (GUILayout.Button("Add"))
        {
            DefineSymbol.Symbol add = new DefineSymbol.Symbol("Symbol_Name", "", false);
            mDefineLit.Add(add);

        }
        if (GUILayout.Button("Save"))
        {
            SaveDefineSymbol();
        }

        if (mSymbolAsset != null)
        {
            mSymbolAsset.mSymbols = mDefineLit.ToArray();
        }

    }

    [MenuItem("Tools/Global Define")]
    static void OpenWindow()
    {
        GetWindow<DefineSymbolEditor>();        
    }
}