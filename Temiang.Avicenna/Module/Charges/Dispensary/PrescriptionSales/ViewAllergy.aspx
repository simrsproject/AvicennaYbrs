<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAllergy.aspx.cs" MasterPageFile="~/MasterPage/MasterDialog.Master"
    Inherits="Temiang.Avicenna.Module.Charges.Dispensary.ViewAllergy" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdPatientAllergy" runat="server" AutoGenerateColumns="False"
        GridLines="None" OnNeedDataSource="grdPatientAllergy_NeedDataSource">
        <MasterTableView DataKeyNames="ItemID" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="Group" HeaderText="Group " />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="Group" SortOrder="Ascending" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="StandardReferenceID" Visible="False" UniqueName="StandardReferenceID" />
                <telerik:GridBoundColumn DataField="ItemID" Visible="False" UniqueName="ItemID" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Allergen Name" SortExpression="ItemName"
                    UniqueName="ItemName">
                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Description &amp; Reaction" UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtAllergenDesc" runat="server" Width="900" MaxLength="4000"
                            Text='<%# DataBinder.Eval(Container.DataItem, "DescAndReaction") %>' ReadOnly="true" />
                    </ItemTemplate>
                    <HeaderStyle Width="600px" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true" AllowGroupExpandCollapse="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
