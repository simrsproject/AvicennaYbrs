<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="DownPaymentList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.DownPaymentList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function viewHistory(regNo) {
                var oWnd = $find("<%= winHistory.ClientID %>");
                oWnd.setUrl('../PaymentReceive/PaymentReceiveHistory.aspx?rno=' + regNo);

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winHistory">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPaymenNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
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
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterMedicalNoD">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListD" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPatientNameD">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListD" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPaymentNoD">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListD" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSsnD">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListD" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListD">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListD" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Down Payment" PageViewID="pgDP" Selected="True" />
            <telerik:RadTab runat="server" Text="Deposit" PageViewID="pgPD" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDP" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" Text="Registration Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtOrderDate1" runat="server" Width="100px" />
                                                </td>
                                                <td>&nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtOrderDate2" runat="server" Width="100px" />
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
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                            <ItemTemplate>
                                <%# (this.IsUserAddAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsChasierCheckin").Equals(false) ? string.Empty
                                    : string.Format("<a href=\"DownPaymentDetail.aspx?md=new&regno={0}&cmno={1}&patid={2}&ut=\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                                                    DataBinder.Eval(Container.DataItem, "CashManagementNo"),
                                                                    DataBinder.Eval(Container.DataItem, "PatientID")))%>
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
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
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
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName" Visible="False">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                            SortExpression="BedID" Visible="false">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="PaymentNo,RegistrationNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# ((this.IsUserEditAble.Equals(false) || (DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) ? string.Empty :
                                            string.Format("<a href=\"DownPaymentDetail.aspx?md=edit&id={0}&regno={1}&cmno={2}&patid={3}&ut=\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "PaymentNo"), 
                                                                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                                                                    DataBinder.Eval(Container.DataItem, "CashManagementNo"),
                                                                                    DataBinder.Eval(Container.DataItem, "PatientID")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridHyperLinkColumn HeaderStyle-Width="120px" DataTextField="PaymentNo"
                                    DataNavigateUrlFields="PaymentUrl" HeaderText="Down Payment No" UniqueName="PaymentNo"
                                    SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate" HeaderText="Payment Date"
                                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PaymentTime" HeaderText="Payment Time"
                                    UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentReferenceNo" HeaderText="Payment Ref. No"
                                    UniqueName="PaymentReferenceNo" SortExpression="PaymentReferenceNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="TotalPaymentAmount" HeaderText="Total Payment"
                                    UniqueName="TotalPaymentAmount" SortExpression="TotalPaymentAmount" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="50px"/>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApproved" HeaderText="Approved"
                                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="UserName" HeaderText="Created By"
                                    UniqueName="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn />
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
        <telerik:RadPageView ID="pgPD" runat="server">
            <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblMedicalNoD" runat="server" Text="Medical No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtMedicalNoD" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterMedicalNoD" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterD_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right"></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientNameD" runat="server" Text="Patient Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPatientNameD" runat="server" Width="300px" MaxLength="150" />
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterPatientNameD" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterD_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right"></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPaymentNoD" runat="server" Text="Payment No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPaymentNoD" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterPaymentNoD" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterD_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right"></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSsnD" runat="server" Text="Ssn"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtSsnD" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterSsnD" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterD_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: right"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdListD" runat="server" OnNeedDataSource="grdListD_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                OnDetailTableDataBind="grdListD_DetailTableDataBind">
                <MasterTableView DataKeyNames="PatientID" ClientDataKeyNames="PatientID"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                            <ItemTemplate>
                                <%# (this.IsUserAddAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsChasierCheckin").Equals(false) ? string.Empty
                                    : string.Format("<a href=\"DownPaymentDetail.aspx?md=new&regno={0}&cmno={1}&patid={2}&ut=\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                                                    string.Empty, 
                                                                    DataBinder.Eval(Container.DataItem, "CashManagementNo"),
                                                                    DataBinder.Eval(Container.DataItem, "PatientID")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="PaymentNo,PatientID" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# ((this.IsUserEditAble.Equals(false) || (DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) ? string.Empty :
                                            string.Format("<a href=\"DownPaymentDetail.aspx?md=edit&id={0}&regno={1}&cmno={2}&patid={3}&ut=\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "PaymentNo"), 
                                                                                    string.Empty, 
                                                                                    DataBinder.Eval(Container.DataItem, "CashManagementNo"),
                                                                                    DataBinder.Eval(Container.DataItem, "PatientID")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridHyperLinkColumn HeaderStyle-Width="120px" DataTextField="PaymentNo"
                                    DataNavigateUrlFields="PaymentUrl" HeaderText="Down Payment No" UniqueName="PaymentNo"
                                    SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate" HeaderText="Payment Date"
                                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PaymentTime" HeaderText="Payment Time"
                                    UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentReferenceNo" HeaderText="Payment Ref. No"
                                    UniqueName="PaymentReferenceNo" SortExpression="PaymentReferenceNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="TotalPaymentAmount" HeaderText="Total Payment"
                                    UniqueName="TotalPaymentAmount" SortExpression="TotalPaymentAmount" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="50px"/>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVisiteDownPayment" HeaderText="Visite Package"
                                    UniqueName="IsVisiteDownPayment" SortExpression="IsVisiteDownPayment" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApproved" HeaderText="Approved"
                                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="UserName" HeaderText="Created By"
                                    UniqueName="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn />
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
