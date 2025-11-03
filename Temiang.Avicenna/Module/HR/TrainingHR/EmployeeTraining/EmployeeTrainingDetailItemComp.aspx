<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="EmployeeTrainingDetailItemComp.aspx.cs" Title="Employee Training Component"
    Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingDetailItemComp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdServiceUnitItemServiceComp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdServiceUnitItemServiceComp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTrainingID" runat="server" Text="Training Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeTrainingName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTrainingID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="true" Visible="false" />
                            &nbsp;
                            <asp:Label ID="lblEmployeeTrainingName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Employee Person"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPersonName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPersonID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" Visible="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdEmployeeTrainingComp" runat="server" OnNeedDataSource="grdEmployeeTrainingComp_NeedDataSource"
                    OnInsertCommand="grdEmployeeTrainingComp_InsertCommand" OnUpdateCommand="grdEmployeeTrainingComp_UpdateCommand"
                    AutoGenerateColumns="False" GridLines="None">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="EmployeeTrainingID, PersonID,SRComponentID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ComponentName" HeaderText="Component Name"
                                UniqueName="ComponentName" SortExpression="ComponentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Price" HeaderText="Price"
                                UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="EmployeeTrainingDetailItemCompDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EmployeeTrainingDetailItemCompDetailEditCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
