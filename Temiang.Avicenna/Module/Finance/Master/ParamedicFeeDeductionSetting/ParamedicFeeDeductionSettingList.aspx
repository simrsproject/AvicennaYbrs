<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeDeductionSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeDeductionSettingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnPreRender="grdList_PreRender">
        <MasterTableView DataKeyNames="DeductionID">
            <Columns>
                <telerik:GridBoundColumn DataField="DeductionName" HeaderText="Deduction Name" UniqueName="DeductionName"
                    SortExpression="DeductionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RegistrationTypeName" HeaderText="Registration Type" UniqueName="RegistrationTypeName"
                    SortExpression="RegistrationTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="GuarantorTypeName" HeaderText="Guarantor Type" UniqueName="GuarantorTypeName"
                    SortExpression="GuarantorTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor Name" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component" UniqueName="TariffComponentName"
                    SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicFeeDeductionMethodName" HeaderText="Paramedic Fee Deduction Method Name" UniqueName="ParamedicFeeDeductionMethodName"
                    SortExpression="ParamedicFeeDeductionMethodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn DataField="IsDeductionValueInPercent" HeaderText="Is Deduction Value In Percent" UniqueName="IsDeductionValueInPercent"
                    SortExpression="IsDeductionValueInPercent" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="DeductionValue" HeaderText="Deduction Value" UniqueName="DeductionValue"
                    SortExpression="DeductionValue" DataFormatString="{0:n2}" Visible="false">
                    <HeaderStyle HorizontalAlign="Center"/>
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="colDeductionValue" HeaderText="Deduction Value" HeaderStyle-Width="90">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtDeductionValue" runat="server" Width="70" ReadOnly="true">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="IsMainPhysicianOnly" HeaderText="Main Physician Only" UniqueName="IsMainPhysicianOnly"
                    SortExpression="IsMainPhysicianOnly">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsAfterTax" HeaderText="After Tax" UniqueName="IsAfterTax"
                    SortExpression="IsAfterTax">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsActive" HeaderText="Active" UniqueName="IsActive"
                    SortExpression="IsActive">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
