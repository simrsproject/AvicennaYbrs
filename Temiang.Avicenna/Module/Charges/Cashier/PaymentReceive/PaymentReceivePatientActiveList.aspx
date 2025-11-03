<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="PaymentReceivePatientActiveList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PaymentReceivePatientActiveList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function viewHistory(regNo) {
                var oWnd = $find("<%= winHistory.ClientID %>");
                oWnd.setUrl('PaymentReceiveHistory.aspx?rno=' + regNo);

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
            function rowDelete(payNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'delete|' + payNo);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winHistory">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDateActive">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListActive" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNoActive">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListActive" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnitIdActive">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListActive" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientNameActive">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListActive" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <script type="text/javascript">
        function onClientTabSelected(sender, eventArgs) {
            var tabIndex = eventArgs.get_tab().get_index();

            switch (tabIndex) {
                case 0:
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                    break;

            }
        }
    </script>

    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop" OnClientTabSelected="onClientTabSelected">
        <Tabs>
            <telerik:RadTab runat="server" Text="Patient Active" PageViewID="pg1" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="All Patient" PageViewID="pg2">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pg1" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationDateActive" runat="server" Text="Registration Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtRegistrationDateActive" runat="server" Width="100px" Enabled="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterRegistrationDateActive" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterActive_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationNoActive" runat="server" Text="Registration No / Medical No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtRegistrationNoActive" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterRegistrationNoActive" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterActive_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblServiceUnitIdActive" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboServiceUnitIdActive" Width="304px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterServiceUnitIdActive" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterActive_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientNameActive" runat="server" Text="Patient Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPatientNameActive" runat="server" Width="300px" MaxLength="150" />
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterPatientNameActive" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterActive_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdListActive" runat="server" OnNeedDataSource="grdListActive_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                OnDetailTableDataBind="grdListActive_DetailTableDataBind">
                <MasterTableView DataKeyNames="RegistrationNo" ClientDataKeyNames="RegistrationNo"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit ">
                                </telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                            <ItemTemplate>
                                <%# (this.IsUserAddAble.Equals(false) ? string.Empty
                            : string.Format("<a href=\"PaymentReceiveDetail.aspx?md=new&regno={0}&pc={1}\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), Page.Request.QueryString["pc"]))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
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
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName">
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <a href="#" onclick="viewHistory('<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'); return false;">
                                    <img src="../../../../Images/Toolbar/details16.png" border="0" alt="Payment List" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="PaymentNo,RegistrationNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# ((this.IsUserEditAble.Equals(false) || (DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) ? string.Empty :
                                    string.Format("<a href=\"PaymentReceiveDetail.aspx?md=edit&id={0}&regno={1}&pc={2}\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "PaymentNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), Page.Request.QueryString["pc"]))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridHyperLinkColumn HeaderStyle-Width="120px" DataTextField="PaymentNo"
                                    DataNavigateUrlFields="PaymentUrl" HeaderText="Payment No" UniqueName="PaymentNo"
                                    SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PaymentTime" HeaderText="Payment Time"
                                    UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn DataField="TotalPaymentAmount" HeaderText="Total Payment"
                                    UniqueName="TotalPaymentAmount" SortExpression="TotalPaymentAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApproved" HeaderText="Approved"
                                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="UserName" HeaderText="Created By"
                                    UniqueName="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(false) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"rowDelete('{0}'); return false;\">{1}</a>",
                                                  DataBinder.Eval(Container.DataItem, "PaymentNo"),
                                                                            "<img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" alt=\"Clear DP\" />"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
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
        <telerik:RadPageView ID="pg2" runat="server">
            <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="304px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                OnDetailTableDataBind="grdList_DetailTableDataBind">
                <MasterTableView DataKeyNames="RegistrationNo" ClientDataKeyNames="RegistrationNo"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit ">
                                </telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                            <ItemTemplate>
                                <%# (this.IsUserAddAble.Equals(false) ? string.Empty
                            : string.Format("<a href=\"PaymentReceiveDetail.aspx?md=new&regno={0}&pc={1}\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                                                                                DataBinder.Eval(Container.DataItem, "RegistrationNo"), string.Empty))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
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
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName">
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <a href="#" onclick="viewHistory('<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'); return false;">
                                    <img src="../../../../Images/Toolbar/details16.png" border="0" alt="Payment List" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="PaymentNo,RegistrationNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# ((this.IsUserEditAble.Equals(false) || (DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) ? string.Empty :
                                    string.Format("<a href=\"PaymentReceiveDetail.aspx?md=edit&id={0}&regno={1}&pc={2}\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "PaymentNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), string.Empty))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridHyperLinkColumn HeaderStyle-Width="120px" DataTextField="PaymentNo"
                                    DataNavigateUrlFields="PaymentUrl" HeaderText="Payment No" UniqueName="PaymentNo"
                                    SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate" HeaderText="Payment Date"
                                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PaymentTime" HeaderText="Payment Time"
                                    UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn DataField="TotalPaymentAmount" HeaderText="Total Payment"
                                    UniqueName="TotalPaymentAmount" SortExpression="TotalPaymentAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApproved" HeaderText="Approved"
                                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="UserName" HeaderText="Created By"
                                    UniqueName="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(false) ? string.Empty :
                                            string.Format("<a href=\"#\" onclick=\"rowDelete('{0}'); return false;\">{1}</a>",
                                                  DataBinder.Eval(Container.DataItem, "PaymentNo"),
                                                                            "<img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" alt=\"Clear DP\" />"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
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
