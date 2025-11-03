<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PatientRelatedDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientRelatedDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID / Medical No" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientID" runat="server" Width="145px" MaxLength="15"
                                            ReadOnly="true" />
                                    </td>
                                    <td>
                                        &nbsp;/&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="144px" MaxLength="15"
                                            ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRSalutation" runat="server" Text="Salutation" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRSalutation" runat="server" Width="300px" Enabled="False" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name" />
                        </td>
                        <td class="entry" style="vertical-align: middle;">
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" MaxLength="50"
                                ReadOnly="True" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name required."
                                ValidationGroup="entry" ControlToValidate="txtFirstName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMiddleName" runat="server" Width="300px" MaxLength="50"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px" MaxLength="50"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCityOfBirth" runat="server" Text="City / Date Of Birth" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtCityOfBirth" runat="server" Width="180px" MaxLength="50" />
                                    </td>
                                    <td>
                                        &nbsp;/&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DateInput-ReadOnly="True"
                                            DatePopupButton-Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="80">
                            <asp:RequiredFieldValidator ID="rfvCityOfBirth" runat="server" ErrorMessage="City Of Birth required."
                                ValidationGroup="entry" ControlToValidate="txtCityOfBirth" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender" />
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="False">
                                <asp:ListItem Value="M" Text="Male" />
                                <asp:ListItem Value="F" Text="Female" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="60">
                            <asp:RequiredFieldValidator ID="rfvSex" runat="server" ErrorMessage="Sex required."
                                ValidationGroup="entry" ControlToValidate="rbtSex" SetFocusOnError="True" Width="20px">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParentSpouseName" runat="server" Text="Parent / Spouse Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParentSpouseName" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvParentSpouseName" runat="server" ErrorMessage="Parent / Spouse Name required."
                                ValidationGroup="entry" ControlToValidate="txtParentSpouseName" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSSN" runat="server" Text="SSN" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSSN" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdPatientRelated" runat="server" OnNeedDataSource="grdPatientRelated_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPatientRelated_UpdateCommand"
        OnDeleteCommand="grdPatientRelated_DeleteCommand" OnInsertCommand="grdPatientRelated_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="PatientID, RelatedPatientID"
            FilterExpression="LastUpdateByUserID <> 'delete' ">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="35px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RelatedPatientID" HeaderText="Patient ID"
                    UniqueName="RelatedPatientID" SortExpression="RelatedPatientID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                    HeaderText="LastUpdateByUserID" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="PatientRelatedItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="PatientRelatedDetailCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
