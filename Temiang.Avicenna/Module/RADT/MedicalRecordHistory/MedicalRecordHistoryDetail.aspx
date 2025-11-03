<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="MedicalRecordHistoryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecordHistoryDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .menuarea {
            z-index: 999999 !important;
        }
    </style>
    <script type="text/javascript">
        function InitWindows() {
            var oWnd = $find("<%= winHistory.ClientID %>");
            oWnd.show();
            //oWnd.maximize();
            oWnd.add_pageLoad(onClientPageLoad);
        }

        function gotoListUrl() {
            window.location.href = 'MedicalRecordHistoryList.aspx';
        }

        function OnClicked(sender, eventArgs) {
            var patientID = '<%= Request.QueryString["patientID"] %>';
            var oWnd = $find("<%= winHistory.ClientID %>");
            switch (eventArgs.get_item().get_index()) {
                case 0:
                    oWnd.setUrl('PatientInformationDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 1:
                    oWnd.setUrl('PatientHealthRecordDetailList.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 2:
                    oWnd.setUrl('PatientPrescriptionDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 3:
                    oWnd.setUrl('PatientRegistrationDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 4:
                    oWnd.setUrl('PatientTransferDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 5:
                    oWnd.setUrl('PatientChargesDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 6:
                    oWnd.setUrl('PatientPaymentDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 7:
                    oWnd.setUrl('PatientEpisodeSOAPEDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 8:
                    oWnd.setUrl('JobOrderResultDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 9:
                    oWnd.setUrl('PatientEpisodeDiagnosisDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
                case 10:
                    oWnd.setUrl('PatientEpisodeProcedureDetail.aspx?patientID=' + patientID);
                    oWnd.show();
                    break;
            }
            oWnd.set_title(eventArgs.get_item().get_text());
            //oWnd.maximize();
            oWnd.add_pageLoad(onClientPageLoad);
        }

    </script>

    <telerik:RadWindow runat="server" Animation="None" Width="100%" Height="450px"
        Behavior="none" Top="0" Left="0"
        ShowContentDuringLoad="False" VisibleStatusbar="False" RestrictionZoneID="offsetElement"
        Style="z-index: -1" VisibleOnPageLoad="true" ID="winHistory" DestroyOnClose="True"
        InitialBehavior="Maximize" />
    <table width="100%" cellpadding="0" cellspacing="0" border="1">
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblPatientName" runat="server" Font-Bold="true" Font-Size="Large" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDoB" runat="server" Font-Bold="false" Font-Italic="true" Font-Size="Small"
                    Text="DoB" />
                &nbsp;&nbsp;
                <asp:Label ID="lblCityOfBirth" runat="server" Font-Bold="True" Font-Size="Medium" />
                <asp:Label ID="lblSeparate" runat="server" Font-Bold="True" Font-Size="Medium" Text="," />
                &nbsp;
                <asp:Label ID="lblDateOfBirth" runat="server" Font-Bold="true" Font-Size="Medium" />
                &nbsp;&nbsp;
                <asp:Label ID="lblAge" runat="server" Font-Bold="true" Font-Size="Medium" />
                &nbsp;&nbsp;
                <asp:Label ID="lblGender" runat="server" Font-Bold="false" Font-Italic="true" Font-Size="Small"
                    Text="Gender" />
                &nbsp;&nbsp;
                <asp:Label ID="lblSex" runat="server" Font-Bold="true" Font-Size="Medium" />
                &nbsp;&nbsp;
                <asp:Label ID="lblMedical" runat="server" Font-Bold="false" Font-Italic="true" Font-Size="Small"
                    Text="Medical No" />
                &nbsp;&nbsp;
                <asp:Label ID="lblMedicalNo" runat="server" Font-Bold="true" Font-Size="Medium" />
                &nbsp;&nbsp;
                <asp:Label ID="lblDiagnostic" runat="server" Font-Bold="false" Font-Italic="true" Font-Size="Small"
                    Text="Radiology No" Visible="False" />
                &nbsp;&nbsp;
                <asp:Label ID="lblDiagnosticNo" runat="server" Font-Bold="true" Font-Size="Medium" Visible="False"/>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 10%">
                <table cellpadding="0" cellspacing="0" width="100%" style="height: 700px">
                    <tr valign="top">
                        <td>
                            <telerik:RadPanelBar runat="server" ID="rpbHistory" OnClientItemClicked="OnClicked">
                                <CollapseAnimation Type="None" />
                                <Items>
                                    <telerik:RadPanelItem Text="Personal Identification" />
                                    <telerik:RadPanelItem Text="Patient Health Record" />
                                    <telerik:RadPanelItem Text="Prescription" />
                                    <telerik:RadPanelItem Text="Registration" />
                                    <telerik:RadPanelItem Text="Room Transfer & Movement" />
                                    <telerik:RadPanelItem Text="Charges" Expanded="True" />
                                    <telerik:RadPanelItem Text="Payment" Expanded="True" />
                                    <telerik:RadPanelItem Text="Episode SOAP" Visible="false" />
                                    <telerik:RadPanelItem Text="Job Order Result" Visible="False" />
                                    <telerik:RadPanelItem Text="Episode Diagnosis" Visible="false" />
                                    <telerik:RadPanelItem Text="Episode Procedure" Visible="false" />
                                </Items>
                                <ExpandAnimation Type="None" />
                            </telerik:RadPanelBar>
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td>
                            <telerik:RadPanelBar runat="server" ID="rpbList" OnClientItemClicked="gotoListUrl">
                                <CollapseAnimation Type="None" />
                                <Items>
                                    <telerik:RadPanelItem Text="Back To List" ImageUrl="~/Images/Toolbar/details16.png"
                                        HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
                                </Items>
                                <ExpandAnimation Type="None" />
                            </telerik:RadPanelBar>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 90%" valign="top">
                <div id="offsetElement" style="width: 100%; height: 700px; z-index: -1" />
            </td>
        </tr>
    </table>
</asp:Content>
