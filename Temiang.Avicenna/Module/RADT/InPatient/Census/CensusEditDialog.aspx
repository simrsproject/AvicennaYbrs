<%@ Page Title="Census Editor" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="CensusEditDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.CensusEditDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdModel1" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdModel1_NeedDataSource"
        OnItemDataBound="grdModel1_ItemDataBound" OnDeleteCommand="grdModel1_DeleteCommand">
        <MasterTableView DataKeyNames="RegistrationNo">
            <Columns>
                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="false" />
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderText="Reg/RM/Bed">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Col0") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn2" HeaderText="Nama/Kelas/Smf">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Col1") %><br />
                        <telerik:RadComboBox ID="cboSmf" runat="server" Width="100%" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SmfID" UniqueName="SmfID" Visible="false" />
                <telerik:GridBoundColumn DataField="TransferNo" UniqueName="TransferNo" Visible="false" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" Visible="false"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn/>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
