using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    public class HtmlWriter
    {

        public virtual void Write(HtmlTextWriter textWriter, IEnumerable<InformationTable> informationTables)
        {
            foreach (var informationTable in informationTables)
            {
                //if (writeAdditionalBreak)
                //{
                //    WriteAdditionalBreak(textWriter);
                //}
                var informationBox = WriteInformationBox(informationTable.Title);
                
                foreach (var memberTable in informationTable.Tables)
                {
                    informationBox.Controls.Add(WriteTitleH3(memberTable.Title));
                    var writeDataTable = WriteDataTable(memberTable.HeaderRows, memberTable.Rows);
                    informationBox.Controls.Add(writeDataTable);
                }

                informationBox.RenderControl(textWriter);
            }
        }

        public virtual void WriteParagraphs(HtmlTextWriter textWriter, IEnumerable<InformationList> informationList)
        {
            foreach (var information in informationList)
            {
                var informationBox = WriteInformationBox(information.Title);

                foreach (var paragraph in information.Paragraphs)
                {
                    informationBox.Controls.Add(WriteTitleH3(paragraph.Title));
                    foreach (var row in paragraph.Rows)
                    {
                        var writeParagraphs = WriteParagraph(row);
                        informationBox.Controls.Add(writeParagraphs);
                    }

                }

                informationBox.RenderControl(textWriter);
            }
        }


        protected virtual Control WriteDataTable(IEnumerable<IEnumerable<string>> headerRows, IEnumerable<IEnumerable<string>> rows)
        {
            var table = new System.Web.UI.WebControls.Table();

            WriteTableRows<TableHeaderRow, TableHeaderCell>(headerRows, table, TableRowSection.TableBody);
            WriteTableRows<TableRow, TableCell>(rows, table, TableRowSection.TableBody);

            return table;
        }

        protected virtual Control WriteInformationBox(string title)
        {
            var divInformation = new HtmlGenericControl("div");
            divInformation.Controls.Add(new HtmlGenericControl("h2") { InnerText = title });
            return divInformation;
        }

        protected virtual Control WriteTitleH3(string title)
        {
            return new HtmlGenericControl("h3") { InnerText = title };
        }

        protected virtual Control WriteParagraph(string content)
        {
            return new HtmlGenericControl("p") { InnerText = content };
        }

        protected void WriteTableRows<T, TCell>(IEnumerable<IEnumerable<string>> rows, System.Web.UI.WebControls.Table table, TableRowSection tableRowSection)
            where T : TableRow, new()
            where TCell : TableCell, new()
        {
            if (HasNoRows(rows))
            {
                return;
            }


            foreach (var row in rows)
            {
                var tableRow = new T();
                tableRow.TableSection = tableRowSection;
                foreach (var columnText in row)
                {
                    WriteHtmlTableColumn<TCell>(tableRow, columnText);
                }
                table.Rows.Add(tableRow);
            }
        }

        private static bool HasNoRows(IEnumerable<IEnumerable<string>> rows)
        {
            return rows == null || !rows.Any();
        }

        protected virtual void WriteHtmlTableColumn<T>(TableRow row, string columnText) where T : TableCell, new()
        {
            var tableCell = new T { Text = columnText };
            row.Cells.Add(tableCell);
        }
    }
}

