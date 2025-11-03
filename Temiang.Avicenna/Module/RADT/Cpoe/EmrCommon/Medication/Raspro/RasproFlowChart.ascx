<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RasproFlowChart.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RasproFlowChart" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<style>
    .container {
        position: relative;
    }

        .container .k-diagram {
            margin: 0 auto;
        }
</style>
<script src="<%=Helper.UrlRoot()%>/JavaScript/Common/HTML5UI/html5/core.js" type="text/javascript"></script>
<script src="<%=Helper.UrlRoot()%>/JavaScript/Common/HTML5UI/Mobile/html5/mobile/scroller.js" type="text/javascript"></script>
<script src="<%=Helper.UrlRoot()%>/JavaScript/Common/HTML5UI/DataViz/html5/dataviz/diagram.js" type="text/javascript"></script>
<script src="<%=Helper.UrlRoot()%>/JavaScript/Diagram/Scripts/RadDiagram.js" type="text/javascript"></script>
<script src="<%=Helper.UrlRoot()%>/JavaScript/Diagram/Scripts/RadDiagramSkins.js" type="text/javascript"></script>
<script src="FlowChartScript.js" type="text/javascript"></script>


<fieldset style="width: 96%">
    <legend>Flow Chart</legend>
    Pan the Flow Chart by holding the Ctrl key and click + dragging
        <div class="container">
            <telerik:RadDiagram ID="theDiagram" runat="server" Width="100%" Height="600px" Editable="False">
                <ClientEvents OnLoad="diagram_load" />
                <ShapeDefaultsSettings Visual="visualizeShape">
                    <StrokeSettings Color="#fff" />
                </ShapeDefaultsSettings>
            </telerik:RadDiagram>
        </div>
</fieldset>
