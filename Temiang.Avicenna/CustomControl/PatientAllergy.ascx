<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PatientAllergy.ascx.cs"
    Inherits="Temiang.Avicenna.CustomControl.PatientAllergy" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadGrid ID="grdPatientAllergy" runat="server" AutoGenerateColumns="False"
    GridLines="None">
    <MasterTableView CommandItemDisplay="None" DataKeyNames="StandardReferenceID">
        <GroupByExpressions>
            <telerik:GridGroupByExpression>
                <SelectFields>
                    <telerik:GridGroupByField FieldName="StandardReferenceID" HeaderText="Allergen Group "></telerik:GridGroupByField>
                </SelectFields>
                <GroupByFields>
                    <telerik:GridGroupByField FieldName="StandardReferenceID" SortOrder="Ascending"></telerik:GridGroupByField>
                </GroupByFields>
            </telerik:GridGroupByExpression>
        </GroupByExpressions>
        <Columns>
            <telerik:GridTemplateColumn HeaderStyle-Width="100px">
                <ItemTemplate>
                    <telerik:RadTextBox ID="txtAllergenName" runat="server" Width="300px" TextMode="multiLine"
                        MaxLength="4000" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <telerik:RadTextBox ID="txtAllergenDesc" runat="server" Width="500px" TextMode="multiLine"
                        MaxLength="4000" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
