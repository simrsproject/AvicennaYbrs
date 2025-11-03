<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ReferToSpecialistList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.ReferToSpecialistList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoEditUrl(id, regno) {
                var url = 'ReferToSpecialistDetail.aspx?md=edit&id=' + id + '&regno=' + regno;
                window.location.href = url;
            }

            function gotoViewUrl(id, regno) {
                var url = 'ReferToSpecialistDetail.aspx?md=view&id=' + id + '&regno=' + regno;
                window.location.href = url;
            }

            function gotoAddUrl(regno) {
                var url = 'ReferToSpecialistDetail.aspx?md=new&regno=' + regno;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboParamedicID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchMedical">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchCluster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchMedical" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientSearch" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientSearch" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchPatient" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblClusterID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                    MarkFirstMatch="true" AutoPostBack="true" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchCluster" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" AllowCustomText="true"
                                    MarkFirstMatch="true">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdRegistration" runat="server" OnNeedDataSource="grdRegistration_NeedDataSource"
        OnDetailTableDataBind="grdRegistration_DetailTableDataBind" AllowSorting="true"
        AllowPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="RegistrationNo" AutoGenerateColumns="false">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="New" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (this.IsUserAddAble.Equals(false) ? string.Empty 
                            : string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>", 
                                DataBinder.Eval(Container.DataItem, "RegistrationNo")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
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
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Sex" HeaderText="Gender"
                    UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="ServiceUnitQue" DataKeyNames="ReferNo" AutoGenerateColumns="false"
                    Width="100%">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsUserEditAble.Equals(false) ? string.Empty 
                                    : string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>", 
                                        DataBinder.Eval(Container.DataItem, "ReferNo"),
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "ReferNo"),
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="ReferNo" HeaderText="Reffer No"
                            UniqueName="ReferNo" SortExpression="ReferNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="QueDate" HeaderText="Reffer Date"
                            UniqueName="QueDate" SortExpression="QueDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Reffer To" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QueNo" HeaderText="Que No"
                            UniqueName="QueNo" SortExpression="QueNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsClosed" HeaderText="Closed"
                            UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
