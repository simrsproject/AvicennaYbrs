<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="EpisodeDiagnoseEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EpisodeDiagnoseEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label" style="font-style: italic">Diagnosis
            </td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="cboDiagnoseID" runat="server" Width="304px" EmptyMessage="Select a Diagnosis"
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboDiagnoseID_SelectedIndexChanged">
                                <WebServiceSettings Method="Diagnoses" Path="~/WebService/ComboBoxDataService.asmx" />
                                <ClientItemTemplate>
                     <div>
                        <ul class="details">
                            <li class="bold"><span>#= Value # </span></li>
                            <li class="small"><span>#= Attributes.DiagnoseName # </span></li>
                            <li class="smaller"><span>DTD: [#= Attributes.DtdNo #] #= Attributes.DtdName #  </span></li>
                        </ul>
                    </div>
                                </ClientItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsOldCase" runat="server" Text="Old Case" Font-Italic="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvDiagnoseName" runat="server" ErrorMessage="Diagnosis required."
                                ValidationGroup="entry" ControlToValidate="cboDiagnoseID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic">Diagnosis Type
            </td>
            <td>
                <telerik:RadComboBox ID="cboSRDiagnoseType" runat="server" Width="304px" />
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic">Diagnosis Text
            </td>
            <td>
                <telerik:RadTextBox ID="txtDiagnosisText" runat="server" Width="99.5%" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="label" style="font-style: italic">Notes
            </td>
            <td>
                <telerik:RadTextBox ID="txtDiagnosisNotes" runat="server" Width="99.5%" TextMode="MultiLine" />
            </td>
        </tr>
    </table>
</asp:Content>
