﻿using ExcelLibrary.SpreadSheet;

namespace QRCodeXLS.Model
{
    public class QRString
    {
        private string nameAgr;
        private string serialNumber;
        private string url;

        public string NameAgr { get => nameAgr; set => nameAgr = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string URL { get => url; set => url = value; }

        public QRString(Row agregateRow) 
        {
            NameAgr = agregateRow.GetCell(0).Value?.ToString() ?? string.Empty;
            SerialNumber = agregateRow.GetCell(1).Value?.ToString() ?? string.Empty;
            URL = agregateRow.GetCell(2).Value?.ToString() ?? string.Empty;
        }
    }
}
