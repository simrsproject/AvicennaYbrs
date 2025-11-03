<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NosocomialCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NosocomialCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/InfusMonitoringCtl.ascx" TagPrefix="uc1" TagName="InfusMonitoringCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/NgtMonitoringCtl.ascx" TagPrefix="uc1" TagName="NgtMonitoringCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/CatheterMonitoringCtl.ascx" TagPrefix="uc1" TagName="CatheterMonitoringCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/SurgeryMonitoringCtl.ascx" TagPrefix="uc1" TagName="SurgeryMonitoringCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/EttMonitoringCtl.ascx" TagPrefix="uc1" TagName="EttMonitoringCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/BedRestMonitoringCtl.ascx" TagPrefix="uc1" TagName="BedRestMonitoringCtl" %>


<telerik:RadTabStrip ID="tspNosocomial" runat="server" MultiPageID="mpgNosocomial" ShowBaseLine="true"
    Align="Left" PerTabScrolling="True"
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Value="infus" Text="Infus" PageViewID="pgInfus"
            Selected="True" />
        <telerik:RadTab runat="server" Value="catheter" Text="Dower Kateter" PageViewID="pgCatheter" />
        <telerik:RadTab runat="server" Value="ngt" Text="NGT" PageViewID="pgNgt" />
        <telerik:RadTab runat="server" Value="surgery" Text="Surgery" PageViewID="pgSurgery" />
        <telerik:RadTab runat="server" Value="ett" Text="Mechanic Ventilation" PageViewID="pgEtt" />
        <telerik:RadTab runat="server" Value="bedrest" Text="Bed Rest" PageViewID="pgBedRest" />
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="mpgNosocomial" runat="server" SelectedIndex="0" ScrollBars="Auto"
    CssClass="multiPage">

    <telerik:RadPageView ID="pgInfus" runat="server">
        <uc1:InfusMonitoringCtl runat="server" ID="infusMonitoringCtl" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="pgCatheter" runat="server">
        <uc1:CatheterMonitoringCtl runat="server" ID="catheterMonitoringCtl" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="pgNgt" runat="server">
        <uc1:NgtMonitoringCtl runat="server" ID="ngtMonitoringCtl" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="pgSurgery" runat="server">
        <uc1:SurgeryMonitoringCtl runat="server" id="surgeryMonitoringCtl" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="pgEtt" runat="server">
        <uc1:EttMonitoringCtl runat="server" id="ettMonitoringCtl" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="pgBedRest" runat="server">
        <uc1:BedRestMonitoringCtl runat="server" id="bedRestMonitoringCtl" />
    </telerik:RadPageView>
</telerik:RadMultiPage>