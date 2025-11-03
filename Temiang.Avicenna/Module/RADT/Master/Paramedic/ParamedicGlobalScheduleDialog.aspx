<%@ Page Title="Schedule Template" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ParamedicGlobalScheduleDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicGlobalScheduleDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdGlobalSchedule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGlobalSchedule" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblParamedicName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPhysicianFeeType" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdGlobalSchedule" runat="server" OnNeedDataSource="grdGlobalSchedule_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdGlobalSchedule_UpdateCommand"
                    OnDeleteCommand="grdGlobalSchedule_DeleteCommand" OnInsertCommand="grdGlobalSchedule_InsertCommand"
                    AllowPaging="true">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ParamedicID, DayOfWeek"
                        PageSize="15">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="DayOfWeekName" HeaderText="Day Of Week" UniqueName="DayOfWeekName"
                                SortExpression="DayOfWeekName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn DataField="OperationalTimeID" HeaderText="ID" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <div style="width: 100%; background-color: <%#DataBinder.Eval(Container.DataItem,"OperationalTimeBackcolor")%>">
                                        <%#DataBinder.Eval(Container.DataItem,"OperationalTimeID")%>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="OperationalTimeName" HeaderText="Operational Time"
                                UniqueName="OperationalTimeName" SortExpression="OperationalTimeName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="ParamedicGlobalScheduleDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="ParamedicGlobalScheduleEditCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
