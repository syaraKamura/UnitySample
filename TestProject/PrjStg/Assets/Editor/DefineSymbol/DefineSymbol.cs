using UnityEngine;
using UnityEditor;

[SerializeField]
public class DefineSymbol : ScriptableObject
{

    public class Symbol
    {
        /// <summary>
        /// Define名
        /// </summary>
        public string mSymbolName;

        /// <summary>
        /// Defineの説明
        /// </summary>
        public string mInfomation;

        /// <summary>
        /// シンボルが有効か
        /// </summary>
        public bool mIsEnable;

        public Symbol(string symbolName, string info,bool isEnable)
        {
            mSymbolName = symbolName;
            mInfomation = info;
            mIsEnable = isEnable;
        }

    }

    public Symbol[] mSymbols;

}