<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ItemPickerListTemplate.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitTransaction.ItemPickerListTemplate" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%=grdListItem.UniqueID%>", "rebind:" + args.getDataKeyValue("TemplateNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="25%" valign="top">
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource" OnDeleteCommand="grdList_DeleteCommand">
                    <MasterTableView DataKeyNames="TemplateNo" ClientDataKeyNames="TemplateNo">
                        <Columns>
                            <telerik:GridBoundColumn DataField="TemplateNo" UniqueName="TemplateNo" SortExpression="TemplateNo"
                                HeaderText="Template No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                            <telerik:GridBoundColumn DataField="TemplateName" UniqueName="TemplateName" SortExpression="TemplateName"
                                HeaderText="Template Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this exam order template?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowSelected="RowSelected" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="3px" />
            </td>
            <td width="85%" valign="top">
                <telerik:RadGrid ID="grdListItem" runat="server" AutoGenerateColumns="False" GridLines="Both" ShowFooter="True">
                    <MasterTableView DataKeyNames="TemplateNo, SequenceNo" ClientDataKeyNames="TemplateNo, SequenceNo">
                        <Columns>
                            <telerik:GridBoundColumn DataField="TemplateNo" UniqueName="TemplateNo" Visible="false" />
                            <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemID" HeaderText="Item ID"
                                UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ItemName" HeaderText="Item Name"
                                UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Qty"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtChargeQuantity" runat="server" Width="85px" MinValue="0" DbValue='<%#Eval("ChargeQuantity")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
