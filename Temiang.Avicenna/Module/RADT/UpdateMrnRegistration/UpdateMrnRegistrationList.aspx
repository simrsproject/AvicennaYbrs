<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="UpdateMrnRegistrationList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.UpdateMrnRegistrationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinProcess(regno) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.setUrl('UpdateMrnRegistrationDetail.aspx?regno=' + regno);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdRegisteredList.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winProcess" Animation="None" Width="1000px" Height="600px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegisteredList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisteredList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table id="Table1" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
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
                                <asp:ImageButton ID="btnFilterServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                            </td>
                            <td class="entry" width="100px">
                                <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                            </td>
                            <td class="entry" width="100px">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientSearch" runat="server" Text="Patient Name / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientSearch" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchPatient" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdRegisteredList" runat="server" OnNeedDataSource="grdRegisteredList_NeedDataSource" OnDetailTableDataBind="grdRegisteredList_DetailTableDataBind"
        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowMultiRowSelection="true">
        <MasterTableView DataKeyNames="RegistrationNo" GroupLoadMode="client">
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
                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                    <ItemTemplate>
                        <%# (this.IsUserEditAble.Equals(false) ? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"))) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RegistrationDate"
                    HeaderText="Reg. Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="RegistrationTime" HeaderText="Time"
                    UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="Sex" HeaderText="Gender"
                    UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                    SortExpression="BedID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="UpdateDateTime" Name="grdDetail" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="15">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="FROM" Name="From" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="TO" Name="To" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="UpdateDateTime"
                            HeaderText="Update Date/Time" UniqueName="UpdateDateTime" SortExpression="UpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="UpdateByUserName" HeaderText="Update By"
                            UniqueName="UpdateByUserName" SortExpression="UpdateByUserName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="FromMedicalNo" HeaderText="Medical No" UniqueName="FromMedicalNo"
                            SortExpression="FromMedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="From" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="350px" DataField="FromPatientName" HeaderText="Patient Name" UniqueName="FromPatientName"
                            SortExpression="FromPatientName" ColumnGroupName="From">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "FromSalutationName"), DataBinder.Eval(Container.DataItem, "FromPatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="FromSex" HeaderText="Gender"
                            UniqueName="FromSex" SortExpression="FromSex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ColumnGroupName="From"/>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ToMedicalNo" HeaderText="Medical No" UniqueName="ToMedicalNo"
                            SortExpression="ToMedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="To" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="350px" DataField="ToPatientName" HeaderText="Patient Name" UniqueName="ToPatientName"
                            SortExpression="ToPatientName" ColumnGroupName="To">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "ToSalutationName"), DataBinder.Eval(Container.DataItem, "ToPatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ToSex" HeaderText="Gender"
                            UniqueName="ToSex" SortExpression="ToSex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ColumnGroupName="To"/>
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
    </telerik:RadGrid>
</asp:Content>
