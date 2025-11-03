<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    Codebehind="BudgetingItemApprovalList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Budgeting.BudgetingItemApprovalList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                // ??
            }

            function confirmRejectionReason() {
                var reason = prompt('Please enter the reason', '');

                if (reason !== null) {
                    var hfid = '<%=hfRejectionReason.ClientID %>';
                    //console.log(hfid);
                    document.getElementById(hfid).value = reason;
                    //console.log(document.getElementById(hfid).value);
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="New" Value="new" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboYear" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left;">
                            <asp:ImageButton ID="btnFilterYear" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" 
        OnItemCommand="grdList_ItemCommand" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="BudgetingNo,Revision,ChartOfAccountID,ItemID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" /> 
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ChartOfAccountName" HeaderText="Chart Of Account"
                    UniqueName="ChartOfAccountName" SortExpression="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" /> 
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" /> 
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ItemName" HeaderText="Item Name"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" /> 
                
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Qty" HeaderText="Qty"
                    UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Right"  /> 
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Item Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" /> 
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" >
                    <HeaderStyle HorizontalAlign="Right"  /> 
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn UniqueName="IsDraft" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="iBtnApproval" runat="server" CommandName="Approval"
                            OnClientClick="if(!confirm('Are you sure want to approve?')){ return false; };"
                            ImageUrl='<%# "../../../../Images/Toolbar/post16.png" %>'
                            Visible='<%# !(bool)DataBinder.Eval(Container.DataItem, "IsAssetApproved") && !(bool)DataBinder.Eval(Container.DataItem, "IsAssetRejected") %>'
                            ToolTip='Approval' />
                        <asp:ImageButton ID="iBtnReject" runat="server" CommandName="Reject" 
                            OnClientClick="return confirmRejectionReason();"
                            ImageUrl='<%# "../../../../Images/Toolbar/row_delete16.png" %>'
                            Visible='<%# !(bool)DataBinder.Eval(Container.DataItem, "IsAssetApproved") && !(bool)DataBinder.Eval(Container.DataItem, "IsAssetRejected") %>'
                            ToolTip='Reject' />
                        <asp:Label ID="lblApprove" runat="server" Visible='<%# (bool)DataBinder.Eval(Container.DataItem, "IsAssetApproved")%>' Text="Approved" ForeColor="Green"></asp:Label>
                        <asp:Label ID="lblReject" runat="server" Visible='<%# (bool)DataBinder.Eval(Container.DataItem, "IsAssetRejected")%>' Text="Rejected" ForeColor="Red"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RejectNotes" HeaderText="Rejection Reason"
                    UniqueName="RejectNotes" SortExpression="RejectNotes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" /> 
            </Columns>
        </MasterTableView>
        <filtermenu>
        </filtermenu>
        <clientsettings enablerowhoverstyle="true">
            <resizing allowcolumnresize="True" />
            <selecting allowrowselect="True" />
        </clientsettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="hfRejectionReason" runat="server" />
</asp:Content>
