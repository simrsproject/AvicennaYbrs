<%@ Page Title="Edit Physician" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="EditPhysicianDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EditPhysicianDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="700px" Height="300px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboPhysicianID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboQue" />
                    <telerik:AjaxUpdatedControl ControlID="txtPhysicianSenders" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>PATIENT INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRegistrationNo" Text="Registration No" />
                                        </td>
                                        <td class="entry">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                                            ReadOnly="true" />
                                                    </td>
                                                    <td>
                                                        <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                                                Text=""></asp:Label>&nbsp; </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblMedicalNo" Text="Medical No" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPatientName" Text="Patient Name" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" Enabled="false" />
                                            <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" Enabled="false" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblServiceUnit" Text="Service Unit" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtServiceUnitName" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRoomBed" Text="Room" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtRoom" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblBedNo" Text="Bed" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtBed" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPhysicianName" Text="Physician" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtPhysicianName" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>PHYSICIAN</legend>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%;vertical-align: top">
                                <table width="100%">
                                    <tr>
                            <td class="label">
                                <asp:Label ID="lblPhysicianID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboPhysicianID_ItemDataBound"
                                    OnItemsRequested="cboPhysicianID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboPhysicianID_SelectedIndexChanged">
                                    <FooterTemplate>
                                        Note : Show max 30 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician required."
                                    ValidationGroup="entry" ControlToValidate="cboPhysicianID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                        <tr id="tblSenders" runat="server">
                            <td class="label">
                                <asp:Label ID="lblPhysicianSenders" runat="server" Text="Physician Senders"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPhysicianSenders" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td width="20px">
                            </td>
                        </tr>
                        <tr id="tblQue" runat="server">
                            <td class="label">
                                <asp:Label ID="lblQue" runat="server" Text="Que No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboQue" runat="server" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                        <tr id="tblShift" runat="server">
                            <td class="label">
                                <asp:Label ID="lblShift" runat="server" Text="Shift"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRShift" runat="server" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px" />
                            <td />
                        </tr>
                                </table>
                            </td>
                            <td style="width: 50%;vertical-align: top">
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
