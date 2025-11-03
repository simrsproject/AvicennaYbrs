<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="UpdateMrnPatientDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.UpdateMrnPatientDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboPatientID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtSalutation2" />
                    <telerik:AjaxUpdatedControl ControlID="txtPatientName2" />
                    <telerik:AjaxUpdatedControl ControlID="txtGender2" />
                    <telerik:AjaxUpdatedControl ControlID="txtPlaceDOB2" />
                    <telerik:AjaxUpdatedControl ControlID="txtAddress2" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="1" cellspacing="5">
        <tr>
            <td style="width: 100%">

                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label3" runat="server" Text="UPDATE FROM" Font-Bold="True" Font-Size="9"></asp:Label>
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromFirstName" runat="server" Text="First Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromFirstName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromMiddleName" runat="server" Text="Middle Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromMiddleName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromLastName" runat="server" Text="Last Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromLastName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 50%" valign="top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label5" runat="server" Text="UPDATE TO" Font-Bold="True" Font-Size="9"></asp:Label>
                                </legend>

                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtToMedicalNo" runat="server" Width="100px" MaxLength="15" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToFirstName" runat="server" Text="First Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtToFirstName" runat="server" Width="300px" MaxLength="50" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToMiddleName" runat="server" Text="Middle Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtToMiddleName" runat="server" Width="300px" MaxLength="50" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToLastName" runat="server" Text="Last Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtToLastName" runat="server" Width="300px" MaxLength="50"/>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
