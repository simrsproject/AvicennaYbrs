<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="TariffComponentList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.TariffComponentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TariffComponentID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="TariffComponentID"
                    HeaderText="ID" UniqueName="TariffComponentID" SortExpression="TariffComponentID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TariffComponentName" HeaderText="Name" UniqueName="TariffComponentName"
                    SortExpression="TariffComponentName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="TariffComponentType"
                    HeaderText="Type" UniqueName="TariffComponentType" SortExpression="TariffComponentType">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsTariffParamedic"
                    HeaderText="Tariff Physician" UniqueName="IsTariffParamedic" SortExpression="IsTariffParamedic"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsIncludeInTaxCalc"
                    HeaderText="Include In Tax Calculation" UniqueName="IsIncludeInTaxCalc" SortExpression="IsIncludeInTaxCalc"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SRPphTypeName"
                    HeaderText="Pph Type Of Physician Fee" UniqueName="SRPphTypeName" SortExpression="SRPphTypeName">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsPrintParamedicInSlip"
                    HeaderText="Print Paramedic Name In Slip" UniqueName="IsPrintParamedicInSlip" SortExpression="IsPrintParamedicInSlip"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsAutoChecklistCorrectedFeeVerification"
                    HeaderText="Auto Checklist Corrected <br />Fee Verification" UniqueName="IsAutoChecklistCorrectedFeeVerification" SortExpression="IsAutoChecklistCorrectedFeeVerification"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsFeeVerificationDefaultSelected"
                    HeaderText="Fee Verification <br />Default Selected" UniqueName="IsFeeVerificationDefaultSelected" SortExpression="IsFeeVerificationDefaultSelected"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
