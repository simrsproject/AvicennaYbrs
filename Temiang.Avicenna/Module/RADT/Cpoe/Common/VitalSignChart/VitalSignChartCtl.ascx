<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VitalSignChartCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.VitalSignChartCtl" %>
<fieldset>
    <legend><%=ChartTitle%></legend>
    <telerik:RadHtmlChart runat="server" ID="chtVitalSign" Width="100%" Height="300" Transitions="true" PlotArea-YAxis-TitleAppearance-TextStyle-Padding="20px">
        <PlotArea>
            <Series>
            </Series>
            <XAxis Type="Auto">
                <MinorGridLines Visible="false"></MinorGridLines>
                <MajorGridLines Visible="true"></MajorGridLines>
                <LabelsAppearance Step="1"></LabelsAppearance>
            </XAxis>
            <YAxis Step="5">
                <TitleAppearance Text="Units"></TitleAppearance>
                <MinorGridLines Visible="false"></MinorGridLines>
                <LabelsAppearance Step="2" Skip="1"></LabelsAppearance>
            </YAxis>
        </PlotArea>
        <Legend>
            <Appearance Visible="true"></Appearance>
        </Legend>

    </telerik:RadHtmlChart>
    <asp:Literal runat="server" ID="litVitalSign"></asp:Literal>
    <asp:Literal runat="server" ID="litEwsValueAndScore"></asp:Literal>
</fieldset>
<br />

