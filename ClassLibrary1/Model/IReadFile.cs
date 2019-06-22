using System.Collections.Generic;

namespace DBManagerModel.Model
{
    /// <summary>
    /// ファイル読込インターフェース
    /// </summary>
    interface IReadFile
    {
        /// <summary>
        /// 読込
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>テーブル情報</returns>
        IEnumerable<Table> Read(string path);
    }
}
