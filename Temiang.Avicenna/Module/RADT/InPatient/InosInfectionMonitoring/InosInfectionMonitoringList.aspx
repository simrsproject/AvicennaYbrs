<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="InosInfectionMonitoringList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.InosInfectionMonitoringList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoGroupNewUrl(roomid) {
                var url = 'InosInfectionMonitoringGroupDetail.aspx?md=edit&id=' + roomid;
                window.location.href = url;
            }
            function gotoNewUrl(regno, roomid) {
                var url = 'InosInfectionMonitoringDetail.aspx?md=new&id=0&regno=' + regno + '&roomid=' + roomid;
                window.location.href = url;
            }
            function gotoViewUrl(id, regno, roomid) {
                var url = 'InosInfectionMonitoringDetail.aspx?md=view&id=' + id + '&regno=' + regno + '&roomid=' + roomid;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRoom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
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
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboRoomID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterRoom" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
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
        AllowSorting="true" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView Name="master" DataKeyNames="ServiceUnitID, RoomID" ClientDataKeyNames="ServiceUnitID, RoomID"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans2" HeaderText="">
                    <ItemTemplate>
                        <%# (this.IsUserAddAble.Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"gotoGroupNewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                                                                                                                                                                                DataBinder.Eval(Container.DataItem, "RoomID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RoomID" HeaderText="RoomID" UniqueName="RoomID" SortExpression="RoomID">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room Name" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="RoomID, RegistrationNo" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="new" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoNewUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                                                                                                DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "RoomID"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                            SortExpression="RegistrationDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="110px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                            SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="145px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                            <HeaderStyle HorizontalAlign="Center" Width="55px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                            SortExpression="BedID">
                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
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
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detailInos" DataKeyNames="MonitoringID, RegistrationNo, RoomID" AutoGenerateColumns="false">
                            <ColumnGroups>
                                <telerik:GridColumnGroup HeaderText="Treatment" Name="Treatment" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                                <telerik:GridColumnGroup HeaderText="Incidence of HAIs" Name="HAIs" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                                <telerik:GridColumnGroup HeaderText="Incidence of Other Infections" Name="Other" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                            </ColumnGroups>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# (this.IsUserEditAble.Equals(false) ? string.Empty :
                                                                                string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                                                                                                                                DataBinder.Eval(Container.DataItem, "MonitoringID"), 
                                                                                                                                                                                                DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                                                                                                                                                DataBinder.Eval(Container.DataItem, "RoomID")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="MonitoringDate" HeaderText="Date" UniqueName="MonitoringDate"
                                    SortExpression="MonitoringDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>

                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsMechanicalVentilator" HeaderText="Mechanical Ventilator"
                                    UniqueName="IsMechanicalVentilator" SortExpression="IsMechanicalVentilator" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Treatment" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsInpatient" HeaderText="Inpatient"
                                    UniqueName="IsInpatient" SortExpression="IsInpatient" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Treatment" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsUrineCatheter" HeaderText="Urine Catheter"
                                    UniqueName="IsUrineCatheter" SortExpression="IsUrineCatheter" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Treatment" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsSurgery" HeaderText="Surgery"
                                    UniqueName="IsSurgery" SortExpression="IsSurgery" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Treatment" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsCentralVeinLine" HeaderText="Central Vein Line"
                                    UniqueName="IsCentralVeinLine" SortExpression="IsCentralVeinLine" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Treatment" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsIntraVeinLine" HeaderText="Intra Vein Line"
                                    UniqueName="IsIntraVeinLine" SortExpression="IsIntraVeinLine" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Treatment" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsTotalCare" HeaderText="Total Care"
                                    UniqueName="IsTotalCare" SortExpression="IsTotalCare" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Treatment" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsAntibioticDrugs" HeaderText="Antibiotic Drugs"
                                    UniqueName="IsAntibioticDrugs" SortExpression="IsAntibioticDrugs" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Treatment" />

                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVAP" HeaderText="VAP"
                                    UniqueName="IsVAP" SortExpression="IsVAP" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="HAIs" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsHAP" HeaderText="HAP"
                                    UniqueName="IsHAP" SortExpression="IsHAP" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="HAIs" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsISK" HeaderText="ISK"
                                    UniqueName="IsISK" SortExpression="IsISK" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="HAIs" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsILO" HeaderText="ILO"
                                    UniqueName="IsILO" SortExpression="IsILO" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="HAIs" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsIADP" HeaderText="IADP"
                                    UniqueName="IsIADP" SortExpression="IsIADP" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="HAIs" />

                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsPhlebitis" HeaderText="Phlebitis"
                                    UniqueName="IsPhlebitis" SortExpression="IsPhlebitis" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Other" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsDecubitus" HeaderText="Decubitus"
                                    UniqueName="IsDecubitus" SortExpression="IsDecubitus" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Other" />

                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVoid" HeaderText="Void"
                                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Last Update" UniqueName="LastUpdateDateTime"
                                    SortExpression="LastUpdateDateTime" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
