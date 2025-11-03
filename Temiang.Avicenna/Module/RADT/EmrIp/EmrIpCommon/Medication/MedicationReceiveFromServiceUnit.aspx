<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveFromServiceUnit.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationReceiveFromServiceUnit"
    Title="Untitled Page" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%=grdDetail.UniqueID%>", "rebind:" + args.getDataKeyValue("TransactionNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Service Unit Transaction">
        <telerik:RadGrid ID="grdList" runat="server" ShowStatusBar="true" OnNeedDataSource="grdList_NeedDataSource"
            AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
                PageSize="10">
                <Columns>
                    <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Prescription No" UniqueName="TransactionNo"
                        SortExpression="TransactionNo">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                        SortExpression="TransactionDate">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                        UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room"
                        UniqueName="RoomName" SortExpression="RoomName">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnRowSelected="RowSelected" />
            </ClientSettings>
        </telerik:RadGrid>
    </cc:CollapsePanel>
    <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Request Item">
        <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None"
            OnNeedDataSource="grdDetail_NeedDataSource" OnPageIndexChanged="grdDetail_PageIndexChanged"
            AllowPaging="False" OnItemDataBound="grdDetail_OnItemDataBound">
            <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                <Columns>
                    <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName"/>
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                        UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                        UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Receive Qty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtQtyInput" runat="server" Width="80px" DbValue='<%#Eval("QtyInput")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderStyle-Width="200px" HeaderText="Consume Method" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadComboBox runat="server" ID="cboSRConsumeMethod" Width="100%" EmptyMessage="Select Unit Consume"
                                                 EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false">
                                <WebServiceSettings Method="ConsumeMethods" Path="~/WebService/ComboBoxDataService.asmx" />
                                <ClientItemTemplate>
                                    <div>
                                        <ul class="details">
                                            <li class="bold"><span>#= Text # </span></li>
                                            <li class="small"><span>Time: #= Attributes.TimeSequence #</span></li>
                                        </ul>
                                    </div>
                                </ClientItemTemplate>
                            </telerik:RadComboBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Qty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtConsumeQty" runat="server" Width="50px"  CssClass="RightAligned" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Unit" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadComboBox runat="server" ID="cboSRConsumeUnit" Width="100%" EmptyMessage="Select Unit Consume"
                                                 EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                                <WebServiceSettings Method="ConsumeUnits" Path="~/WebService/ComboBoxDataService.asmx" />
                            </telerik:RadComboBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderStyle-Width="170px" HeaderText="Start Schedule" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadDateTimePicker ID="txtStartDateTime" runat="server" Width="100%" SelectedDate='<%#Eval("StartDateTime")%>'
                                Culture="<%#AppConstant.DisplayFormat.DateCultureInfo%>" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Medication Consume" HeaderStyle-HorizontalAlign="Center" 
                                                ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadComboBox ID="cboSRMedicationConsume" runat="server" Width="100%"  />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="SRMedicationConsume" UniqueName="SRMedicationConsume" Visible="False"/>
                    <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <table width="100%">
            <tr>
                <td class="label">Receive Date</td>
                <td>
                    <telerik:RadDatePicker ID="txtReceiveDateTime" runat="server" Width="100px" />
                </td>
                <td></td>
            </tr>
        </table>
    </cc:CollapsePanel>
</asp:Content>
