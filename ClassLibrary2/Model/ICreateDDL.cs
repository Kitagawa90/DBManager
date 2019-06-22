using System;

namespace DBManagerModel.Model
{
    /// <summary>
    /// DDL作成
    /// </summary>
    interface ICreateDDL
    {
        /// <summary>
        /// DDL作成
        /// </summary>
        /// <returns></returns>
        bool Create(String saveDirectory);

    }
}
