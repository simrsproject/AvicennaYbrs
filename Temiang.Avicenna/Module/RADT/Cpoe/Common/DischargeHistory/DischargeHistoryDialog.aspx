<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="DischargeHistoryDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.DischargeHistoryDialog"
    Title="Discharge History" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="15"
        AllowSorting="False" ExpandCollapseColumn-Display="true">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo"
            GroupLoadMode="Client" >
            <NestedViewTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 30%;vertical-align: top;">
                            <fieldset>
                                <legend>Diagnosis</legend>
                                <%#DataBinder.Eval(Container.DataItem, "Diagnosis") %><br />
                                <%#DataBinder.Eval(Container.DataItem, "ICD10")%>
                            </fieldset>
                        </td>
                        <td style="vertical-align: top;">
                            <fieldset>
                                <legend>Therapy</legend>
                                <%#DataBinder.Eval(Container.DataItem, "Therapy")%>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </NestedViewTemplate>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="RegistrationDateTime" HeaderText="Registration DateTime"
                    UniqueName="RegistrationDateTime" SortExpression="RegistrationDateTime" DataType="System.DateTime"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="BedID" HeaderText="Bed No"
                    UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DischargeDate" HeaderText="Discharge DateTime"
                    UniqueName="DischargeDate" SortExpression="DischargeDate" DataType="System.DateTime"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DischargeMethod" HeaderText="Discharge Method"
                    UniqueName="DischargeMethod" SortExpression="DischargeMethod" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DischargeCondition" HeaderText="Condition" UniqueName="DischargeCondition"
                    SortExpression="DischargeCondition" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DischargeNotes" HeaderText="Discharge Notes"
                    UniqueName="DischargeNotes" SortExpression="DischargeNotes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DischargeMedicalNotes" HeaderText="Discharge Medical Notes"
                    UniqueName="DischargeMedicalNotes" SortExpression="DischargeMedicalNotes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
