using System.Collections.Generic;

namespace DBManagerModel.Model
{
    /// <summary> テーブル </summary>
    class Table
    {
        /// <summary> 名称 </summary>
        public string Name { get; set; }
        /// <summary> コメント </summary>
        public string Comment { get; set; }
        /// <summary> カラム </summary>
        public IEnumerable<Column> Columns { get; set; }
        /// <summary> キー </summary>
        public IEnumerable<Key> Keys { get; set; }
    }

    /// <summary> カラム </summary>
    class Column
    {
        /// <summary> No </summary>
        public int No { get; set; }
        /// <summary> 名称 </summary>
        public string Name { get; set; }
        /// <summary> 型 </summary>
        public string Type { get; set; }
        /// <summary> 長さ </summary>
        public int Length { get; set; }
        /// <summary> 小数長さ </summary>
        public int? DecimalLength { get; set; }
        /// <summary> NULL許容 </summary>
        public bool Nullable { get; set; }
        /// <summary> デフォルト値 </summary>
        public string Default { get; set; }
        /// <summary> 主キー番号 </summary>
        public int? PrimaryKeyNo { get; set; }
        /// <summary> 外部キー </summary>
        public string ForeignKey { get; set; }
        /// <summary> コメント </summary>
        public string Comment { get; set; }
    }

    /// <summary> 制約キー </summary>
    class Key
    {
        /// <summary> 制約キー </summary>
        public string KeyValue { get; set; }
    }
}
