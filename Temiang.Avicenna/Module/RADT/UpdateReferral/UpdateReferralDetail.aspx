<%@ Page Title="Update Referral" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="UpdateReferralDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.UpdateReferralDetail" %>

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
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboSRReferralGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReferralID" />
                    <telerik:AjaxUpdatedControl ControlID="txtReferralName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboReferralID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtReferralName" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRReferralGroup" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
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
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
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
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="25px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="246px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
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
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
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
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 103px">
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="true">
                                        </telerik:RadMaskedTextBox>
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
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="true" />
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
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
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
                        <asp:Label ID="Label3" runat="server" Text="Referral Information" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRReferralGroup" runat="server" Text="Referral Group"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRReferralGroup" runat="server" Width="300px" AutoPostBack="true"
                                                EnableLoadOnDemand="true" MarkFirstMatch="False" HighlightTemplatedItems="true"
                                                OnItemDataBound="cboSRReferralGroup_ItemDataBound" OnItemsRequested="cboSRReferralGroup_ItemsRequested"
                                                OnSelectedIndexChanged="cboSRReferralGroup_SelectedIndexChanged">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 20 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvSRReferralGroup" runat="server" ErrorMessage="Referral Group is required."
                                                ValidationGroup="entry" ControlToValidate="cboSRReferralGroup" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReferralID" runat="server" Text="Referral"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboReferralID" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboReferralID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                                MarkFirstMatch="false" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboReferralID_ItemDataBound"
                                                OnItemsRequested="cboReferralID_ItemsRequested">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReferralName" runat="server" Text="Referral Description"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtReferralName" runat="server" Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td style="width: 60px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top" width="100%">
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
