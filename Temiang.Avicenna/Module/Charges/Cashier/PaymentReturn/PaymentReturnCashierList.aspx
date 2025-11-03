<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="PaymentReturnCashierList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PaymentReturnCashierList" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../JavaScript/jQuery.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowDelete(payNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'delete|' + payNo);
            }
            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListPayment" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListPayment" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPaymenNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListPayment" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblServiceUnitName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListPayment" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListPayment" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Payment Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtPaymentDate1" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtPaymentDate2" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPaymentNo" runat="server" Text="Payment No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterPaymenNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding List" PageViewID="pgOs" Selected="True" />
            <telerik:RadTab runat="server" Text="Payment List" PageViewID="pgList" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOs" runat="server" Selected="true">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                OnDetailTableDataBind="grdList_DetailTableDataBind">
                <MasterTableView DataKeyNames="PaymentNo" ClientDataKeyNames="PaymentNo"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Group" HeaderText="Payment Date " FormatString="{0:dd-MMM-yyyy}"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Group" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# ((this.IsUserEditAble.Equals(false)) || DataBinder.Eval(Container.DataItem, "IsChasierCheckin").Equals(false) ? string.Empty :
                                            string.Format("<a href=\"PaymentReturnDetail.aspx?md=view&id={0}&regno={1}&cmno={2}&patid={3}&utype=c\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "PaymentNo"), 
                                                                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                                                                    DataBinder.Eval(Container.DataItem, "CashManagementNo"),
                                                                                    DataBinder.Eval(Container.DataItem, "PatientID")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="PaymentNo" HeaderText="Payment No"
                            UniqueName="PaymentNo" SortExpression="PaymentNo">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PaymentTime" HeaderText="Payment Time"
                            UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                            SortExpression="RegistrationDate">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName">
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="TotalPaymentAmount"
                            HeaderText="Total Payment" UniqueName="TotalPaymentAmount" SortExpression="TotalPaymentAmount"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="PaymentNo, SequenceNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="PaymentTypeName" HeaderText="Payment Type" UniqueName="PaymentTypeName"
                                    SortExpression="PaymentTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="PaymentMethodName" HeaderText="Payment Method"
                                    UniqueName="PaymentMethodName" SortExpression="PaymentMethodName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AmountReceived" HeaderText="Received"
                                    UniqueName="AmountReceived" SortExpression="AmountReceived" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Change" HeaderText="Change"
                                    UniqueName="Change" SortExpression="Change" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RoundingAmount" HeaderText="Rounding"
                                    UniqueName="RoundingAmount" SortExpression="RoundingAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Return"
                                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <ExpandCollapseColumn Visible="True" />
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgList" runat="server">
            <telerik:RadGrid ID="grdListPayment" runat="server" OnNeedDataSource="grdListPayment_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                OnDetailTableDataBind="grdListPayment_DetailTableDataBind">
                <MasterTableView DataKeyNames="PaymentNo" ClientDataKeyNames="PaymentNo"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Group" HeaderText="Approved Date " FormatString="{0:dd-MMM-yyyy}"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Group" SortOrder="Descending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                  <%# ((this.IsUserEditAble.Equals(false)) ? string.Empty :
                                            string.Format("<a href=\"PaymentReturnDetail.aspx?md=view&id={0}&regno={1}&cmno={2}&patid={3}&utype=c\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "PaymentNo"), 
                                                                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                                                                    DataBinder.Eval(Container.DataItem, "CashManagementNo"),
                                                                                    DataBinder.Eval(Container.DataItem, "PatientID")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="PaymentNo" HeaderText="Payment No"
                            UniqueName="PaymentNo" SortExpression="PaymentNo">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ApprovedTime" HeaderText="Approved Time"
                            UniqueName="ApprovedTime" SortExpression="ApprovedTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                            SortExpression="RegistrationDate">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName">
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="TotalPaymentAmount"
                            HeaderText="Total Payment" UniqueName="TotalPaymentAmount" SortExpression="TotalPaymentAmount"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="PaymentNo, SequenceNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="PaymentTypeName" HeaderText="Payment Type" UniqueName="PaymentTypeName"
                                    SortExpression="PaymentTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="PaymentMethodName" HeaderText="Payment Method"
                                    UniqueName="PaymentMethodName" SortExpression="PaymentMethodName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AmountReceived" HeaderText="Received"
                                    UniqueName="AmountReceived" SortExpression="AmountReceived" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Change" HeaderText="Change"
                                    UniqueName="Change" SortExpression="Change" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RoundingAmount" HeaderText="Rounding"
                                    UniqueName="RoundingAmount" SortExpression="RoundingAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                    FooterStyle-HorizontalAlign="right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Return"
                                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <ExpandCollapseColumn Visible="True" />
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