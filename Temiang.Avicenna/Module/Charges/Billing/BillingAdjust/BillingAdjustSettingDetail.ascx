<%@ Control Language="C#" AutoEventWireup="true" Codebehind="BillingAdjustSettingDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.BillingAdjustSettingDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsum" runat="server" ValidationGroup="vg" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="vg"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td width="35%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Paramedic" Width="100px"></asp:Label>
                        <asp:HiddenField ID="hfId" runat="server" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboParamedic" runat="server" Width="304px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td class="label">
                        <asp:Label ID="Label11" runat="server" Text="Specialty" Width="100px"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSpecialty" runat="server" Width="304px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRegistrationType" runat="server" Width="304px" 
                            AutoPostBack="True" OnSelectedIndexChanged="cboSRRegistrationType_SelectedIndexChanged" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td class="label">
                        <asp:Label ID="Label12" runat="server" Text="Tariff Type" Width="100px"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboTariffType" runat="server" Width="304px" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td class="label">
                        <asp:Label ID="Label3" runat="server" Text="Guarantor" Width="100px"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboGuarantor" runat="server" Width="304px"
                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                            OnItemDataBound="cboGuarantor_ItemDataBound"
                            OnItemsRequested="cboGuarantor_ItemsRequested"
                        ></telerik:RadComboBox>
                    </td>
                    <td width="20">
                    </td>
                    
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Service Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="304px"
                            OnItemDataBound="cboServiceUnit_ItemDataBound"
                            OnItemsRequested="cboServiceUnit_ItemsRequested"
                        >
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td width="35%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label9" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboItem" runat="server" Width="304px" AutoPostBack="true"
                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                            OnItemDataBound="cboItem_ItemDataBound"
                            OnItemsRequested="cboItem_ItemsRequested"
                            OnSelectedIndexChanged="cboItem_SelectedIndexChanged"
                        >
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label4" runat="server" Text="Class"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboClass" runat="server" Width="304px" 
                        OnSelectedIndexChanged="cboClass_SelectedIndexChanged" AutoPostBack="true">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>        
                <tr>
                    <td class="label">
                        <asp:Label ID="Label6" runat="server" Text="Tarif Component"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboTariffComponent" runat="server" Width="304px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label8" runat="server" Text="Fee Value In Percent"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsFeeValueInPercent" runat="server" Text="Fee Value In Percent" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label7" runat="server" Text="Fee Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtFeeValue" runat="server" Type="Number" NumberFormat-DecimalDigits="2"
                                        Width="100px" MinValue="0" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Fee value required."
                            ControlToValidate="txtFeeValue" ValidationGroup="entry" SetFocusOnError="True"
                            Width="100%">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td width="35%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label10" runat="server" Text="Item Group Replacement" Width="100px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height:200px;overflow:scroll;">
                            <telerik:RadGrid ID="radgrdItemGroup" runat="server" 
                                OnNeedDataSource="radgrdItemGroup_NeedDataSource"
                                OnDataBound="radgrdItemGroup_DataBound"
                                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                                <HeaderContextMenu>
                                </HeaderContextMenu>
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemGroupID">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px" >
                                            <HeaderTemplate>
                                                <label>Select</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkItemGroup" runat="server" ></asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Item Group" UniqueName="ItemGroupName"
                                            SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="vg"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="vg" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<br />
<br />
