<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ClassDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ClassDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Class ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class ID required."
                                ValidationGroup="entry" ControlToValidate="txtClassID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassName" runat="server" Text="Class Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClassName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvClassName" runat="server" ErrorMessage="Class Name required."
                                ValidationGroup="entry" ControlToValidate="txtClassName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtShortName" runat="server" Width="100px" MaxLength="35" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvShortName" runat="server" ErrorMessage="Short Name required."
                                ValidationGroup="entry" ControlToValidate="txtShortName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRClassRL" runat="server" Text="Class RL"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRClassRL" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRClassRL" runat="server" ErrorMessage="Class RL required."
                                ValidationGroup="entry" ControlToValidate="cboSRClassRL" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">BPJS Class ID
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboBpjsClassID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Sequence No
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtClassSeq" runat="server" Width="100px" />
                        </td>
                        <td width="20"></td>
                    </tr>
                    
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMarginPercentage" runat="server" Text="Item Medic Margin"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMarginPercentage" runat="server" Type="Percent"
                                Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMarginPercentage" runat="server" ErrorMessage="Item Medic Margin Percentage required."
                                ValidationGroup="entry" ControlToValidate="txtMarginPercentage" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMargin2Percentage" runat="server" Text="Item Non Medic Margin"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtMargin2Percentage" runat="server" Type="Percent"
                                Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMargin2Percentage" runat="server" ErrorMessage="Item Non Medic Margin Percentage required."
                                ValidationGroup="entry" ControlToValidate="txtMargin2Percentage" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDepositAmount" runat="server" Text="Deposit Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtDepositAmount" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvDepositAmount" runat="server" ErrorMessage="Deposit Amount required."
                                ValidationGroup="entry" ControlToValidate="txtDepositAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsInPatientClass" runat="server" Text="Inpatient Class" />
                        </td>
                        <td width="20"></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsTariffClass" runat="server" Text="Tariff Class" />
                        </td>
                        <td width="20"></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlUpdate">
                        <tr>
                            <td class="entry" colspan="3">
                                <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click">
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/process16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblUpdate" Text="Update Item Product Margin"
                                        Font-Bold="True" ForeColor="Blue"></asp:Label>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Bridging & Integration" PageViewID="pgvAliasName"
                Selected="true" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView runat="server" ID="pgvAliasName" Selected="true">
            <telerik:RadGrid ID="grdAliasName" runat="server" OnNeedDataSource="grdAliasName_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAliasName_UpdateCommand"
                OnDeleteCommand="grdAliasName_DeleteCommand" OnInsertCommand="grdAliasName_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ClassID, SRBridgingType, BridgingID"
                    PageSize="15">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="BridgingTypeName" HeaderText="Bridging Type"
                            UniqueName="BridgingTypeName" SortExpression="BridgingTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BridgingID" HeaderText="Bridging ID"
                            UniqueName="BridgingID" SortExpression="BridgingID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="BridgingName" HeaderText="Bridging Name" UniqueName="BridgingName"
                            SortExpression="BridgingName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ClassAliasDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ClassAliasEditCommand">
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
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
