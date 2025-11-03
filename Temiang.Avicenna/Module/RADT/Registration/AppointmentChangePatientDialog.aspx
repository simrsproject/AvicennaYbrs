<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="AppointmentChangePatientDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.AppointmentChangePatientDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">
            
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboPatientID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPatientID" />
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtFirstName" />
                    <telerik:AjaxUpdatedControl ControlID="txtMiddleName" />
                    <telerik:AjaxUpdatedControl ControlID="txtLastName" />
                    <telerik:AjaxUpdatedControl ControlID="txtDateOfBirth" />
                    <telerik:AjaxUpdatedControl ControlID="txtAddress" />
                    <telerik:AjaxUpdatedControl ControlID="txtSsn" />
                    <telerik:AjaxUpdatedControl ControlID="txtGuarantorCardNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtMobilePhoneNo" />
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
            <td class="label">
                <asp:Label ID="lblPatientID" runat="server" Text="Medical No"></asp:Label>
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                                OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                    </b>&nbsp;-&nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                    &nbsp;|&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>                                     
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 7px"></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15" ReadOnly="true" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" ReadOnly="true">
                </telerik:RadTextBox>
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMiddleName" runat="server" Width="300px" ReadOnly="true">
                </telerik:RadTextBox>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px" ReadOnly="true">
                </telerik:RadTextBox>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
            </td>
            <td class="entry2Column">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DatePopupButton-Enabled="false" Enabled="false">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="(Format: DD/MM/YYYY)" ForeColor="red" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" Height="100px" ReadOnly="true">
                </telerik:RadTextBox>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSsn" runat="server" Text="SSN"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSsn" runat="server" Width="300px" ReadOnly="false">
                </telerik:RadTextBox>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblGuarantorCardNo" runat="server" Text="BPJS No / Guarantor Card No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtGuarantorCardNo" runat="server" Width="300px" MaxLength="50" ReadOnly="false">
                </telerik:RadTextBox>
            </td>
            <td width="10px"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPhoneNo" runat="server" Text="Mobile Phone No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMobilePhoneNo" runat="server" Width="300px" MaxLength="50" ReadOnly="false">
                </telerik:RadTextBox>
            </td>
            <td width="10px"></td>
            <td></td>
        </tr>
    </table>
                             
</asp:Content>
