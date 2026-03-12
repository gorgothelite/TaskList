using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Test
{
    internal static class XlsxWriter
    {
        // ── Sheet definition ──────────────────────────────────────────────────
        internal class SheetSpec
        {
            public string           Name    { get; set; }
            public List<string>     Headers { get; set; }
            public List<List<string>> Rows  { get; set; }
        }

        // ── Public entry points ───────────────────────────────────────────────

        /// <summary>Single-sheet convenience overload.</summary>
        public static void Write(string path, string sheetName,
                                 List<string> headers, List<List<string>> rows)
        {
            Write(path, new List<SheetSpec>
            {
                new SheetSpec { Name = sheetName, Headers = headers, Rows = rows }
            });
        }

        /// <summary>Multi-sheet overload.</summary>
        public static void Write(string path, List<SheetSpec> sheets)
        {
            if (sheets == null || sheets.Count == 0)
                throw new ArgumentException("At least one sheet is required.", nameof(sheets));

            var enc = new UTF8Encoding(false);

            using (var fs  = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var zip = new ZipArchive(fs, ZipArchiveMode.Create, leaveOpen: false))
            {
                AddEntry(zip, "[Content_Types].xml",        ContentTypes(sheets.Count), enc);
                AddEntry(zip, "_rels/.rels",                Rels(),                     enc);
                AddEntry(zip, "xl/workbook.xml",            Workbook(sheets),           enc);
                AddEntry(zip, "xl/_rels/workbook.xml.rels", WorkbookRels(sheets.Count), enc);
                AddEntry(zip, "xl/styles.xml",              Styles(),                   enc);

                for (int i = 0; i < sheets.Count; i++)
                    AddEntry(zip,
                             $"xl/worksheets/sheet{i + 1}.xml",
                             Sheet(sheets[i].Headers, sheets[i].Rows),
                             enc);
            }
        }

        // ── ZIP helper ────────────────────────────────────────────────────────
        private static void AddEntry(ZipArchive zip, string name, string content, Encoding enc)
        {
            var entry = zip.CreateEntry(name, CompressionLevel.Optimal);
            using (var sw = new StreamWriter(entry.Open(), enc))
                sw.Write(content);
        }

        // ── Static XML parts ─────────────────────────────────────────────────
        private static string ContentTypes(int sheetCount)
        {
            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n");
            sb.Append("<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">");
            sb.Append("<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>");
            sb.Append("<Default Extension=\"xml\"  ContentType=\"application/xml\"/>");
            sb.Append("<Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/>");
            for (int i = 1; i <= sheetCount; i++)
                sb.AppendFormat(
                    "<Override PartName=\"/xl/worksheets/sheet{0}.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/>",
                    i);
            sb.Append("<Override PartName=\"/xl/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml\"/>");
            sb.Append("</Types>");
            return sb.ToString();
        }

        private static string Rels() =>
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n" +
            "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
              "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/>" +
            "</Relationships>";

        private static string Workbook(List<SheetSpec> sheets)
        {
            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n");
            sb.Append("<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" " +
                      "xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">");
            sb.Append("<sheets>");
            for (int i = 0; i < sheets.Count; i++)
                sb.AppendFormat("<sheet name=\"{0}\" sheetId=\"{1}\" r:id=\"rId{1}\"/>",
                                XmlEscape(sheets[i].Name), i + 1);
            sb.Append("</sheets></workbook>");
            return sb.ToString();
        }

        private static string WorkbookRels(int sheetCount)
        {
            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n");
            sb.Append("<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">");
            for (int i = 1; i <= sheetCount; i++)
                sb.AppendFormat(
                    "<Relationship Id=\"rId{0}\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet{0}.xml\"/>",
                    i);
            // Styles relationship — rId after all sheets
            sb.AppendFormat(
                "<Relationship Id=\"rId{0}\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/>",
                sheetCount + 1);
            sb.Append("</Relationships>");
            return sb.ToString();
        }

        private static string Styles() =>
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n" +
            "<styleSheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">" +

              // fonts: 0=normal, 1=bold
              "<fonts count=\"2\">" +
                "<font><sz val=\"11\"/><name val=\"Calibri\"/></font>" +
                "<font><b/><sz val=\"11\"/><name val=\"Calibri\"/></font>" +
              "</fonts>" +

              // fills: 0=none (required), 1=gray125 (required by spec)
              "<fills count=\"2\">" +
                "<fill><patternFill patternType=\"none\"/></fill>" +
                "<fill><patternFill patternType=\"gray125\"/></fill>" +
              "</fills>" +

              // borders: 0=no border
              "<borders count=\"1\">" +
                "<border><left/><right/><top/><bottom/><diagonal/></border>" +
              "</borders>" +

              "<cellStyleXfs count=\"1\">" +
                "<xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\"/>" +
              "</cellStyleXfs>" +

              // cellXfs: 0=normal, 1=bold (headers)
              "<cellXfs count=\"2\">" +
                "<xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\"/>" +
                "<xf numFmtId=\"0\" fontId=\"1\" fillId=\"0\" borderId=\"0\" xfId=\"0\"/>" +
              "</cellXfs>" +

            "</styleSheet>";

        // ── Sheet XML ─────────────────────────────────────────────────────────
        private static string Sheet(List<string> headers, List<List<string>> rows)
        {
            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n");
            sb.Append("<worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">");
            sb.Append("<sheetData>");

            int rowNum = 1;

            // Header row — bold (style 1)
            if (headers != null && headers.Count > 0)
            {
                sb.AppendFormat("<row r=\"{0}\">", rowNum);
                for (int c = 0; c < headers.Count; c++)
                    sb.AppendFormat("<c r=\"{0}\" t=\"inlineStr\" s=\"1\"><is><t>{1}</t></is></c>",
                                   ColRef(c) + rowNum, Prep(headers[c]));
                sb.Append("</row>");
                rowNum++;
            }

            // Data rows
            if (rows != null)
            {
                foreach (var row in rows)
                {
                    sb.AppendFormat("<row r=\"{0}\">", rowNum);
                    if (row != null)
                        for (int c = 0; c < row.Count; c++)
                            sb.AppendFormat("<c r=\"{0}\" t=\"inlineStr\"><is><t>{1}</t></is></c>",
                                           ColRef(c) + rowNum, Prep(row[c]));
                    sb.Append("</row>");
                    rowNum++;
                }
            }

            sb.Append("</sheetData></worksheet>");
            return sb.ToString();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private static string ColRef(int zeroIdx)
        {
            if (zeroIdx < 26)
                return ((char)('A' + zeroIdx)).ToString();
            int first  = (zeroIdx / 26) - 1;
            int second =  zeroIdx % 26;
            return ((char)('A' + first)).ToString() + ((char)('A' + second)).ToString();
        }

        private static string Prep(string value)
        {
            if (value == null) return "";
            value = value.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", " | ");
            return XmlEscape(value);
        }

        private static string XmlEscape(string value)
        {
            if (value == null) return "";
            return value
                .Replace("&",  "&amp;")
                .Replace("<",  "&lt;")
                .Replace(">",  "&gt;")
                .Replace("\"", "&quot;");
        }
    }
}
