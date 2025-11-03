<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="CheckInConfirmationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.CheckInConfirmationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo" />
                            </a>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationTime" runat="server" Width="50px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnit" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomBed" runat="server" Text="Room / Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomBed" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClass" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClass" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsRoomIn" runat="server" Text="Room In" Enabled="False" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label5" runat="server" Text="CHECKIN CONFIRMED DETAIL" Font-Bold="True"
                            Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSmfId" runat="server" Text="SMF"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSmfID" runat="server" Width="300px" AllowCustomText="true"
                                                Filter="Contains" />
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvSmfId" runat="server" ErrorMessage="SMF required."
                                                ValidationGroup="entry" ControlToValidate="cboSmfID" SetFocusOnError="True" Width="100%">
                                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                        </td>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsFileReceived" Text="Medical File Received" runat="server" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblArrivedDateTime" runat="server" Text="Arrived Date / Time"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtArrivedDate" runat="server" Width="100px" />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadMaskedTextBox ID="txtArrivedTime" runat="server" Mask="<00..23>:<00..59>"
                                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                        </telerik:RadMaskedTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvArrivedDate" runat="server" ErrorMessage="Arrived Date required."
                                                ValidationGroup="entry" ControlToValidate="txtArrivedDate" SetFocusOnError="True">
                                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>&nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRTransferredPatientWith" runat="server" Text="Transferred With"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRTransferredPatientWith" runat="server" Width="300px" />
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvSRTransferredPatientWith" runat="server" ErrorMessage="Transferred With required."
                                                ValidationGroup="entry" ControlToValidate="cboSRTransferredPatientWith" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTransferredByUserID" runat="server" Text="Transferred By"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox runat="server" ID="cboTransferredByUserID" Width="300px" EnableLoadOnDemand="true"
                                                HighlightTemplatedItems="true" OnItemDataBound="cboUserID_ItemDataBound" OnItemsRequested="cboTransferredByUserID_ItemsRequested">
                                                <ItemTemplate>
                                                    <b>
                                                        <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                    </b>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 20 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvTransferredByUserID" runat="server" ErrorMessage="Transferred By required."
                                                ValidationGroup="entry" ControlToValidate="cboTransferredByUserID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReceivedByUserID" runat="server" Text="Received By"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox runat="server" ID="cboReceivedByUserID" Width="300px" EnableLoadOnDemand="true"
                                                HighlightTemplatedItems="true" OnItemDataBound="cboUserID_ItemDataBound" OnItemsRequested="cboReceivedByUserID_ItemsRequested"
                                                Enabled="False">
                                                <ItemTemplate>
                                                    <b>
                                                        <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                    </b>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 20 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvReceivedByUserID" runat="server" ErrorMessage="Received By required."
                                                ValidationGroup="entry" ControlToValidate="cboTransferredByUserID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top" width="100%">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
