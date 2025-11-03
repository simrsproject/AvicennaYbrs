<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterDialog.Master" CodeBehind="ARWriteOffPickList.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.Receivable.Adjustment.WriteOff.ARWriteOffPickList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%= grdInvoicesDetail.UniqueID %>", args.getDataKeyValue("InvoiceNo"));
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
                    <telerik:AjaxUpdatedControl ControlID="grdInvoicesDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdInvoicesDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInvoicesDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="15%" valign="top">
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource" AllowPaging="true">
                    <MasterTableView DataKeyNames="InvoiceNo" ClientDataKeyNames="InvoiceNo" PageSize="15">
                        <Columns>
                            <telerik:GridBoundColumn DataField="InvoiceNo" UniqueName="InvoiceNo" SortExpression="InvoiceNo"
                                HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowSelected="RowSelected" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="3px" />
            </td>
            <td width="85%" valign="top">
                <telerik:RadGrid ID="grdInvoicesDetail" runat="server" AutoGenerateColumns="False"
                    GridLines="Both">
                    <MasterTableView DataKeyNames="InvoiceNo, PaymentNo, InvoiceReferenceNo" ClientDataKeyNames="InvoiceNo, PaymentNo, InvoiceReferenceNo">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="InvoiceNo" UniqueName="InvoiceNo" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                                UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="InvoiceReferenceNo" UniqueName="InvoiceReferenceNo"
                                Visible="false" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                                UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="PatientID" HeaderText="Patient ID" UniqueName="PatientID"
                                HeaderStyle-Width="80px" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                                HeaderStyle-Width="80px" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                HeaderStyle-Width="170px" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceAmount" HeaderText="Amount"
                                UniqueName="BalanceAmount" SortExpression="BalanceAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="120px" HeaderText="Write Off Amount"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtPaymentAmount" runat="server" Width="95%" DbValue='<%#Eval("BalanceAmount")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
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

