using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Drawing;

namespace DBManagerModel.Model
{
    class ReadFileExcel : IReadFile
    {
        /// <summary>
        /// 読込最大行数
        /// </summary>
        const int MAX_READ_COUNT = 500;

        readonly Point COLUMN_ROW_START_POS = new Point() { X = 0, Y = 1 };


        /// <summary>
        /// 読込処理
        /// </summary>
        /// <param name="path">読込ファイルパス</param>
        /// <returns></returns>
        public IEnumerable<Table> Read(string path)
        {
            var tableInfos = new List<Table>();

            //ファイル読込
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                var tableSheets = workBook.Worksheets.Where(x => IsTableSheet(x));

                foreach (var sheet in tableSheets)
                {

                    //テーブル情報取得
                    Table tableInfo = CreateTableInfo(sheet);

                    //キー取得
                    IEnumerable<Column> columnInfos = CreateColumns(sheet);
                    tableInfo.Columns = columnInfos;

                    //カラム取得
                    IEnumerable<Key> keyInfos = CreateKeys(sheet);
                    tableInfo.Keys = keyInfos;


                    tableInfos.Add(tableInfo);
                }

                return tableInfos;
            }


        }

        /// <summary>
        /// テーブルシートかを判定
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private bool IsTableSheet(IXLWorksheet sheet)
        {
            const string JUDGMENT_CELL_POSISION = "Z1";
            const string JUDGMENT_VALUE = "TARGET";

            string JudgmentCellValue = sheet.Cell(JUDGMENT_CELL_POSISION).GetString();

            return JudgmentCellValue == JUDGMENT_VALUE;
        }



        /// <summary>
        /// テーブル情報作成
        /// </summary>
        /// <param name="workBook"></param>
        /// <returns></returns>
        private Table CreateTableInfo(IXLWorksheet sheet)
        {
            var tableInfo = new Table();

            Point TABLE_NAME_POS = new Point(1, 1);
            Point TABLE_COMMENT_POS = new Point(1, 2);

            tableInfo.Name    = sheet.Cell(TABLE_NAME_POS.X   , TABLE_NAME_POS.Y   ).GetString()?.Trim();
            tableInfo.Comment = sheet.Cell(TABLE_COMMENT_POS.X, TABLE_COMMENT_POS.Y).GetString()?.Trim();

            return tableInfo;
        }

        enum Columns:int
        {
            No=0,
            Name,
            Type,
            Lemgth,
            DecimalLength,
            Nullable,
            PrimaryKeyNo,
            Comment,
        }

        /// <summary>
        /// カラム情報作成
        /// </summary>
        /// <param name="workBook"></param>
        /// <returns></returns>
        private IEnumerable<Column> CreateColumns(IXLWorksheet sheet)
        {
            var columnInfos = new List<Column>();


            for (int i = COLUMN_ROW_START_POS.Y; i < MAX_READ_COUNT; i++)
            {
                var row = sheet.Row(i);
                var cellNo = row.Cell((int)Columns.No);

                if (cellNo.Value == null)
                {
                    continue;
                }

                if(!int.TryParse(cellNo.GetString().Trim(),out int no))
                {
                    continue;
                }

                var columnInfo = new Column();
                columnInfo.No = no;
                columnInfo.Name = row.Cell((int)Columns.Name).GetString()??"";

                columnInfo.Type = row.Cell((int)Columns.Type).GetString()??"";

                columnInfo.Length = int.Parse(row.Cell((int)Columns.Lemgth).GetString());
                columnInfo.DecimalLength = int.TryParse(row.Cell((int)Columns.DecimalLength).GetString(), out int decimalLength) ? (int?)decimalLength : null;
                columnInfo.Nullable = row.Cell((int)Columns.Nullable).GetString() == "Y";
                columnInfo.PrimaryKeyNo = int.TryParse(row.Cell((int)Columns.PrimaryKeyNo).GetString(), out int primaryKeyNo) ? (int?)primaryKeyNo : null;
                columnInfo.Comment = row.Cell((int)Columns.Comment).GetString();



            }

            return columnInfos;
        }

        /// <summary>
        /// キー情報作成
        /// </summary>
        /// <param name="workBook"></param>
        /// <returns></returns>
        private IEnumerable<Key> CreateKeys(IXLWorksheet sheet)
        {
            var keyInfos = new List<Key>();

            //TODO

            return keyInfos;

        }




    }
}
