<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="BloodReceivedList.aspx.cs" Inherits="Temiang.Avicenna.Module.BloodBank.BloodReceivedList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winHistory">
    </telerik:RadWindow>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoEditUrl(id, regno, bg) {
                var url = 'BloodReceivedDetail.aspx?md=edit&id=' + id + '&regno=' + regno + '&bg=' + bg + '&type=<%= Page.Request.QueryString["type"] %>';
                window.location.href = url;
            }

            function gotoViewUrl(id, regno, bg) {
                var url = 'BloodReceivedDetail.aspx?md=view&id=' + id + '&regno=' + regno + '&bg=' + bg+ '&type=<%= Page.Request.QueryString["type"] %>';
                window.location.href = url;
            }

            function openRpt() {
                var oWnd = $find('<%=winPrint.ClientID%>');
                oWnd.SetUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                oWnd.Show();
                //oWnd.Maximize();
                oWnd.add_pageLoad(onClientPageLoad);
                return;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterBloodBankNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
                ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
                Modal="true">
            </telerik:RadWindow>
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        OnDetailTableDataBind="grdList_DetailTableDataBind" OnItemCommand="grdList_ItemCommand">
        <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
            GroupLoadMode="Client">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (this.IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(false)) ? string.Empty :
                                (string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SRBloodGroupRequest")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsValidatedByCasemix").Equals(false)) ? (string.Format("<a href=\"#\"><img src=\"../../../Images/Toolbar/lock16.png\" border=\"0\" title=\"Need validation by Casemix\" /></a>")) :
                                (string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SRBloodGroupRequest")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridHyperLinkColumn HeaderStyle-Width="120px" DataTextField="TransactionNo"
                    DataNavigateUrlFields="ReceivedUrl" HeaderText="Transaction No" UniqueName="TransactionNo"
                    SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="RequestDate" HeaderText="Request Date" UniqueName="RequestDate"
                    SortExpression="RequestDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="RequestTime" HeaderText="Request Time"
                    UniqueName="RequestTime" SortExpression="RequestTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BloodBankNo" HeaderText="Blood Bank No"
                    UniqueName="BloodBankNo" SortExpression="BloodBankNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PdutNo" HeaderText="PDUT No"
                    UniqueName="PdutNo" SortExpression="PdutNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
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
                <telerik:GridTemplateColumn UniqueName="PrintLblRssmcb" HeaderStyle-Width="35px"
                    ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnLblRSSMCB" runat="server" CommandName="PatientSticker"
                            ToolTip='Patient Sticker' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'>
                            <img src="../../../Images/Toolbar/print16.png" border="0" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="grdDetail" DataKeyNames="TransactionNo" AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="BloodType" HeaderText="Blood Type"
                            UniqueName="BloodType" SortExpression="BloodType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="BloodRhesus" HeaderText="Rhesus"
                            UniqueName="BloodRhesus" SortExpression="BloodRhesus" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BloodGroup" HeaderText="Blood Group"
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
                <telerik:GridTableView DataKeyNames="TransactionNo,BagNo" Name="grdDetailItem" Width="100%"
                    AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="150px" DataField="BagNo" HeaderText="Bag No" UniqueName="BagNo"
                            SortExpression="BagNo">
                            <ItemTemplate>
                                <%# string.Format("{0}", DataBinder.Eval(Container.DataItem, "BagNo"))%><br />
                                <i>ED:&nbsp;<%# DataBinder.Eval(Container.DataItem, "ExpiredDateTime", "{0:dd-MMM-yyyy HH:mm}")%></i>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VolumeBag"
                            HeaderText="Volume (ML/CC)" UniqueName="VolumeBag" SortExpression="VolumeBag"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BloodBagTemperature"
                            HeaderText="Blood Bag Temperature (°C)" UniqueName="BloodBagTemperature" SortExpression="BloodBagTemperature"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn DataField="ReceivedDate" HeaderText="Submitted Date" UniqueName="ReceivedDate"
                            SortExpression="ReceivedDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="55px" DataField="ReceivedTime" HeaderText="Time"
                            UniqueName="ReceivedTime" SortExpression="ReceivedTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ExaminerByUserName" HeaderText="Submitted By"
                            UniqueName="ExaminerByUserName" SortExpression="ExaminerByUserName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="UnitOfficer" HeaderText="Received By"
                            UniqueName="UnitOfficer" SortExpression="UnitOfficer" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsProceedToTransfusion"
                            HeaderText="Proceed To Transfusion" UniqueName="IsProceedToTransfusion" SortExpression="IsProceedToTransfusion"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="BloodBagNotes" HeaderText="Notes" UniqueName="BloodBagNotes"
                            SortExpression="BloodBagNotes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BloodBagStatusName"
                            HeaderText="Blood Status" UniqueName="BloodBagStatusName" SortExpression="BloodBagStatusName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsVoid"
                            HeaderText="Void" UniqueName="IsVoid" SortExpression="IsVoid"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
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
</asp:Content>
