<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="EditBedNoDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EditBedNoDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboRoomID" />
                    <telerik:AjaxUpdatedControl ControlID="cboBedID" />
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" />
                    <telerik:AjaxUpdatedControl ControlID="cboChargeClassID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboRoomID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboBedID" />
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" />
                    <telerik:AjaxUpdatedControl ControlID="cboChargeClassID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboBedID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboRoomID" />
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" />
                    <telerik:AjaxUpdatedControl ControlID="cboChargeClassID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
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
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblMedicalNo" Text="Medical No" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPatientName" Text="Patient Name" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
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
                                        <td style="width: 20px"></td>
                                        <td></td>
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
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblRoomBed" Text="Room" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtRoom" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblBedNo" Text="Bed" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtBed" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label runat="server" ID="lblPhysicianName" Text="Physician" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox runat="server" ID="txtPhysicianName" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
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
                    <legend>EDIT BED NO</legend>
                    <table width="100%">
                        <tr>
                            <td style="vertical-align: top; width: 50%">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="From Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromServiceUnitID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblFromServiceUnitName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromRoomID" runat="server" Text="From Room"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromRoomID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblFromRoomName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromBedID" runat="server" Text="From Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromBedID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromClassID" runat="server" Text="From Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromClassID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblFromClassName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromChargeClassID" runat="server" Text="From Charge Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromChargeClassID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblFromChargeClassName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="To Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                                MarkFirstMatch="true" OnItemDataBound="cboServiceUnitID_ItemDataBound" OnItemsRequested="cboServiceUnitID_ItemsRequested"
                                                EnableLoadOnDemand="true" NoWrap="True">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 10 result
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                            <%--<telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" AllowCustomText="true"
                                                Filter="Contains">
                                            </telerik:RadComboBox>--%>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="To Service Unit required."
                                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToRoomID" runat="server" Text="To Room"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboRoomID" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboRoomID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                                MarkFirstMatch="true" OnItemDataBound="cboRoomID_ItemDataBound" OnItemsRequested="cboRoomID_ItemsRequested"
                                                EnableLoadOnDemand="true" NoWrap="True">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "RoomName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 10 result
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                            <%--<telerik:RadComboBox ID="cboRoomID" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboRoomID_SelectedIndexChanged" AllowCustomText="true"
                                                Filter="Contains">
                                            </telerik:RadComboBox>--%>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToRoomID" runat="server" ErrorMessage="To Room required."
                                                ValidationGroup="entry" ControlToValidate="cboRoomID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToBedID" runat="server" Text="To Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboBedID" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboBedID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                                MarkFirstMatch="true" OnItemDataBound="cboBedID_ItemDataBound" OnItemsRequested="cboBedID_ItemsRequested"
                                                EnableLoadOnDemand="true" NoWrap="True">
                                                <ItemTemplate>
                                                    <b>
                                                        <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                                    </b>&nbsp;:&nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                                    &nbsp; (<%# DataBinder.Eval(Container.DataItem, "Sex")%>)
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 10 result
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToBedID" runat="server" ErrorMessage="To Bed No required."
                                                ValidationGroup="entry" ControlToValidate="cboBedID" SetFocusOnError="True" Width="100%">
                                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToClassID" runat="server" Text="To Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px" Enabled="False">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToClassID" runat="server" ErrorMessage="To Class required."
                                                ValidationGroup="entry" ControlToValidate="cboClassID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToChargeClassID" runat="server" Text="To Charge Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboChargeClassID" runat="server" Width="300px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToChargeClassID" runat="server" ErrorMessage="To Charge Class required."
                                                ValidationGroup="entry" ControlToValidate="cboChargeClassID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
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
