<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ProphylaxisEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ProphylaxisEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Telerik.Web.UI.Skins" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.Emr" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproFlowChart.ascx" TagPrefix="uc1" TagName="RasproFlowChart" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproHeader.ascx" TagPrefix="uc1" TagName="RasproHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <style>
            .AutoHeightGridClass .rgDataDiv {
                height: auto !important;
            }
        </style>
        <script type="text/javascript">
            function onOkClick() {
                if (Page_ClientValidate()) {
                    // Tampilkan tandatangan
                    var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html';
                    var oWnd = $find("<%= winImage.ClientID %>");
                    oWnd.setUrl(url);
                    oWnd.show();
                }
            }

            function winImage_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById('<%=hdnImage.ClientID %>').value = arg.image;

                    // PostBack
                    __doPostBack("<%= btnOk.UniqueID %>", 'save');
                }
            }
            function entryEpisodeProcedure(seqno, bookingno) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/EpisodeProcedure/EpisodeProcedureEntry.aspx?md=read&parid=&patid=<%= PatientID %>&regno=<%= RegistrationNo %>' +
                    '&seqno=' +
                    seqno +
                    '&bno=' +
                    bookingno + '&unit=<%=Request.QueryString["unit"]%>';

                openWindow(url);
            }
            function openWindow(url) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <asp:HiddenField runat="server" ID="hdnRegistrationNo" />

    <asp:HiddenField runat="server" ID="hdnImage" />
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
    </telerik:RadWindow>

    <fieldset style="width: 1000px">
        <legend>
            <asp:Label runat="server" ID="lblRasproName" Text="PROPHYLAXIS ANTIBIOTIC FORM" Font-Bold="True" Font-Size="14px"></asp:Label>
        </legend>
        <fieldset>
            <asp:Label runat="server" ID="lblRasproNote" Text="" Font-Size="Medium"></asp:Label>
        </fieldset>
        <table width="100%">
            <tr>
                <td style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="label">Registration No
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Medical No
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Name
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Birth Date
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="160px" DatePopupButton-Visible="False" Enabled="False" MinDate="01/01/1900" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>

                    </table>
                </td>
                <td style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="label">Date and Time
                            </td>
                            <td class="entry">
                                <telerik:RadDateTimePicker ID="txtRasproDateTime" runat="server" Width="160px" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">DPJP
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Service Unit
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" Enabled="False" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 1000px">
        <legend>SURGERY</legend>
        <table width="600px">
            <tr>
                <td style="width: 120px;">Surgery Name</td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtSurgeryName" Width="100%"></telerik:RadTextBox>
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvConsume" runat="server" ErrorMessage="Please specify a Surgery Name"
                        ControlToValidate="txtSurgeryName" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 120px;">Surgery Type</td>
                <td>
                    <telerik:RadDropDownTree runat="server" ID="cboAbRestrictionID" Width="100%"
                        DefaultMessage="Choose a Surgery" DefaultValue="0" DataValueField="AbRestrictionID"
                        DataTextField="AbRestrictionName" DataFieldID="AbRestrictionID" DataFieldParentID="ParentID">
                        <DropDownSettings Height="250px" CloseDropDownOnSelection="true" />
                    </telerik:RadDropDownTree>
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please specify a Surgery Type"
                        ControlToValidate="cboAbRestrictionID" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 120px;">Surgery Category</td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboSRWoundClassification" Width="100%"></telerik:RadComboBox>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 1000px">
        <legend>ANTIBIOTIK</legend>
        <div style="font-style: italic;">(*Antibiotik diisi dientrian resep setelah form ini diclose)</div>
    </fieldset>
    <asp:Panel runat="server" ID="panMenu" class="footer">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70" OnClientClick="onOkClick();return false;" ValidationGroup="entry" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70" OnClientClick="Close();return false;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
