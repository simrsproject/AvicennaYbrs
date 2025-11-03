<%@  Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ItemConsumptionPackage.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ItemConsumptionPackage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function edit(transNo, seqNo, itemID) {
                var oWnd = $find("<%= winCharges.ClientID %>");
                oWnd.setUrl('ItemConsumptionPackageDetail.aspx?trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + "&unit=" + '<%= Request.QueryString["unit"] %>');
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.rebind != null)
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
            }
            
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="200px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winCharges">
    </telerik:RadWindow>
    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
        OnNeedDataSource="grdList_NeedDataSource" OnDeleteCommand="grdList_DeleteCommand"
        OnInsertCommand="grdList_InsertCommand">
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="TransactionNo, SequenceNo, DetailItemID">
            <Columns>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" Visible="False">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"edit('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>", DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "DetailItemID"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" SortExpression="TransactionNo"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="DetailItemID" UniqueName="DetailItemID" SortExpression="DetailItemID"
                    HeaderText="Item ID" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" SortExpression="ItemName"
                    HeaderText="Item Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn DataField="Qty" UniqueName="Qty" SortExpression="Qty"
                    HeaderText="Qty" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderText="Qty Realization" UniqueName="QtyRealizationText"
                    HeaderStyle-HorizontalAlign="center">
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <telerik:RadNumericTextBox runat="server" ID="txtQtyRealization" Width="80px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "QtyRealization")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SRItemUnit" UniqueName="SRItemUnit" SortExpression="SRItemUnit"
                    HeaderText="Unit" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn DataField="IsPackage" UniqueName="IsPackage" SortExpression="IsPackage"
                    Visible="false" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ItemConsumptionPackageEntry.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemConsumptionPackageEntryEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
