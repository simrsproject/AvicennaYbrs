using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Text;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class RasproFlowChart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        internal void PopulateFlowChart(string rasproID, string currentRasproLineID)
        {
            var colls = new RasproCollection();
            colls.Query.Where(colls.Query.SRRaspro == rasproID);
            colls.Query.OrderBy(colls.Query.SeqNo.Ascending);
            colls.Query.Load();

            // Start
            var start = new DiagramShape { Type = "start", Id = "start", X = 180, Y = 70 };
            start.ContentSettings.Text = "Start";
            start.FillSettings.Color = "#cf3737";
            theDiagram.ShapesCollection.Add(start);

            var y = 0;
            var no = 1;
            bool isCurrentRasproLineIDCreated = false;
            Raspro prevItem = null;
            var dcon = new DiagramConnection();
            foreach (Raspro item in colls)
            {
                // Question
                y = y + 260;
                var quest = new DiagramShape { Type = "question", Id = string.Format("q_{0}", item.RasproLineID), Width = 400, Height = 200, X = 30, Y = y };
                quest.ContentSettings.Text = string.Format("{0}. {1}", no, WrapText(item.Spesification.Replace("<br/>", "\\n"), 30));
                //quest.StrokeSettings.Color = !isCurrentRasproLineIDCreated? "#ffffff": "#000000";
                quest.StrokeSettings.Color = "#000000";
                quest.FillSettings.Color = !isCurrentRasproLineIDCreated ? "#ffbe33" : "#f2f2f2"; // Yg sudah dilewati diberi warna kuning jika belum diberi warna abu2
                theDiagram.ShapesCollection.Add(quest);

                
                // Text dikanan
                var tToRight = new DiagramShape { Type = "text", Id = string.Format("t_toright_{0}", item.RasproLineID), X = 450, Y = y + 80 };
                tToRight.ContentSettings.Text = (!item.NoAction.Equals("NEXT") ? "Tidak" : "Ya");
                tToRight.StrokeSettings.Color = "#000";
                theDiagram.ShapesCollection.Add(tToRight);

                // Text dibawah
                var tToBottom = new DiagramShape { Type = "text", Id = string.Format("t_tobottom_{0}", item.RasproLineID), X = 260, Y = y + 220 };
                tToBottom.ContentSettings.Text = (!item.NoAction.Equals("NEXT") ? "Ya" : "Tidak");
                tToBottom.StrokeSettings.Color = "#000";
                theDiagram.ShapesCollection.Add(tToBottom);

                // Right Action
                var rightActionText = tToRight.ContentSettings.Text == "Ya" ? item.YesActionDescription : item.NoActionDescription;
                if (!string.IsNullOrWhiteSpace(rightActionText))
                {
                    var rightAction = new DiagramShape
                    {
                        Type = "rectangle",
                        Id = string.Format("a_right_{0}", item.RasproLineID),
                        Width = 400,
                        Height = 100,
                        X = 500,
                        Y = y + 50
                    };
                    rightAction.ContentSettings.Text = rightActionText.Replace("<br/>", "\\n");
                    rightAction.ContentSettings.Align = "Left";
                    rightAction.StrokeSettings.Color = "#000000";
                    rightAction.FillSettings.Color = !isCurrentRasproLineIDCreated ? "#7de3fc" : "#f2f2f2"; 
                    theDiagram.ShapesCollection.Add(rightAction);

                    // Add Connection Question to Right Action
                    dcon = new DiagramConnection();
                    dcon.FromSettings.ShapeId = string.Format("q_{0}", item.RasproLineID);
                    dcon.FromSettings.Connector = "Right";
                    dcon.ToSettings.ShapeId = string.Format("a_right_{0}", item.RasproLineID);
                    dcon.ToSettings.Connector = "Left";
                    theDiagram.ConnectionsCollection.Add(dcon);
                }

                // Bottom Action
                var bottomActionText = tToBottom.ContentSettings.Text == "Ya" ? item.YesActionDescription : item.NoActionDescription;
                if (!string.IsNullOrWhiteSpace(bottomActionText))
                {
                    // Add Action No dibawahnya
                    y = y + 260;

                    var bottomAction = new DiagramShape
                    {
                        Type = "rectangle",
                        Id = string.Format("a_bottom_{0}", item.RasproLineID),
                        Width = 400,
                        Height = 100,
                        X = 30,
                        Y = y
                    };

                    //bottomAction.ContentSettings.Text = WrapText(bottomActionText.Replace("<br/>", "\\n"), 50);
                    bottomAction.ContentSettings.Text = bottomActionText.Replace("<br/>", "\\n");
                    bottomAction.ContentSettings.Align = "Left";
                    theDiagram.ShapesCollection.Add(bottomAction);
                    // Connection Question to bottom Action Action
                    AddConnectionToBottom(string.Format("q_{0}", item.RasproLineID),
                        string.Format("a_bottom_{0}", item.RasproLineID), !isCurrentRasproLineIDCreated);
                }


                // Create Connection from prev to current item
                if (prevItem != null)
                {
                    AddConnectionToBottom(string.Format("q_{0}", prevItem.RasproLineID),
                        string.Format("q_{0}", item.RasproLineID), !isCurrentRasproLineIDCreated);
                }
                prevItem = item;
                no++;

                if (currentRasproLineID == item.RasproLineID)
                    isCurrentRasproLineIDCreated = true;
            }

            // Connection Start to first Question
            AddConnectionToBottom("start",
                string.Format("q_{0}", colls[0].RasproLineID), true);


        }

        private void AddConnectionToBottom(string fromShapeId, string toShapeId, bool isHightLight)
        {
            var dcon = new DiagramConnection();

            if (isHightLight)
            {
                dcon.StrokeSettings.Color = "#0000ff"; // blue
            }

            dcon.FromSettings.ShapeId = fromShapeId;
            dcon.FromSettings.Connector = "Bottom";
            dcon.ToSettings.ShapeId = toShapeId;
            dcon.ToSettings.Connector = "Top";

            theDiagram.ConnectionsCollection.Add(dcon);
        }
        private static string WrapText(string value, int charactersToWrapAt = 35, int maxLength = 250)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            value = value.Replace("  ", " ");
            var words = value.Split(' ');
            var sb = new StringBuilder();
            var currString = new StringBuilder();

            foreach (var word in words)
            {
                if (currString.Length + word.Length + 1 < charactersToWrapAt) // The + 1 accounts for spaces
                {
                    sb.AppendFormat(" {0}", word);
                    currString.AppendFormat(" {0}", word);
                }
                else
                {
                    currString.Clear();
                    sb.AppendFormat("{0}{1}", "\\n", word);
                    currString.AppendFormat(" {0}", word);
                }
            }

            if (sb.Length > maxLength)
            {
                return sb.ToString().Substring(0, maxLength) + " ...";
            }

            return sb.ToString().TrimStart().TrimEnd();
        }

    }
}