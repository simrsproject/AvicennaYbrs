<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="BedBookingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.BedBookingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPatientID" />
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtPatientName" />
                    <telerik:AjaxUpdatedControl ControlID="rbMale" />
                    <telerik:AjaxUpdatedControl ControlID="rbFemale" />
                    <telerik:AjaxUpdatedControl ControlID="txtServiceUnit" />
                    <telerik:AjaxUpdatedControl ControlID="txtRoomBed" />
                    <telerik:AjaxUpdatedControl ControlID="txtParamedic" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend><asp:Label ID="Label1" runat="server" Text="BED INFORMATION" Font-Bold="True" Font-Size="9"></asp:Label></legend>
                    <table width="100%">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBedNo" runat="server" Text="Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtBedNo" runat="server" Width="300px" ReadOnly="true" />
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
                                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td style="width: 20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRoom" runat="server" Text="Room"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRoom" runat="server" Width="300px" ReadOnly="true" />
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
                    <legend><asp:Label ID="Label3" runat="server" Text="BOOKING DETAIL" Font-Bold="True" Font-Size="9"></asp:Label></legend>
                    <table width="100%">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
                                                OnItemDataBound="cboRegistrationNo_ItemDataBound" AutoPostBack="true" OnSelectedIndexChanged="cboRegistrationNo_SelectedIndexChanged">
                                                <ItemTemplate>
                                                    <b>
                                                        <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                                    </b>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                                    &nbsp;-&nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 5 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 20px">
                                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                                ValidationGroup="entry" ControlToValidate="cboRegistrationNo" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="label">
                                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPatientID" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20">
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
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" ErrorMessage="Patient Name required."
                                                ValidationGroup="entry" ControlToValidate="txtPatientName" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
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
                            <td style="width: 50%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtServiceUnit" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRoomBed" runat="server" Text="Room / Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRoomBed" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedic" runat="server" Text="Physician"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtParamedic" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="true" />
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
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
