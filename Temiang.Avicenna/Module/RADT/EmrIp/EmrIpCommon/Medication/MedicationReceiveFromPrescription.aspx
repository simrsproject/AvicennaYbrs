<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveFromPrescription.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationReceiveFromPrescription"
    Title="Untitled Page" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%=grdDetail.UniqueID%>", "rebind:" + args.getDataKeyValue("PrescriptionNo"));
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
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Prescription">
        <telerik:RadGrid ID="grdList" runat="server" ShowStatusBar="true" OnNeedDataSource="grdList_NeedDataSource"
            AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="PrescriptionNo" ClientDataKeyNames="PrescriptionNo"
                PageSize="10">
                <Columns>
                    <telerik:GridBoundColumn DataField="PrescriptionNo" HeaderText="Prescription No" UniqueName="PrescriptionNo"
                        SortExpression="PrescriptionNo">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="PrescriptionDate" HeaderText="Date" UniqueName="PrescriptionDate"
                        SortExpression="PrescriptionDate">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician"
                        UniqueName="ParamedicName" SortExpression="ParamedicName">
                        <HeaderStyle HorizontalAlign="Left" Width="300px" />
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
            OnNeedDataSource="grdDetail_NeedDataSource" OnPageIndexChanged="grdDetail_PageIndexChanged" OnItemDataBound="grdDetail_OnItemDataBound"
            AllowPaging="False">
            <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                <Columns>
                    <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" Visible="false" />
                    <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                        <ItemTemplate>
                            <asp:Label ID="lblItemName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemName") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateResultQty" HeaderStyle-Width="75px"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblResultQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemQtyInString") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateSRItemUnit" HeaderStyle-Width="50px"
                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblSRItemUnit" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRConsumeMethodName"
                        HeaderText="Consume Method" UniqueName="SRConsumeMethodName" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="75px" DataField="ConsumeQty" HeaderText="Dosage Qty"
                        UniqueName="ConsumeQty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRConsumeUnit" HeaderText="Consume<br/>Unit"
                        UniqueName="SRConsumeUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Receive Qty<br/>(in consume unit)" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtQtyInput" runat="server" Width="80px" DbValue='<%#Eval("QtyInput")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="180px" HeaderText="Start Schedule" HeaderStyle-HorizontalAlign="Center"
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
                    <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
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
    <asp:Button runat="server" ID="btnReImport" Text="Reimport" OnClick="btnReImport_Click" />
</asp:Content>
