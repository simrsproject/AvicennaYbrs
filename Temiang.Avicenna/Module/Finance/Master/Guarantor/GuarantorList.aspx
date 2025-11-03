<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="GuarantorList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="GuarantorID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="GuarantorID" HeaderText="Guarantor ID"
                    UniqueName="GuarantorID" SortExpression="GuarantorID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor Name" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ContractStart" HeaderText="Contract Start"
                    UniqueName="ContractStart" SortExpression="ContractStart" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ContractEnd" HeaderText="Contract End"
                    UniqueName="ContractEnd" SortExpression="ContractEnd" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                    
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ContractNumber" HeaderText="Contact Number"
                    UniqueName="ContractNumber" SortExpression="ContractNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ContactPerson" HeaderText="Contact Person"
                    UniqueName="ContactPerson" SortExpression="ContactPerson" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRTariffType" HeaderText="Tariff Type"
                    UniqueName="SRTariffType" SortExpression="SRTariffType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ChartOfAccountCode" HeaderText="COA A/R"
                    UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
