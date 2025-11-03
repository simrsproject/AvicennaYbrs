<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="BudgetingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.BudgetingByItem.BudgetingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItemTransactionItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemTransactionItem" LoadingPanelID="lp1" />
                    <telerik:AjaxUpdatedControl ControlID="lblSumBudgetAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblTotalItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="lp1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="1000px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width:50%;">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Budgeting No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><telerik:RadTextBox ID="txtBudgetingNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" /></td>
                                    <td>&nbsp;</td>
                                    <td>Rev.</td>
                                    <td>&nbsp;</td>
                                    <td><telerik:RadTextBox ID="txtRev" runat="server" Width="50px" MaxLength="20" ReadOnly="true" /></td>
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
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="true">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboYear" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="Year required."
                                ValidationGroup="entry" ControlToValidate="cboYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Budgeting Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRBudgetingGroup" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Budgeting group required."
                                ValidationGroup="entry" ControlToValidate="cboSRBudgetingGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width:50%;">
                <table id="tblStatus" runat="server" cellpadding="0" cellspacing="0" 
                    class="info success" style="font-size:medium; width:100%;">
                    <tr>
                        <td style="width:150px">Total Item</td>
                        <td>:</td>
                        <td><asp:Label ID="lblTotalItem" runat="server"></asp:Label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Total Budget</td>
                        <td>:</td>
                        <td><asp:Label ID="lblSumBudgetAmount" runat="server"></asp:Label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width:150px">Status</td>
                        <td>:</td>
                        <td colspan="2"><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                    </tr>
                </table>
                    
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemTransactionItem" runat="server" OnNeedDataSource="grdItemTransactionItem_NeedDataSource"
        OnUpdateCommand="grdItemTransactionItem_UpdateCommand" OnDeleteCommand="grdItemTransactionItem_DeleteCommand" 
        OnInsertCommand="grdItemTransactionItem_InsertCommand" AutoGenerateColumns="False" GridLines="None" >
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ItemID" PageSize="10">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Item Unit" UniqueName="SRItemUnit"
                    SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth01" HeaderText="Jan"
                    UniqueName="QtyMonth01" SortExpression="QtyMonth01" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth02" HeaderText="Feb"
                    UniqueName="QtyMonth02" SortExpression="QtyMonth02" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth03" HeaderText="Mar"
                    UniqueName="QtyMonth03" SortExpression="QtyMonth03" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth04" HeaderText="Apr"
                    UniqueName="QtyMonth04" SortExpression="QtyMonth04" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth05" HeaderText="May"
                    UniqueName="QtyMonth05" SortExpression="QtyMonth05" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth06" HeaderText="Jun"
                    UniqueName="QtyMonth06" SortExpression="QtyMonth06" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth07" HeaderText="Jul"
                    UniqueName="QtyMonth07" SortExpression="QtyMonth07" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth08" HeaderText="Aug"
                    UniqueName="QtyMonth08" SortExpression="QtyMonth08" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth09" HeaderText="Sep"
                    UniqueName="QtyMonth09" SortExpression="QtyMonth09" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth10" HeaderText="Oct"
                    UniqueName="QtyMonth10" SortExpression="QtyMonth10" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth11" HeaderText="Nov"
                    UniqueName="QtyMonth11" SortExpression="QtyMonth11" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="QtyMonth12" HeaderText="Dec"
                    UniqueName="QtyMonth12" SortExpression="QtyMonth12" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="BudgetingDetailItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="BudgetingDetailItemCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
