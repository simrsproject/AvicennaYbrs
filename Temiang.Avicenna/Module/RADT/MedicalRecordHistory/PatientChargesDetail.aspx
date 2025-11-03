<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientChargesDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientChargesDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function viewCompList(transNo, seqNo) {
                var oWnd = $find("<%= winCompList.ClientID %>");
                oWnd.setUrl('PatientChargesDetailItemComp.aspx?tno=' + transNo + '&sno=' + seqNo);

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function gotoPrintJoNotes(transNo) {
                //alert("empty");
                __doPostBack("<%= grdList.UniqueID %>", 'printJoNotes|' + transNo);
            }

            function openLabelPrint(transNo, type) {
                var oWnd = $find("<%= winPrintLbl.ClientID %>");
                oWnd.SetUrl("../../Charges/ServiceUnit/FilmConsumption/LabelPrint.aspx?tno=" + transNo + "&type=" + type + "&init=rsch");
                oWnd.show();
            }

            function onClientCloseLabelPrint(oWnd, args) {
                if (oWnd.argument && oWnd.argument.print != null) {
                    var oWnd = $find("<%= winPrint.ClientID %>");
                    oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                    oWnd.setSize(800, 450);
                    oWnd.show();
                }
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="200px" Height="200px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winCompList">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrintLbl" Animation="None" Width="600px" Height="300px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="True" Modal="true" OnClientClose="onClientCloseLabelPrint" Title="Print Label">
    </telerik:RadWindow>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="304px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    OnPreRender="grdList_PreRender" OnDetailTableDataBind="grdList_DetailTableDataBind"
                    AutoGenerateColumns="false">
                    <MasterTableView Name="gridMain" DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
                        GroupLoadMode="Client">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="RegistrationNo" HeaderText="Registration No ">
                                    </telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="RegDateRegNo" SortOrder="Descending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PatientID" HeaderText="Patient ID"
                                UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                            <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="TransactionNo" HeaderText="Transaction No"
                                UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="TransactionDate"
                                HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="ToServiceUnitName" HeaderText="Service Unit"
                                UniqueName="ToServiceUnitName" SortExpression="ToServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                                UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsOrder" HeaderText="Order"
                                UniqueName="IsOrder" SortExpression="IsOrder" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                <ItemTemplate>
                                    <%# (DataBinder.Eval(Container.DataItem, "IsOrder").Equals(false) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"gotoPrintJoNotes('{0}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print Job Order Notes\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                <ItemTemplate>
                                    <%# (DataBinder.Eval(Container.DataItem, "IsOrder").Equals(false) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"openLabelPrint('{0}','1'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" title=\"Print Label\" /></a>",
                                     DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="TransactionNo, SequenceNo" Name="grdDetail"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ParamedicCollectionName" HeaderText="Paramedic"
                                        UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="ChargeQuantity" HeaderText="Qty"
                                        UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRItemUnit" HeaderText="Unit"
                                        UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Price" HeaderText="Price"
                                        UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="DiscountAmount" HeaderText="Disc."
                                        UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="CitoAmount" HeaderText="Cito"
                                        UniqueName="CitoAmount" SortExpression="CitoAmount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Total" HeaderText="Total"
                                        UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApprove" HeaderText="Approved"
                                        UniqueName="IsApprove" SortExpression="IsApprove" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsVoid" HeaderText="Void"
                                        UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsOrderRealization"
                                        HeaderText="Realization" UniqueName="IsOrderRealization" SortExpression="IsOrderRealization"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="LastUpdateByUserID"
                                        HeaderText="User" UniqueName="User" SortExpression="User" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                                        <ItemTemplate>
                                            <a href="#" onclick="viewCompList('<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>', '<%# DataBinder.Eval(Container.DataItem, "SequenceNo") %>'); return false;">
                                                <img src="../../../Images/Toolbar/details16.png" border="0" title="Transaction Item Component List" /></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPackage" HeaderText="Package"
                                        UniqueName="IsPackage" SortExpression="IsPackage" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" Visible="True" />
                                </Columns>
                                <DetailTables>
                                    <telerik:GridTableView DataKeyNames="TransactionNo, SequenceNo" Name="grdDetailPackage"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="ParamedicCollectionName" HeaderText="Paramedic"
                                                UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="ChargeQuantity" HeaderText="Qty"
                                                UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRItemUnit" HeaderText="Unit"
                                                UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Price" HeaderText="Price"
                                                UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="DiscountAmount" HeaderText="Disc."
                                                UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="CitoAmount" HeaderText="Cito"
                                                UniqueName="CitoAmount" SortExpression="CitoAmount" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="Total" HeaderText="Total"
                                                UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApprove" HeaderText="Approve"
                                                UniqueName="IsApprove" SortExpression="IsApprove" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                                UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsOrderRealization"
                                                HeaderText="Realization" UniqueName="IsOrderRealization" SortExpression="IsOrderRealization"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="LastUpdateByUserID"
                                                HeaderText="User" UniqueName="User" SortExpression="User" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                                                <ItemTemplate>
                                                    <a href="#" onclick="viewCompList('<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>', '<%# DataBinder.Eval(Container.DataItem, "SequenceNo") %>'); return false;">
                                                        <img src="../../../Images/Toolbar/details16.png" border="0" alt="Transaction Item Component List" /></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPackage" HeaderText="Package"
                                                UniqueName="IsPackage" SortExpression="IsPackage" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                                        </Columns>
                                    </telerik:GridTableView>
                                </DetailTables>
                            </telerik:GridTableView>
                        </DetailTables>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="True" AllowExpandCollapse="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
