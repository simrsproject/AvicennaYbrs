<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="TransfusionMonitoringList.aspx.cs" Inherits="Temiang.Avicenna.Module.BloodBank.TransfusionMonitoringList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winHistory">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterBloodBankNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterBagNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterReceivedDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransfusionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransfusionList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransfusionList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdOutstandingList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOutstandingList" />
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
                                <asp:Label ID="Label1" runat="server" Text="Request Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRequestDate1" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRequestDate2" runat="server" Width="100px" />
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
                                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterTransactionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBloodBankNo" runat="server" Text="Blood Bank / PDUT No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBloodBankNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterBloodBankNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td style="text-align: right"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBagNo" runat="server" Text="Bag No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBagNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterBagNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
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
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding List" PageViewID="pgOutstanding"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Transfusion List" PageViewID="pgTransfusion">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOutstanding" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReceivedDate" runat="server" Text="Received Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtReceived" runat="server" Width="100px" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterReceivedDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td style="text-align: right"></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdOutstandingList" runat="server" OnNeedDataSource="grdOutstandingList_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                OnDetailTableDataBind="grdOutstandingList_DetailTableDataBind" OnItemDataBound="grdOutstandingList_ItemDataBound">
                <MasterTableView Name="master" DataKeyNames="TransactionNo, BagNo" ClientDataKeyNames="TransactionNo, BagNo"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="new" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# ((this.IsUserEditAble.Equals(false)) ? string.Empty :
                                         string.Format("<a href=\"TransfusionMonitoringDetail.aspx?md=edit&id={0}&bagno={1}&regno={2}\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                         DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "BagNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BagNo" HeaderText="Bag No"
                            UniqueName="BagNo" SortExpression="BagNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ReceivedDate" HeaderText="Received Date" UniqueName="ReceivedDate"
                            SortExpression="ReceivedDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ReceivedTime" HeaderText="Received Time"
                            UniqueName="ReceivedTime" SortExpression="ReceivedTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BloodGroupReceived"
                            HeaderText="Blood Group" UniqueName="BloodGroupReceived" SortExpression="BloodGroupReceived"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="140px" />
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
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName">
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FromRefer" UniqueName="FromRefer" Visible="False" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="grdDetail" DataKeyNames="TransactionNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RequestDate" HeaderText="Request Date"
                                    UniqueName="RequestDate" SortExpression="RequestDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RequestTime" HeaderText="Request Time"
                                    UniqueName="RequestTime" SortExpression="RequestTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BloodBankNo" HeaderText="Blood Bank No"
                                    UniqueName="BloodBankNo" SortExpression="BloodBankNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PdutNo" HeaderText="PDUT No"
                                    UniqueName="PdutNo" SortExpression="PdutNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="BloodType" HeaderText="Blood Type"
                                    UniqueName="BloodType" SortExpression="BloodType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="BloodRhesus" HeaderText="Rhesus"
                                    UniqueName="BloodRhesus" SortExpression="BloodRhesus" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BloodGroup" HeaderText="Blood Group Request"
                                    UniqueName="BloodGroup" SortExpression="BloodGroup" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="QtyBagRequest" HeaderText="Qty Bag"
                                    UniqueName="QtyBagRequest" SortExpression="QtyBagRequest" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="QtyBagRequest" HeaderText="Volume (ML/CC)"
                                    UniqueName="VolumeBag" SortExpression="VolumeBag" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
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
        <telerik:RadPageView ID="pgTransfusion" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTransfusionDate" runat="server" Text="Transfusion Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtTransfusionDate" runat="server" Width="100px" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterTransfusionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td style="text-align: right"></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdTransfusionList" runat="server" OnNeedDataSource="grdTransfusionList_NeedDataSource"
                GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
                OnDetailTableDataBind="grdTransfusionList_DetailTableDataBind" OnItemDataBound="grdTransfusionList_ItemDataBound">
                <MasterTableView Name="master" DataKeyNames="TransactionNo, BagNo" ClientDataKeyNames="TransactionNo, BagNo"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# ((this.IsUserEditAble.Equals(false)) ? string.Empty :
                                                                             string.Format("<a href=\"TransfusionMonitoringDetail.aspx?md=edit&id={0}&bagno={1}&regno={2}\"><img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                         DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "BagNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"TransfusionMonitoringDetail.aspx?md=view&id={0}&bagno={1}&regno={2}\"><img src=\"../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                         DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "BagNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="120px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="TransfusionUrl" HeaderText="Transaction No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BagNo" HeaderText="Bag No"
                            UniqueName="BagNo" SortExpression="BagNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TransfusionStartDateTime" HeaderText="Transfusion Start Date"
                            UniqueName="TransfusionStartDateTime" SortExpression="TransfusionStartDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TransfusionEndDateTime" HeaderText="Transfusion End Date"
                            UniqueName="TransfusionEndDateTime" SortExpression="TransfusionEndDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BloodGroupReceived"
                            HeaderText="Blood Group" UniqueName="BloodGroupReceived" SortExpression="BloodGroupReceived"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="140px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName">
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FromRefer" UniqueName="FromRefer" Visible="False" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="grdDetail" DataKeyNames="TransactionNo, BagNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RequestDate" HeaderText="Request Date"
                                    UniqueName="RequestDate" SortExpression="RequestDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RequestTime" HeaderText="Request Time"
                                    UniqueName="RequestTime" SortExpression="RequestTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BloodBankNo" HeaderText="Blood Bank No"
                                    UniqueName="BloodBankNo" SortExpression="BloodBankNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PdutNo" HeaderText="PDUT No"
                                    UniqueName="PdutNo" SortExpression="PdutNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="BloodType" HeaderText="Blood Type"
                                    UniqueName="BloodType" SortExpression="BloodType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="BloodRhesus" HeaderText="Rhesus"
                                    UniqueName="BloodRhesus" SortExpression="BloodRhesus" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BloodGroup" HeaderText="Blood Group Request"
                                    UniqueName="BloodGroup" SortExpression="BloodGroup" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="QtyBagRequest" HeaderText="Qty Bag"
                                    UniqueName="QtyBagRequest" SortExpression="QtyBagRequest" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="QtyBagRequest" HeaderText="Volume (ML/CC)"
                                    UniqueName="VolumeBag" SortExpression="VolumeBag" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                                <telerik:GridBoundColumn DataField="ReceivedDate" HeaderText="Received Date" UniqueName="ReceivedDate"
                                    SortExpression="ReceivedDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ReceivedTime" HeaderText="Received Time"
                                    UniqueName="ReceivedTime" SortExpression="ReceivedTime" HeaderStyle-HorizontalAlign="Center"
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
