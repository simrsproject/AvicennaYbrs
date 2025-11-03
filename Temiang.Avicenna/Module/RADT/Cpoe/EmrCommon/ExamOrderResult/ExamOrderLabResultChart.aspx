<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ExamOrderLabResultChart.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ExamOrderLabResultChart" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Show data from</td>
            <td style="width: 110px">
                <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="110px"></telerik:RadDatePicker>
            </td>
            <td style="text-align: left">
                <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnRefresh_Click" ToolTip="Search" />
            </td>
            <td></td>
        </tr>
    </table>
    <telerik:RadHtmlChart runat="server" ID="chartControl" Width="860" Height="450" Transitions="true">
        <PlotArea>
            <Series>
            </Series>
            <XAxis Type="Date">
                <LabelsAppearance DataFormatString="{0:dd-MMM HH:MM}" RotationAngle="-45" />
                <MinorGridLines Visible="true"></MinorGridLines>
                <MajorGridLines Visible="true"></MajorGridLines>
            </XAxis>
            <YAxis>
                <TitleAppearance Text="Units"></TitleAppearance>
                <MinorGridLines Visible="false"></MinorGridLines>
            </YAxis>
        </PlotArea>
        <Legend>
            <Appearance Visible="true"></Appearance>
        </Legend>
        <ChartTitle Text="Vital Sign">
        </ChartTitle>

    </telerik:RadHtmlChart>
</asp:Content>
