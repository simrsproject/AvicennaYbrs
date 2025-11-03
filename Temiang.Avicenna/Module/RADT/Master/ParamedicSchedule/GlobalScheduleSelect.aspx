<%@ Page Title="Schedule Setting" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="GlobalScheduleSelect.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicGlobalScheduleSelect" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdGlobalSchedule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGlobalSchedule" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td style="width: 100%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblMonth" Text="For Period" />
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboMonth" Width="100px" runat="server">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtYear" runat="server" Width="100px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblTemplate" Text="- Schedule Template -" Font-Bold="true" Font-Size="Small" />
                        </td>
                    </tr>
                </table>
                <telerik:RadGrid ID="grdGlobalSchedule" runat="server" OnNeedDataSource="grdGlobalSchedule_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true" OnUpdateCommand="grdGlobalSchedule_UpdateCommand"
                    OnDeleteCommand="grdGlobalSchedule_DeleteCommand" OnInsertCommand="grdGlobalSchedule_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ParamedicID, DayOfWeek"
                        PageSize="15">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DayOfWeekName" HeaderText="Day Of Week" UniqueName="DayOfWeekName"
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
                        <EditFormSettings UserControlName="../Paramedic/ParamedicGlobalScheduleDetail.ascx" EditFormType="WebUserControl">
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
