<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GrowthChartCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.Common.GrowthChartCtl" %>
<fieldset>
    <legend><asp:Label runat="server" ID="lblGrowthChartName" Font-Bold="true" Font-Size="Large"></asp:Label></legend>
    <telerik:RadHtmlChart runat="server" ID="chtGrowthChart" Width="98%" Height="480" Transitions="true">
        <PlotArea>
            <Series>
            </Series>
            <XAxis  Step="6">
                <MinorGridLines Visible="false"></MinorGridLines>
                <MajorGridLines Visible="true"></MajorGridLines>
                <LabelsAppearance Skip="0" Position="Start">
                                        <ClientTemplate>
                            #=value# \n              
                        </ClientTemplate>

                </LabelsAppearance>
            </XAxis>
            <YAxis Step="5">
                <TitleAppearance Text="Units"></TitleAppearance>
                <MinorGridLines Visible="false"></MinorGridLines>
                <LabelsAppearance ></LabelsAppearance>
            </YAxis>
        </PlotArea>
        <Legend>
            <Appearance Visible="true"></Appearance>
        </Legend>

    </telerik:RadHtmlChart>
    <asp:Literal runat="server" ID="ewsInfo"></asp:Literal>
</fieldset>
<br/>